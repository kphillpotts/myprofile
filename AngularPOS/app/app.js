var app = angular.module('angularPOS', ['mgcrea.ngStrap.modal', 'mgcrea.ngStrap.aside', 'ngGrid']);
var url = "http://vsstackpos.azurewebsites.net/api/";
//var url = "http://localhost:50991/api/"
var posController = app.controller('posController', function ($scope, $sce, $aside, $modal, transactionService, productService) {

    $scope.products = [];
    $scope.categories = [];
    $scope.selectedProducts = [];
    $scope.filteredProductsBySelectedCtg = [];
    $scope.showCategoryPane = false;
    $scope.showProductPane = false;
    $scope.codeOrQty = '';

    $scope.salesAmount = (0.00).toFixed(2);
    $scope.salesTax = function () {
        var amount = $('#salesAmount').text();
        return (amount * 0.1).toFixed(2);
    };

    $scope.salesTotal = function () {
        var amount = $('#salesAmount').text();
        var tax = $('#salesTax').text();
        return (parseFloat(amount) + parseFloat(tax)).toFixed(2);
    };

    $scope.ReceivedAmount = 0.00;
    $scope.salesBalance = function () {
        var total = $('#salesTotal').text();
        return (parseFloat(total) - parseFloat($scope.ReceivedAmount)).toFixed(2);
    };

    $scope.onClick = function (newCodeOrQty) {
        console.log(newCodeOrQty);

        if (newCodeOrQty === "c")
            $scope.codeOrQty = '';
        if (newCodeOrQty === "bs")
            $scope.codeOrQty = $scope.codeOrQty.slice(0, -1);
        if (newCodeOrQty === "." && (!($scope.codeOrQty.indexOf(".") > -1)))
            $scope.codeOrQty += newCodeOrQty;
        if (newCodeOrQty === "ok") {
            // check to see if this is a Qty (length < 4 and whole number)
            if ($scope.codeOrQty.length < 4) {
                var products = $scope.gridSelectedProducts.selectedItems;
                if (products.length == 0) return;

                if ($scope.codeOrQty % 1 == 0) {
                    angular.forEach(products, function (product) {
                        product.quantity = parseInt($scope.codeOrQty);
                    });
                }
                else if ($scope.codeOrQty % 1 != 0)
                    angular.forEach(products, function (product) {
                        product.price = parseFloat($scope.codeOrQty);
                    });
            }
            else {
                var code = $scope.codeOrQty.toLowerCase();
                // try and find the code of the product matching the entered string
                angular.forEach($scope.products, function (product) {
                    if (product.code.toLowerCase() == code)
                        addOrUpdateRow(product);
                });
            }
        }
        else {
            if (typeof newCodeOrQty === 'number')
                $scope.codeOrQty += newCodeOrQty;
        }
    };
      
    $scope.aside = {
        "title": "Title",
        "content": "Hello aside<br />This is a multiline message!"
    };    
    // Show when some event occurs (use $promise property to ensure the template has been loaded)
    $scope.showModal = function () {
        var newTransaction = { 'Id': 0, 'TransactionDate': new Date(), 'Lines': $scope.selectedProducts.length, 'Comments': "test", 'TotalAmount': $scope.salesTotal() };
        transactionService.recordTransaction(newTransaction, onTransactionPost);        
    };

    onTransactionPost = function(data){
        console.log('Transaction posted successfully ' + data);
        $scope.NewTransaction = { 'Id': data.id, 'TransactionDate': data.transactionDate, 'Lines': data.lines, 'Comments': data.comments, 'TotalAmount': data.totalAmount };
        var filterProductsModal = $modal({ scope: $scope, template: 'modal/modal.html', show: false });
        filterProductsModal.$promise.then(filterProductsModal.show);
    }

    $scope.showCategoryLookup = function () {
        $scope.showCategoryPane = !$scope.showCategoryPane;
        if ($scope.showCategoryPane || $scope.showProductPane)
            $("#lookupBtn").html('<span class="glyphicon glyphicon-list"> Back To POS</span>');
        else
            $("#lookupBtn").html('<span class="glyphicon glyphicon-search"> Lookup Product</span>');
    };
    $scope.payCash = function () {
        $scope.showModal();
    }

    $scope.listFilteredProducts = function (cat) {
        $scope.selectedCategory = cat;
        $.each($scope.products, function (product) {
            $scope.filteredProductsBySelectedCtg.push(product);
        });
        $scope.showProductPane = true;
        $scope.showCategoryPane = false;
    };

    $scope.selectProduct = function (product) {
        addOrUpdateRow(product);
        $scope.showProductPane = false;
    };

    function calculateGrandTotal() {
        $scope.salesAmount = 0;
        angular.forEach($scope.selectedProducts, function (row) {
            var rowTotal = (row.quantity * row.price);
            row.getTotal = function () {
                return rowTotal;
            };
            $scope.salesAmount += rowTotal;
        });
        // Round it to 2 decimal spots - Need to check rounding errors
        $scope.salesAmount = $scope.salesAmount.toFixed(2);
    };

    $scope.deleteThisRow = function (row) {
        if (row.rowIndex > -1) {
            $scope.selectedProducts.splice(row.rowIndex, 1);
            calculateGrandTotal();
        }
    };

    $scope.deleteRows = function () {
        $scope.selectedProducts = [];
        calculateGrandTotal();
    };

    function addOrUpdateRow(product) {

        $scope.gridSelectedProducts.selectedItems = [];
        var result = $.grep($scope.selectedProducts, function (e) { return e.id == product.id; });

        if (result.length == 0) {
            // this item has never been added before
            var newProduct = { 'id': product.id, 'code': product.code, 'name': product.name, 'price': product.price, 'quantity': 1 };
            $scope.selectedProducts.push(newProduct);
            $scope.gridSelectedProducts.selectedItems.push(newProduct);
        } else if (result.length == 1) {
            // Item exists - just increment the quantity
            result[0].quantity = result[0].quantity + 1;
            $scope.gridSelectedProducts.selectedItems.push(result);
        };
        calculateGrandTotal();
    }

    $scope.$on('ngGridEventEndCellEdit', function (data) {
        console.log('Grid Cell Edit ' + data);
        calculateGrandTotal();
    });

    productService.getProducts(populateProducts);
    function populateProducts(data) {
        $.each(data, function (key, value) {
            console.log('key: ' + key + ' value:' + value);
            $scope.products.push(value);
            if ($.inArray(value.category, $scope.categories) === -1)
                $scope.categories.push(value.category);
        });
    }
    //$.getJSON("/app/products.json", function (data) {
    //    $.each(data, function (key, value) {
    //        $scope.products.push(value);
    //        // Store the Category seperately in a list
    //        if ($.inArray(value.Category, $scope.categories) === -1)
    //            $scope.categories.push(value.Category);
    //    });
    //});
    //#383e4b
    var firstCol = 'style = "background-color: #428bca;color: #fff;font-size: 1.2em;border-top-left-radius: 6px;"';
    var lastCol = 'style = "background-color: #428bca;color: #fff;font-size: 1.2em;border-top-right-radius: 6px;"';
    var restCol = 'style="background-color: #428bca;color: #fff;font-size: 1.2em;"';

    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>' +
                               '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>' +
                               '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>' +
                               '<div class="ngSortPriority">{{col.sortPriority}}</div>' +
                             '</div>' +
                             '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';
    var delHeaderTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText"><a ng-click="deleteRows(); $event.stopPropagation();" href="#"><span class="glyphicon glyphicon-trash  text-center bigFont "></span></a></div></div>' +
                           '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';
    var firstHeaderCellTemplate = '<div ' + firstCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + delHeaderTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;

    $scope.gridSelectedProducts = {
        data: 'selectedProducts',
        selectedItems: [],
        headerRowHeight: 42,
        rowHeight: 40,
        enableCellSelection: false,
        enableRowSelection: true,
        enableCellEdit: false,
        showGroupPanel: false,
        showFilter: false,
        showFooter: false,
        enablePaging: false,
        showColumnMenu: false,
        enablePinning: false,
        enableColumnResize: true,
        columnDefs: [{ field: 'code', displayName: 'Code', headerCellTemplate: firstHeaderCellTemplate, enableCellEdit: false },
            { field: 'name', width: '35%', displayName: 'Product Name', headerCellTemplate: headerCellTemplate, enableCellEdit: false, enableCellFocus: false },
            { field: 'price', displayName: 'Unit Price', headerCellTemplate: headerCellTemplate, enableCellSelection: true, enableCellEdit: true },
            { field: 'quantity', displayName: 'Quantity', headerCellTemplate: headerCellTemplate, enableCellSelection: true, enableCellEdit: true },
            { field: 'getTotal()', displayName: 'Total', headerCellTemplate: headerCellTemplate, enableCellEdit: false },
            {
                displayName: 'DEL', width: '50', enableCellEdit: false, headerCellTemplate: lastHeaderCellTemplate, cellTemplate:
                 '<div class="grid-action-cell">' +
                 '<a ng-click="deleteThisRow(row); $event.stopPropagation();" href="#"><span class="glyphicon glyphicon-trash  text-center bigFont "></span></a></div>'
            }
        ]
    };
});

app.service("transactionService", function ($http, $q) {

    // Return public API.
    return ({
        recordTransaction: recordTransaction
    });

    function recordTransaction(transactionData, cb) {

        var request = $http({
            url:url+"transactions?callback=cb",
            dataType: "json",
            method: "POST",
            headers: {"Content-Type": "application/json"},
            data: transactionData,
        }).success(function (data) {
            // With the data succesfully returned, call our callback
            cb(data);
        }).error(function () {
            alert("error");
        });
    }
    // transform the error response, unwrapping the application dta from
    // the API response payload.
    function handleError(response) {
        if (!angular.isObject(response.data) || !response.data.message) {
            return ($q.reject("An unknown error occurred."));
        }
        // Otherwise, use expected error message.
        return ($q.reject(response.data.message));
    }
    // transform the successful response, unwrapping the application data
    // from the API response payload.
    function handleSuccess(response) {
        return (response.data);
    }
});

app.service("productService", function ($http, $q) {

    // Return public API.
    return ({
        getProducts: getProducts
    });

    function getProducts(callbackFunc) {
        var request = $http({
            method: "get",
            url: url+"products",
            dataType: "json",
            headers: { "Content-Type": "application/json" },
            params: {action: "get"}
        }).success(function(data){
            // With the data succesfully returned, call our callback
            callbackFunc(data);
        }).error(function(){
            alert("error");
        });
        return (request.then(handleSuccess, handleError));
    }

    function handleError(response) {
        if (!angular.isObject(response.data) || !response.data.message) {
            return ($q.reject("An unknown error occurred."));
        }
        // Otherwise, use expected error message.
        return ($q.reject(response.data.message));
    }
    // transform the successful response, unwrapping the application data
    // from the API response payload.
    function handleSuccess(response) {
        return (response.data);
    }
});