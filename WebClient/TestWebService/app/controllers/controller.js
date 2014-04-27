var masterServiceAddress = "http://vsbackoffice.azurewebsites.net/BackOfficeService.asmx";

app.controller('introController', function ($scope, stopwatchService) {
    
        
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/GetBackOfficeVersion",
            //url: "http://localhost:3460/IntegrationService.asmx/GetHeadOfficeLinkedProducts",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data:"",
            dataType: "json", // datatype is the output expected type
            success: function (data) {
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                $scope.backOfficeVersion = data.d;
                $scope.$apply();
            },
            error: function (msg) {
                alert('Error Occured: ' + msg.responseText);
            }
        });
    
});
app.controller('linkedProductController', function ($scope, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    function resetData() {
        $scope.productIDs = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 20;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
    };

    $scope.PopulateInput = function () {
        resetData();
        
        stopwatchService.reset();
        stopwatchService.start();
        $scope.gridMessage = "Invoking the service. Please wait...";
        //$.support.cors = true;
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/GetHeadOfficeLinkedProducts",
           
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "' }",
            
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value/10;
                var $page = $(xml);
                $page.find("ArrayOfString").each(function () {
                    var $data = $(this);
                    var $stringList = $data.find("string");
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.productIDs.push(result);
                    }
                });
                //or $scope.productIDs = xml.getElementsByTagName("string")[0].textContent;
                $scope.groupToPages();
                $scope.gridMessage = 'No Records Found';
                $scope.$apply();
            },
            error: function (msg) {
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value/10;
                $scope.gridMessage = 'Error Occured: ' + msg.responseText;
            }
        });
    };

    $scope.ClearData = function() {
        resetData();
        $scope.groupToPages();
    };

    // Pagination Functions
    $scope.groupToPages = function () {
        $scope.pagedItems = [];
        if ($scope.productIDs) {
            for (var i = 0; i < $scope.productIDs.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.productIDs[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.productIDs[i]);
                }
            }
        }
    };

    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pagedItems.length - 1) {
            $scope.currentPage++;
        }
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };

    $scope.selectShowItems = function () {
        
        if (!$scope.productIDs)
            return;
        
        if ($scope.selectedShowCount <= 0 )
            $scope.itemsPerPage = $scope.productIDs === null ? 0 : $scope.productIDs.length;
        else {
            $scope.itemsPerPage = $scope.selectedShowCount;
        }
        if ($scope.itemsPerPage > 0 && $scope.productIDs) {
            $scope.groupToPages();
        }
    };
});

app.controller('linkedSupplierController', function ($scope, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    function resetData() {
        $scope.supplierIDs = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 20;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
    };

    $scope.PopulateInput = function () {
        resetData();
        stopwatchService.reset();
        stopwatchService.start();
        $scope.gridMessage = "Invoking the service. Please wait...";
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/GetHeadOfficeLinkedSuppliers",
            //url: "http://localhost:3460/IntegrationService.asmx/GetHeadOfficeLinkedSuppliers",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "' }",
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value/10;
                var $page = $(xml);
                //you can now use all the jQuery to conquer the world
                $page.find("ArrayOfString").each(function () {
                    var $data = $(this);
                    var $stringList = $data.find("string");
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.supplierIDs.push(result);
                    }
                });
                //or $scope.productIDs = xml.getElementsByTagName("string")[0].textContent;
                $scope.groupToPages();
                $scope.gridMessage = 'No Records Found';
                $scope.$apply();
            },
            error: function (msg) {
                //var $errorMsg = JSON.parse(msg.responseText);
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value/10;
                $scope.gridMessage = 'Error Occured: ' + msg.responseText;
            }
        });
    };

    $scope.ClearData = function () {
        resetData();
        $scope.groupToPages();
    };

    // Pagination Functions
    $scope.groupToPages = function () {
        $scope.pagedItems = [];
        if ($scope.supplierIDs) {
            for (var i = 0; i < $scope.supplierIDs.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.supplierIDs[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.supplierIDs[i]);
                }
            }
        }
    };

    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pagedItems.length - 1) {
            $scope.currentPage++;
        }
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };

    $scope.selectShowItems = function () {

        if (!$scope.supplierIDs)
            return;

        if ($scope.selectedShowCount <= 0)
            $scope.itemsPerPage = $scope.supplierIDs === null ? 0 : $scope.supplierIDs.length;
        else {
            $scope.itemsPerPage = $scope.selectedShowCount;
        }
        if ($scope.itemsPerPage > 0 && $scope.supplierIDs) {
            $scope.groupToPages();
        }
    };
});

var ModalInstanceCtrl = function ($scope, $modalInstance, items) {
    $scope.items = items;
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

app.controller('manageSupplierItemController', function ($scope, $modal, convertExcelToGridService, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.loadCount = 100;
    
    $scope.excelToGrid = function () {
        var parseOutput = convertExcelToGridService.convert2Grid($scope.excelText);
        $scope.myData = parseOutput.dataGrid;
        $scope.headerNames = parseOutput.headerNames;
    };
    
    var firstCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-left-radius: 6px;"';
    var lastCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-right-radius: 6px;"';
    var restCol = 'style="background-color: #383e4b;color: #fff;font-weight: bold;"';
    
    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>'+
                               '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>'+
                               '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>'+
                               '<div class="ngSortPriority">{{col.sortPriority}}</div>'+
                             '</div>'+
                             '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';
    
    var firstHeaderCellTemplate = '<div '+ firstCol +' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    
    $scope.gridOptions = {
        data: 'myData',
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEdit: true,
        showGroupPanel: false,
        showFilter: true,
        showFooter: true,
        pagingOptions: $scope.pagingOptions,
        enablePaging: true,
        showColumnMenu: true,
        enablePinning: true,
        enableColumnResize: true,
        columnDefs: [{ field: 0, displayName: 'HeadOffice Item HQID', headerCellTemplate: firstHeaderCellTemplate },
            { field: 1, displayName: 'HeadOffice Supplier HQID', headerCellTemplate: headerCellTemplate },
            { field: 2, displayName: 'Preferred Supplier', headerCellTemplate: headerCellTemplate },
            { field: 3, displayName: 'Set As Primary', headerCellTemplate: headerCellTemplate },
            { field: 4, displayName: 'Pack Size', headerCellTemplate: headerCellTemplate },
            { field: 5, displayName: 'Reorder Number', headerCellTemplate: headerCellTemplate },
            { field: 6, displayName: 'Supplier RRP', headerCellTemplate: headerCellTemplate },
            { field: 7, displayName: 'Minimum Order Qty', headerCellTemplate: headerCellTemplate },
            { field: 8, displayName: 'Supplier Cost', headerCellTemplate: lastHeaderCellTemplate }
        ]
    };
    
    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: false
    };
    $scope.pagingOptions = {
        pageSizes: [250, 500, 1000], //page Sizes
        pageSize: 250, //Size of Paging data
        totalServerItems: 0, //how many items are on the server (for paging)
        currentPage: 1 //what page they are currently on
    };
    
    self.getPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            self.gettingData = true;
            var data;
            if (searchText) {
                var ft = searchText.toLowerCase();
                data = $scope.myData.filter(function (item) {
                    return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                });
            } else {
                data = $scope.myData;
            }
            var pagedData = data.split((page - 1) * pageSize, page * pageSize);
            $scope.pagingOptions.totalServerItems = data.length;
            $scope.myData = pagedData;
            if (!$scope.$$phase) {
                $scope.$apply();
            }
            self.gettingData = false;
        }, 100);
    };
    $scope.$watch('pagingOptions', function () {
        if (!self.poInit || self.gettingData) {
            self.poInit = true;
            return;
        }
        self.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
    }, true);
    $scope.$watch('filterOptions', function () {
        if (!self.foInit || self.gettingData) {
            self.foInit = true;
            return;
        }
        self.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
    }, true);
    self.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

    function convertToSupplierItems(headerNames, dataArray) {
        var jsonSupplierItem = [];
        if (!dataArray) return jsonSupplierItem;
        for (var r = 0; r < dataArray.length; r++) {
            // for each row reset the variables
            var itemId = '';
            var supplierId = '';
            var preferred = 'TRUE';
            var primary = 'TRUE';
            var packSize = 0;
            var reorderNumber = '';
            var rrp = 0;
            var minQty = 0;
            var cost = 0;

            if (dataArray[r]) {

                for (var i = 0; i < headerNames.length; i++) {
                    if ((dataArray[r][i])) {
                        switch (i) {
                            case 0:
                                itemId = dataArray[r][i];
                                break;
                            case 1:
                                supplierId = dataArray[r][i];
                                break;
                            case 2:
                                preferred = dataArray[r][i];
                                break;
                            case 3:
                                primary = dataArray[r][i];
                                break;
                            case 4:
                                packSize = dataArray[r][i];
                                break;
                            case 5:
                                reorderNumber = dataArray[r][i];
                                break;
                            case 6:
                                rrp = dataArray[r][i];
                                break;
                            case 7:
                                minQty = dataArray[r][i];
                                break;
                            case 8:
                                cost = dataArray[r][i];
                                break;
                        }
                    }
                }
                jsonSupplierItem.push({ 'ProductHQID': itemId, 'SupplierHQID': supplierId, 'PreferredSupplier': preferred, 'SetPreferredAsPrimary': primary, 'PackSize': packSize, 'ReorderNumber': reorderNumber, 'SupplierRRP': rrp, 'MinOrderQty': minQty, 'SupplierCost': cost });
            }
        }
        return jsonSupplierItem;
    }
    
    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return JSON.stringify($scope.supplierItemErrors, null, 2);
                }
            }
        });
    };

    $scope.ManageSupplierItems = function() {
        $scope.supplierItemErrors = [];
        stopwatchService.reset();
        stopwatchService.start();
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/CreateUpdateSupplierProducts",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            //data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', supplierProducts: [{'HeadOfficeItemHQID':341070,'HeadOfficeSupplierHQID':341070,'PreferredSupplier':'TRUE','PackSize':36,'ReorderNumber':'65805222100','SupplierRRP':10.23,'MinOrderQty':36, 'SupplierCost' : 11.90 }]  }",
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', supplierProducts: " + JSON.stringify(convertToSupplierItems($scope.headerNames, $scope.myData)) + " }",
            dataType: "xml", // datatype is the output expected type
            success: function(xml) {
                var $page = $(xml);
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value/10;
                $page.find("ArrayOfProcessResult").each(function () {
                    var $data = $(this);
                    var $processResult = $data.find("ProcessResult");
                    var $stringList = $processResult.find('Message');
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.supplierItemErrors.push(result);
                    }
                    if ($scope.supplierItemErrors.length > 0)
                        alert('Errors Occured. Please click on View Results to learn more');
                    else alert('Successfully Created/Updated Supplier Items in Fred Office.');
                });
            },
            error: function(msg) {
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value/10;
                alert('Errors Occured. Please click on View Results to learn more');
                $scope.supplierItemErrors = msg.responseText;
            }
        });
    };

    $scope.LoadTest = function (testInserts) {
        var message = (testInserts) ? "Arrgh you sure you want to do Load Testing - Inserts" : "Arrgh you sure you want to do Load Testing - Updates";
            message = message + ", ya pirate?";
        var doIt = confirm(message);
        if (!doIt) return;
        var now = new Date();
        console.log('START - Load Test Beginning at '+ now);
        // Get the Batch of 100
        var loadTestCounter = $scope.loadCount;
        var loadResultCounter = 0;
        var batchSize = 100;
        var supplierId = 1;
        $scope.supplierItemErrors = [];
        // Kick Off Stopwatch
        stopwatchService.reset();
        stopwatchService.start();
        var batchCounter = 1;
        
        $scope.batchPayLoad = [];
        
        for (var loadCount = 0; loadCount < loadTestCounter; loadCount++) {
            var payLoad = [];
            // construct the batch
            for (var i = 0; i < batchSize; i++) {
                payLoad.push({ 'ProductHQID': batchCounter, 'SupplierHQID': supplierId, 'PreferredSupplier': 'TRUE', 'SetPreferredAsPrimary': 'TRUE', 'PackSize': 12, 'ReorderNumber': (batchCounter + 10000), 'SupplierRRP': 10.00, 'MinOrderQty': 36, 'SupplierCost': 5.99 });
                if (testInserts)
                    batchCounter = batchCounter + 1;
            }
            // send the batch
            //console.log('Running Load Counter - ' + loadCount + ' Processing ' + batchSize + 'inserts.');
            $scope.batchPayLoad.push(payLoad);
        }
        
        // Fire the first batch
        processNext(0);
        
        function processNext(batchIndex) {
            var payLoadData = $scope.batchPayLoad[batchIndex];
            $.ajax({
                type: "POST",
                url: masterServiceAddress + "/CreateUpdateSupplierProducts",
                contentType: "application/json; charset=utf-8", // contenttype is the input type
                //data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', supplierProducts: [{'HeadOfficeItemHQID':341070,'HeadOfficeSupplierHQID':341070,'PreferredSupplier':'TRUE','PackSize':36,'ReorderNumber':'65805222100','SupplierRRP':10.23,'MinOrderQty':36, 'SupplierCost' : 11.90 }]  }",
                data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', supplierProducts: " + JSON.stringify(payLoadData) + " }",
                dataType: "xml", // datatype is the output expected type
                success: function (xml) {
                    var $page = $(xml);

                    $page.find("ArrayOfProcessResult").each(function () {
                        var $data = $(this);
                        var $processResult = $data.find("ProcessResult");
                        var $stringList = $processResult.find('Message');
                        for (var index = 0; index < $stringList.length; index++) {
                            var result = $stringList[index].textContent;
                            $scope.supplierItemErrors.push('Load #' + loadCount + '.Result - ' + index + ' - Error Returned - ' + result);
                        }
                    });

                    loadResultCounter = loadResultCounter + 1;
                    now = new Date();
                    console.log('LAP - ' + loadResultCounter + ' Successfully Finished Processing ' + batchSize + ' inserts/updates.' + ' Lapped at ' + now);
                    if (loadTestCounter == loadResultCounter) {
                        stopwatchService.stop();
                        $scope.timetaken = stopwatchService.data.value / 10;
                        console.log('Finished - Load Testing ' + loadTestCounter + ' batches at ' + now + '. Total Seconds ' + $scope.timetaken);
                        alert('Finished - Load Testing ' + loadTestCounter + ' batches with ' + batchSize + ' inserts/updates per batch in ' + $scope.timetaken + ' seconds.');
                    }
                },
                error: function (msg) {
                    loadResultCounter = loadResultCounter + 1;
                    $scope.supplierItemErrors.push(msg.responseText);
                    console.log('LAP - ' + loadCount + ' Errored Processing ' + batchSize + ' inserts/updates.' + ' Lapped at ' + now);
                    if (loadTestCounter == loadResultCounter) {
                        stopwatchService.stop();
                        $scope.timetaken = stopwatchService.data.value / 10;
                        console.log('Errored - Load Testing ' + loadTestCounter + ' batches at ' + now + '. Total Seconds ' + $scope.timetaken);
                        alert('Errored - Load Testing ' + loadTestCounter + ' batches with ' + batchSize + ' inserts/updates per batch took ' + $scope.timetaken + ' seconds.');
                    }
                },
                complete: function (msg) {
                    batchIndex = batchIndex + 1;
                    // Process only if less than the count of the array
                    if (batchIndex < $scope.batchPayLoad.length)
                        processNext(batchIndex);
                }
            });
        }
    };
});

app.controller('manageProductsController', function ($scope, $modal, convertExcelToGridService, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.loadCount = 100;
    
    $scope.excelToGrid = function () {
        var parseOutput = convertExcelToGridService.convert2Grid($scope.excelText);
        $scope.productData = parseOutput.dataGrid;
        $scope.headerNames = parseOutput.headerNames;
    };

    var firstCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-left-radius: 6px;text-wrap: unrestricted;"';
    var lastCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-right-radius: 6px;text-wrap: unrestricted;"';
    var restCol = 'style="background-color: #383e4b;color: #fff;font-weight: bold;text-wrap: unrestricted;"';

    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>' +
                               '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>' +
                               '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>' +
                               '<div class="ngSortPriority">{{col.sortPriority}}</div>' +
                             '</div>' +
                             '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';

    var firstHeaderCellTemplate = '<div ' + firstCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;

    $scope.gridProducts = {
        data: 'productData',
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEdit: true,
        showGroupPanel: false,
        showFilter: false,
        showFooter: true,
        showColumnMenu: true,
        enablePinning: true,
        enableColumnResize: true,
        filterOptions: { filterText: 'filteringText', useExternalFilter: true },
        columnDefs: [{ field: 0, displayName: 'HeadOffice Item HQID', headerCellTemplate: firstHeaderCellTemplate },
            { field: 1, displayName: 'Description', headerCellTemplate: headerCellTemplate },
            { field: 2, displayName: 'Product Type', headerCellTemplate: headerCellTemplate },
            { field: 3, displayName: 'Purchase Tax', headerCellTemplate: headerCellTemplate },
            { field: 4, displayName: 'Sales Tax', headerCellTemplate: headerCellTemplate },
            { field: 5, displayName: 'Alias1', headerCellTemplate: headerCellTemplate },
            { field: 6, displayName: 'Alias2', headerCellTemplate: headerCellTemplate },
            { field: 7, displayName: 'TW Managed', headerCellTemplate: headerCellTemplate },
            { field: 8, displayName: 'Department Code', headerCellTemplate: headerCellTemplate },
            { field: 9, displayName: 'Category Code', headerCellTemplate: headerCellTemplate },
            { field: 10, displayName: 'Manufacturer HQID', headerCellTemplate: headerCellTemplate },
            { field: 11, displayName: 'Item Location', headerCellTemplate: headerCellTemplate },
            { field: 12, displayName: 'TW Brand', headerCellTemplate: headerCellTemplate },
            { field: 13, displayName: 'TW Name', headerCellTemplate: headerCellTemplate },
            { field: 14, displayName: 'TW Size', headerCellTemplate: headerCellTemplate },
            { field: 15, displayName: 'Discontinued', headerCellTemplate: headerCellTemplate },
            { field: 16, displayName: 'Update Description', headerCellTemplate: headerCellTemplate },
            { field: 17, displayName: 'Update Taxes', headerCellTemplate: headerCellTemplate },
            { field: 18, displayName: 'Update ItemLocation', headerCellTemplate: headerCellTemplate },
            { field: 19, displayName: 'Append Barcodes', headerCellTemplate: lastHeaderCellTemplate }
        ]
    };

    $scope.filterOptions = {
        filterText: "ABC",
        useExternalFilter: true
    };
    
    $scope.$on('filterChanged', function (evt, text) {
        $scope.filteringText = text;
    });
    $scope.setSelection = function () {
        $scope.gridProducts.selectRow(0, true);
    };
    //$scope.dropDownOpts = ['editing', 'is', 'impossibru?'];

    function convertToProducts(headerNames, dataArray) {
        var jsonProduct = [];
        if (!dataArray) return jsonProduct;
        for (var r = 0; r < dataArray.length; r++) {
            // for each row reset the variables
            var ProductHQID = '';
            var Description  = '';
            var ProductType = 0;
            var HasPurchaseTax  = 'TRUE';
            var HasSalesTax = 'TRUE';
            var Aliases = [];
            var TWManaged = 'TRUE';
            var DepartmentCode = '';
            var CategoryCode = '';
            var ManufacturerHQID = 0;
            var ItemLocation = '';
            var TWBrand = '';
            var TWName = '';
            var TWSize = '';
            var Discontinued = 'FALSE';
            var UpdateDescription = 'FALSE';
            var UpdateTaxes = 'FALSE';
            var UpdateItemLocation = 'FALSE';
            var AppendBarcodes = 'FALSE';

            if (dataArray[r]) {

                for (var i = 0; i < headerNames.length; i++) {
                    if (dataArray[r][i]) {
                        switch (i) {
                            case 0:
                                ProductHQID = dataArray[r][i];
                                break;
                            case 1:
                                Description = dataArray[r][i];
                                break;
                            case 2:
                                ProductType = dataArray[r][i];
                                break;
                            case 3:
                                HasPurchaseTax = dataArray[r][i];
                                break;
                            case 4:
                                HasSalesTax = dataArray[r][i];
                                break;
                            case 5:
                            case 6:
                                Aliases.push(dataArray[r][i]);
                                break;
                            case 7:
                                TWManaged = dataArray[r][i];
                                break;
                            case 8:
                                DepartmentCode = dataArray[r][i];
                                break;
                            case 9:
                                CategoryCode = dataArray[r][i];
                                break;
                            case 10:
                                ManufacturerHQID = dataArray[r][i];
                                break;
                            case 11:
                                ItemLocation = dataArray[r][i];
                                break;
                            case 12:
                                TWBrand = dataArray[r][i];
                                break;
                            case 13:
                                TWName = dataArray[r][i];
                                break;
                            case 14:
                                TWSize = dataArray[r][i];
                                break;
                            case 15:
                                Discontinued = dataArray[r][i];
                                break;
                            case 16:
                                UpdateDescription = dataArray[r][i];
                                break;
                            case 17:
                                UpdateTaxes = dataArray[r][i];
                                break;
                            case 18:
                                UpdateItemLocation = dataArray[r][i];
                                break;
                            case 19:
                                AppendBarcodes = dataArray[r][i];
                                break;
                        }
                    }
                }
                jsonProduct.push({
                    'ProductHQID': ProductHQID,
                    'Description': Description,
                    'ProductType': ProductType,
                    'HasPurchaseTax': HasPurchaseTax,
                    'HasSalesTax' : HasSalesTax,
                    'Aliases':Aliases,
                    'TWManaged': TWManaged,
                    'DepartmentCode': DepartmentCode,
                    'CategoryCode': CategoryCode,
                    'ManufacturerHQID': ManufacturerHQID,
                    'ItemLocation': ItemLocation,
                    'Brand': TWBrand,
                    'Name': TWName,
                    'Size': TWSize,
                    'Discontinued': Discontinued,
                    'UpdateDescription': UpdateDescription,
                    'UpdateTaxes': UpdateTaxes,
                    'UpdateItemLocation': UpdateItemLocation,
                    'AppendBarcodes': AppendBarcodes
                });
            }
        }
        return jsonProduct;
    }

    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.productErrors;
                }
            }
        });
    };

    $scope.ManageProducts = function () {
        $scope.productErrors = [];
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/CreateUpdateProducts",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
          
            //data: "{ userName : 'Web', password: 'PO2005', products: [{'ProductHQID':'11471','Description':'KWELLS TAB 10','ProductType':'0','HasPurchaseTax':'TRUE','Aliases':['0000000000055','9310041227306'],'TWManaged':'TRUE','DepartmentCode':'','CategoryCode':'','ManufacturerHQID':'0','ItemLocation':'','TWBrand':'','TWName':'','TWSize':'','Discontinued':'FALSE','UpdateDescription':'TRUE','UpdatePurchaseTax':'TRUE','UpdateSalesTax':'TRUE','UpdateItemLocation':'TRUE','AppendBarcodes':'TRUE'}] }",
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', products: " + JSON.stringify(convertToProducts($scope.headerNames, $scope.productData)) + " }",
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                //alert('success');
                var $page = $(xml);
                //you can now use all the jQuery to conquer the world
                $page.find("ArrayOfProcessResult").each(function () {
                    var $data = $(this);
                    var $processResult = $data.find("ProcessResult");
                    var $stringList = $processResult.find('Message');
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.productErrors.push(result);
                    }
                    if ($scope.productErrors.length > 0)
                        alert('Errors Occured. Please click on View Results to learn more');
                    else alert('Successfully Created/Updated Items in Fred Office.');
                });
            },
            error: function (msg) {
                alert('Errors Occured. Please click on View Results to learn more');
                $scope.productErrors = msg.responseText;
            }
        });
    };
    
    $scope.LoadTest = function (testInserts) {
        var message = (testInserts) ? "Are you sure you want to do Load Testing - Inserts" : "Are you sure you want to do Load Testing - Updates";
        message = message + ", ya muppet?";
        var doIt = confirm(message);
        if (!doIt) return;
        var now = new Date();
        console.log('START - Load Test Beginning at ' + now);
        // Get the Batch of 100
        var loadTestCounter = $scope.loadCount;
        var loadResultCounter = 0;
        var batchSize = 100;
        var supplierId = 1001;
        $scope.productErrors = [];
        $scope.supplierItemErrors = [];
        // Kick Off Stopwatch
        stopwatchService.reset();
        stopwatchService.start();
        var batchCounter = 1;
        
        $scope.batchPayLoad = [];
        for (var loadCount = 0; loadCount < loadTestCounter; loadCount++) {
            var payLoad = [];
            // construct the batch
            for (var i = 0; i < batchSize; i++) {
                payLoad.push({ 'ProductHQID': batchCounter, 'Description': 'Item Number ' + batchCounter, 'ProductType': '0', 'HasPurchaseTax': 'TRUE', 'Aliases': ['ItemAlias' + batchCounter, 'Alias' + batchCounter], 'TWManaged': 'TRUE', 'DepartmentCode': '', 'CategoryCode': '', 'ManufacturerHQID': '0', 'ItemLocation': '', 'TWBrand': '', 'TWName': '', 'TWSize': '', 'Discontinued': 'FALSE', 'UpdateDescription': 'TRUE', 'UpdateTaxes': 'TRUE', 'UpdateItemLocation': 'TRUE', 'AppendBarcodes': 'TRUE' });
                //payLoad.push({ 'ProductHQID': batchCounter, 'SupplierHQID': supplierId, 'PreferredSupplier': 'TRUE', 'PackSize': 12, 'ReorderNumber': (batchCounter + 10000), 'SupplierRRP': 10.00, 'MinOrderQty': 36, 'SupplierCost': 5.99 });
                if (testInserts)
                    batchCounter = batchCounter + 1;
            };
            
            // send the batch
            //console.log('Running Load Counter - ' + loadCount + ' Processing ' + batchSize + 'inserts.');
            $scope.batchPayLoad.push(payLoad);
        }
        // Fire the first batch
        processNext(0);
        
        function processNext(batchIndex) {
            
            var payLoadData = $scope.batchPayLoad[batchIndex];
            
            $.ajax({
                type: "POST",
                url: masterServiceAddress + "/CreateUpdateProducts",
                contentType: "application/json; charset=utf-8", // contenttype is the input type
                //data: "{ userName : 'Web', password: 'PO2005', products: [{'ProductHQID':'11471','Description':'KWELLS TAB 10','ProductType':'0','HasPurchaseTax':'TRUE','Aliases':['0000000000055','9310041227306'],'TWManaged':'TRUE','DepartmentCode':'','CategoryCode':'','ManufacturerHQID':'0','ItemLocation':'','TWBrand':'','TWName':'','TWSize':'','Discontinued':'FALSE','UpdateDescription':'TRUE','UpdatePurchaseTax':'TRUE','UpdateSalesTax':'TRUE','UpdateItemLocation':'TRUE','AppendBarcodes':'TRUE'}] }",
                data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', products: " + JSON.stringify(payLoadData) + " }",
                dataType: "xml", // datatype is the output expected type
                success: function (xml) {
                    var $page = $(xml);

                    $page.find("ArrayOfProcessResult").each(function () {
                        var $data = $(this);
                        var $processResult = $data.find("ProcessResult");
                        var $stringList = $processResult.find('Message');
                        for (var index = 0; index < $stringList.length; index++) {
                            var result = $stringList[index].textContent;
                            $scope.productErrors.push('Load #' + loadCount + '.Result - ' + index + ' - Error Returned - ' + result);
                        }
                    });

                    loadResultCounter = loadResultCounter + 1;
                    now = new Date();
                    console.log('LAP - ' + loadResultCounter + ' Successfully Finished Processing ' + batchSize + ' inserts.' + ' Lapped at ' + now);
                    if (loadTestCounter == loadResultCounter) {
                        stopwatchService.stop();
                        $scope.timetaken = stopwatchService.data.value / 10;
                        console.log('Finished - Load Testing ' + loadTestCounter + ' batches at ' + now + '. Total Seconds ' + $scope.timetaken);
                        alert('Finished - Load Testing ' + loadTestCounter + ' batches with ' + batchSize + ' inserts per batch in ' + $scope.timetaken + ' seconds.');
                    }
                },
                error: function (msg) {
                    loadResultCounter = loadResultCounter + 1;
                    $scope.productErrors.push(msg.responseText);
                    console.log('LAP - ' + loadCount + ' Errored Processing ' + batchSize + ' inserts.' + ' Lapped at ' + now);
                    if (loadTestCounter == loadResultCounter) {
                        stopwatchService.stop();
                        $scope.timetaken = stopwatchService.data.value / 10;
                        console.log('Errored - Load Testing ' + loadTestCounter + ' batches at ' + now + '. Total Seconds ' + $scope.timetaken);
                        alert('Errored - Load Testing ' + loadTestCounter + ' batches with ' + batchSize + ' inserts per batch took ' + $scope.timetaken + ' seconds.');
                    }
                },
                complete: function (res) {
                    batchIndex = batchIndex + 1;
                    // Process only if less than the count of the array
                    if (batchIndex < $scope.batchPayLoad.length)
                        processNext(batchIndex);
                },
            });
        }
    };
});

app.controller('manageDepartmentController', function ($scope, $modal, convertExcelToGridService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';

    $scope.excelToGrid = function () {
        var parseOutput = convertExcelToGridService.convert2Grid($scope.excelText);
        $scope.departmentRawData = parseOutput.dataGrid;
        $scope.headerNames = parseOutput.headerNames;
    };

    var firstCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-left-radius: 6px;"';
    var lastCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-right-radius: 6px;"';
    var restCol = 'style="background-color: #383e4b;color: #fff;font-weight: bold;"';

    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>' +
                               '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>' +
                               '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>' +
                               '<div class="ngSortPriority">{{col.sortPriority}}</div>' +
                             '</div>' +
                             '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';

    var firstHeaderCellTemplate = '<div ' + firstCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;

    $scope.gridDepartments = {
        data: 'departmentRawData',
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEdit: true,
        showGroupPanel: false,
        showColumnMenu: true,
        enablePinning: true,
        enableColumnResize: true,
        showFilter: true,
        showFooter: true,
        filterOptions: { filterText: '', useExternalFilter: false },
        columnDefs: [{ field: 0, displayName: 'Department Code', headerCellTemplate: firstHeaderCellTemplate },
            { field: 1, displayName: 'Department Name', headerCellTemplate: lastHeaderCellTemplate }
        ]
    };
    
    function convertToDepartments(headerNames, dataArray) {
        var jsonDepartments = [];
        if (!dataArray) return jsonDepartments;
        for (var r = 0; r < dataArray.length; r++) {
            // for each row reset the variables
            var code = '';
            var name = '';

            if (dataArray[r]) {

                for (var i = 0; i < headerNames.length; i++) {
                    if ((dataArray[r][i])) {
                        switch (i) {
                            case 0:
                                code = dataArray[r][i];
                                break;
                            case 1:
                                name = dataArray[r][i];
                                break;
                        }
                    }
                }
                jsonDepartments.push({ 'Code': code, 'Name': name });
            }
        }
        return jsonDepartments;
    }

    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.departmentErrors;
                }
            }
        });
    };

    $scope.ManageDepartments = function () {
        $scope.departmentErrors = [];
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/CreateUpdateDepartments",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', departmentsList: " + JSON.stringify(convertToDepartments($scope.headerNames, $scope.departmentRawData)) + " }",
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                //alert('success');
                var $page = $(xml);
                //you can now use all the jQuery to conquer the world
                $page.find("ArrayOfProcessResult").each(function () {
                    var $data = $(this);
                    var $processResult = $data.find("ProcessResult");
                    var $stringList = $processResult.find('Message');
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.departmentErrors.push(result);
                    }
                    if ($scope.departmentErrors.length > 0)
                        alert('Errors Occured. Please click on View Results to learn more');
                    else alert('Successfully Created/Updated Departments in Fred Office.');
                });
            },
            error: function (msg) {
                alert('Errors Occured. Please click on View Results to learn more');
                $scope.departmentErrors = msg.responseText;
            }
        });
    };
});

app.controller('manageCategoryController', function ($scope, $modal, convertExcelToGridService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';

    $scope.excelToGrid = function () {
        var parseOutput = convertExcelToGridService.convert2Grid($scope.excelText);
        $scope.categoryRawData = parseOutput.dataGrid;
        $scope.headerNames = parseOutput.headerNames;
    };

    var firstCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-left-radius: 6px;"';
    var lastCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-right-radius: 6px;"';
    var restCol = 'style="background-color: #383e4b;color: #fff;font-weight: bold;"';

    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>' +
                               '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>' +
                               '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>' +
                               '<div class="ngSortPriority">{{col.sortPriority}}</div>' +
                             '</div>' +
                             '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';

    var firstHeaderCellTemplate = '<div ' + firstCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;

    $scope.gridCategories = {
        data: 'categoryRawData',
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEdit: true,
        showGroupPanel: false,
        showColumnMenu: true,
        enablePinning: true,
        enableColumnResize: true,
        showFilter: true,
        showFooter: true,
        filterOptions: { filterText: '', useExternalFilter: false },
        columnDefs: [{ field: 0, displayName: 'Department Code', headerCellTemplate: firstHeaderCellTemplate },
             { field: 1, displayName: 'Category Code', headerCellTemplate: headerCellTemplate },
            { field: 2, displayName: 'Category Name', headerCellTemplate: lastHeaderCellTemplate }
        ]
    };
    
    function convertToCategory(headerNames, dataArray) {
        var jsonCategory = [];
        if (!dataArray) return jsonCategory;
        for (var r = 0; r < dataArray.length; r++) {
            // for each row reset the variables
            var deptCode = '';
            var catCode = '';
            var catName = '';
            
            if (dataArray[r]) {

                for (var i = 0; i < headerNames.length; i++) {
                    if ((dataArray[r][i])) {
                        switch (i) {
                            case 0:
                                deptCode = dataArray[r][i];
                                break;
                            case 1:
                                catCode = dataArray[r][i];
                                break;
                            case 2:
                                catName = dataArray[r][i];
                                break;
                        }
                    }
                }
                jsonCategory.push({ 'DepartmentCode': deptCode, 'Code': catCode, 'Name': catName });
            }
        }
        return jsonCategory;
    }

    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.categoryErrors;
                }
            }
        });
    };

    $scope.ManageCategories = function () {
        $scope.categoryErrors = [];
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/CreateUpdateCategories",
            //url: "http://localhost:3460/IntegrationService.asmx/CreateUpdateCategories",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', categoriesList: " + JSON.stringify(convertToCategory($scope.headerNames, $scope.categoryRawData)) + " }",
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                //alert('success');
                var $page = $(xml);
                //you can now use all the jQuery to conquer the world
                $page.find("ArrayOfProcessResult").each(function () {
                    var $data = $(this);
                    var $processResult = $data.find("ProcessResult");
                    var $stringList = $processResult.find('Message');
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.categoryErrors.push(result);
                    }
                    if ($scope.categoryErrors.length > 0)
                        alert('Errors Occured. Please click on View Results to learn more');
                    else alert('Successfully Created/Updated Categories in Fred Office.');
                });
            },
            error: function (msg) {
                alert('Errors Occured. Please click on View Results to learn more');
                $scope.categoryErrors = msg.responseText;
            }
        });
    };
});

app.controller('offerDetailsController', function ($scope, $modal, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    var info = [];
    $scope.offerErrors = [];
    $scope.offerCodes = info;
    
    $scope.filterOptions = {
        filterText: ''
    };

    $scope.ApplyFilter = function () {
        $scope.filterOptions.filterText = $scope.query;
    };

    $scope.gridOffers = {
        data: 'offerEntries',
        showColumnMenu: true,
        enableColumnResize: true,
        enablePinning: true,
        filterOptions: $scope.filterOptions,
    };

    function resetData() {
        $scope.offerEntries = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 20;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.offerErrors = [];
    };
    
    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.offerErrors;
                }
            }
        });
    };

    $scope.PopulateInput = function () {
        resetData();

        stopwatchService.reset();
        stopwatchService.start();
        $scope.gridMessage = "Invoking the service. Please wait...";
        
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/GetOfferEntryDetails",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', offerCodes: [" + $scope.offerCodes + "] }",

            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                var CampaignName = '';
                var OfferCode = '';
                var EAN = '';
                var ProductName ='';
                var OfferPrice = 0;
                var RetailPrice = 0;
                var QuantityAvailable=0;
                var QuantityOnOrder=0;
                var Discontinued = false;
                var LastSoldDate ='';
                var CreatedDate= '';
                var Brand ='';
                var Name='';
                var Size ='';
                
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                var $page = $(xml);
                $page.find("ArrayOfOfferEntry ").each(function () {
                    var $data = $(this);
                    var $stringList = $data.find("OfferEntry");
                    
                    for (var index = 0; index < $stringList.length; index++) {
                        for (var i = 0; i < $stringList[index].childNodes.length; i++) {
                            switch(i) {
                                case 0:
                                    CampaignName = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 1:
                                    OfferCode = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 2:
                                    EAN = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 3:
                                    ProductName = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 4:
                                    OfferPrice = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 5:
                                    RetailPrice = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 6:
                                    QuantityAvailable = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 7:
                                    QuantityOnOrder = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 8:
                                    Discontinued = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 9:
                                    LastSoldDate = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 10:
                                    CreatedDate = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 11:
                                    Brand = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 12:
                                    Name = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 13:
                                    Size = $stringList[index].childNodes[i].textContent;
                                    break;
                            }
                        }
                        $scope.offerEntries.push({
                            'CampaignName': CampaignName,
                            'OfferCode': OfferCode,
                            'ProductName' : ProductName,
                            'EAN': EAN,
                            'OfferPrice': OfferPrice,
                            'RetailPrice': RetailPrice,
                            'QuantityAvailable': QuantityAvailable,
                            'QuantityOnOrder': QuantityOnOrder,
                            'Discontinued': Discontinued,
                            'LastSoldDate': LastSoldDate,
                            'CreatedDate': CreatedDate,
                            'Brand': Brand,
                            'Name': Name,
                            'Size': Size,
                        });
                    }
                });
                //or $scope.productIDs = xml.getElementsByTagName("string")[0].textContent;
                //$scope.groupToPages();
              //  $scope.gridMessage = 'No Records Found';
                $scope.$apply();
            },
            error: function (msg) {
                //var $errorMsg = $.parseJSON(msg.responseText);
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                $scope.offerErrors = 'Error Occured: ' + msg.responseText;
                alert('Errors Occured. Please click on View Results to learn more');
            }
        });
    };
});

app.controller('campaignHeaderController', function ($scope, $modal, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    $scope.campaignErrors = [];

    $scope.filterOptions = {
        filterText: ''
    };

    $scope.ApplyFilter = function () {
        $scope.filterOptions.filterText = $scope.query;
    };
    
    $scope.gridCampaigns = {
        data: 'campaignHeaders',
        showColumnMenu: true,
        enableColumnResize: true,
        enablePinning: true,
        filterOptions: $scope.filterOptions
    };

  
    function resetData() {
        $scope.campaignHeaders = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 20;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.campaignErrors = [];
    };

    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.campaignErrors;
                }
            }
        });
    };

    $scope.PopulateInput = function () {
        resetData();

        stopwatchService.reset();
        stopwatchService.start();
        $scope.gridMessage = "Invoking the service. Please wait...";

        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/GetActiveCampaignOfferHeaders",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "'}",

            dataType: "xml", // datatype is the output expected type
            success: function (xml) {

                var fields = new Array();
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                var $page = $(xml);
                $page.find("ArrayOfCampaignOfferHeader ").each(function () {
                    var $data = $(this);
                    var $stringList = $data.find("CampaignOfferHeader");

                    for (var index = 0; index < $stringList.length; index++) {
                        for (var i = 0; i < $stringList[index].childNodes.length; i++) {
                            fields[i] = $stringList[index].childNodes[i].textContent;
                        }
                        
                        $scope.campaignHeaders.push({
                            'CampaignName': fields[0],
                            'OfferCode': fields[1],
                            'OfferName': fields[2],
                            'OfferDescription': fields[3],
                            'OfferType': fields[4],
                            'IsLoyalty': fields[5],
                            'StartDate': fields[6],
                            'EndDate': fields[7],
                            'DollarOffDisc': fields[8],
                            'PercentOffDisc': fields[9],
                            'DollarThreshold': fields[10],
                            'MultiBuyXQty': fields[11],
                            'MultiBuyYQty': fields[12],
                            'MultiBuyXDollarAmt': fields[13],
                            'MultiBuyYDollarAmt': fields[14],
                            'QuantityThreshold': fields[15],
                            'DivideDiscount': fields[16]
                        });
                        fields = new Array();
                    }
                });
                //or $scope.productIDs = xml.getElementsByTagName("string")[0].textContent;
                //$scope.groupToPages();
                //  $scope.gridMessage = 'No Records Found';
                $scope.$apply();
            },
            error: function (msg) {
                //var $errorMsg = $.parseJSON(msg.responseText);
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                $scope.campaignErrors = 'Error Occured: ' + msg.responseText;
                alert('Errors Occured. Please click on View Results to learn more');
            }
        });

    };
});


app.controller('getProductPricingController', function($scope, $modal, stopwatchService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    $scope.itemHQIDs = [];
    $scope.pricingDetails = [];
    
    $scope.filterOptions = {
        filterText: ''
    };

    $scope.ApplyFilter = function () {
        $scope.filterOptions.filterText = $scope.query;
    };
    
    $scope.gridPricingDetails = {
        data: 'pricingDetails',
        showColumnMenu: true,
        enableColumnResize: true,
        enablePinning: true,
        allowColumnReorder: true,
        filterOptions: $scope.filterOptions
    };
    
    function resetData() {
        $scope.pricingDetails = [];
        $scope.getPricingError = [];
        stopwatchService.reset();
       
    };

    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.getPricingError;
                }
            }
        });
    };
    
    $scope.PopulateInput = function () {
        resetData();
        
        stopwatchService.start();
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/GetProductPricingDetails",
            //url: "http://localhost:3460/IntegrationService.asmx/GetProductPricingDetails",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', productHQIDs: [" + $scope.itemHQIDs + "] }",

            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                var productHqid = '';
                var description = '';
                var brand = '';
                var name = '';
                var size = '';
                var itemLocation = '';
                var currentPricingPolicy = '';
                var lastPurchaseCost = 0.00;
                var currentPrice = 0.00;
                var stockOnHand = 0.0;
                var inActive = false;
                var lastPurchaseDate = '';
                var lastSoldDate = '';
                var createdDate = '';
                var daysToNextPromotion = 0;
                var currentSalePrice = 0.00;
                var isOnCurrentPromotion = false;
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                var $page = $(xml);
                $page.find("ArrayOfProductPricing ").each(function () {
                    var $data = $(this);
                    var $stringList = $data.find("ProductPricing");

                    for (var index = 0; index < $stringList.length; index++) {
                        for (var i = 0; i < $stringList[index].childNodes.length; i++) {
                            switch (i) {
                                case 0:
                                    productHqid = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 1:
                                    description = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 2:
                                    brand = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 3:
                                    name = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 4:
                                    size = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 5:
                                    itemLocation = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 6:
                                    currentPricingPolicy = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 7:
                                    lastPurchaseCost = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 8:
                                    currentPrice = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 9:
                                    stockOnHand = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 10:
                                    inActive = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 11:
                                    lastPurchaseDate = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 12:
                                    lastSoldDate = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 13:
                                    createdDate = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 14:
                                    daysToNextPromotion = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 15:
                                    currentSalePrice = $stringList[index].childNodes[i].textContent;
                                    break;
                                case 16:
                                    isOnCurrentPromotion = $stringList[index].childNodes[i].textContent;
                                    break;
                            }
                        }
                        
                        /* <?xml version="1.0" encoding="utf-8"?><ArrayOfProductPricing xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
             <ProductPricing>
                 <ProductHQID>196</ProductHQID>
                 <Description>PANADOL CAPLETS 24</Description>
                 <Brand /><Name /><Size /><ItemLocation /><CurrentPricingPolicy />
                 <LastPurchaseCost>0.00</LastPurchaseCost>
                 <CurrentPrice>0.00</CurrentPrice>
                 <StockOnHand>0</StockOnHand>
                 <>false</InActive>
                 <>1800-01-01T00:00:00</LastPurchaseDate>
                 <>1800-01-01T00:00:00</LastSoldDate>
                 <>2013-09-02T11:00:12.767</CreatedDate>
                 <>0</DaysToNextPromotion>
                 <>0.0000000000</CurrentSalePrice>
                 <>false</IsOnCurrentPromotion>
             </ProductPricing>
             </ArrayOfProductPricing>*/
                        $scope.pricingDetails.push({
                            'ProductHQID': productHqid,
                            'Description': description,
                            'Brand': brand,
                            'Name': name,
                            'Size': size,
                            'ItemLocation': itemLocation,
                            'PricingPolicy': currentPricingPolicy,
                            'LastPurchaseCost': lastPurchaseCost,
                            'Price': currentPrice,
                            'SOH': stockOnHand,
                            'InActive': inActive,
                            'LastPurchaseDate': lastPurchaseDate,
                            'LastSoldDate': lastSoldDate,
                            'CreatedDate': createdDate,
                            'DaysToNextPromotion': daysToNextPromotion,
                            'SalePrice': currentSalePrice,
                            'IsOnPromotion': isOnCurrentPromotion
                        });
                    }
                });
                $scope.$apply();
            },
            error: function (msg) {
                //var $errorMsg = $.parseJSON(msg.responseText);
                stopwatchService.stop();
                $scope.timetaken = stopwatchService.data.value / 10;
                $scope.getPricingError= 'Error Occured: ' + msg.responseText;
                alert('Errors Occured. Please click on View Results to learn more');
            }
        });
    };
});

app.controller('setProductPricingController', function ($scope, $modal, stopwatchService, convertExcelToGridService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    
    $scope.excelToGrid = function () {
        var parseOutput = convertExcelToGridService.convert2Grid($scope.excelText);
        $scope.pricingDetails = parseOutput.dataGrid;
        $scope.headerNames = parseOutput.headerNames;
    };


    $scope.filterOptions = {
        filterText: ''
    };

    $scope.ApplyFilter = function () {
        $scope.filterOptions.filterText = $scope.query;
    };
    
    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>' +
                              '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>' +
                              '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>' +
                              '<div class="ngSortPriority">{{col.sortPriority}}</div>' +
                            '</div>' +
                            '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';
    var firstCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-left-radius: 6px;"';
    var lastCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-right-radius: 6px;"';
    var restCol = 'style="background-color: #383e4b;color: #fff;font-weight: bold;"';
    
    var firstHeaderCellTemplate = '<div ' + firstCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    
    $scope.gridPricingDetails = {
        data: 'pricingDetails',
        showColumnMenu: true,
        enableColumnResize: true,
        enablePinning: true,
        filterOptions: $scope.filterOptions,
        columnDefs: [{ field: 0, displayName: 'Product HQID', headerCellTemplate: firstHeaderCellTemplate },
          { field: 1, displayName: 'PP Override', headerCellTemplate: headerCellTemplate },
           { field: 2, displayName: 'New Price', headerCellTemplate: headerCellTemplate },
         { field: 3, displayName: 'Remove From PPlan', headerCellTemplate: lastHeaderCellTemplate }
        ]
    };
    
    function convertToNewProductPricing(headerNames, dataArray) {
        var jsonNewPricing = [];
        if (!dataArray) return jsonNewPricing;
        for (var r = 0; r < dataArray.length; r++) {
            // for each row reset the variables
            var ProductHQID = '';
            var NewPrice = '';
            var OverridingPricingPolicy = '';
            var RemoveFromPricingPlan = false;
            
            if (dataArray[r]) {

                for (var i = 0; i < headerNames.length; i++) {
                    if ((dataArray[r][i])) {
                        switch (i) {
                            case 0:
                                ProductHQID = dataArray[r][i];
                                break;
                            case 1:
                                OverridingPricingPolicy = dataArray[r][i];
                                break;
                            case 2:
                                NewPrice = dataArray[r][i];
                                break;
                            case 3:
                                RemoveFromPricingPlan = dataArray[r][i];
                                break;
                        }
                    }
                }
                jsonNewPricing.push({ 'ProductHQID': ProductHQID, 'OverridingPricingPolicy': OverridingPricingPolicy, 'NewPrice': NewPrice, 'RemoveFromPricingPlan': RemoveFromPricingPlan });
            }
        }
        return jsonNewPricing;
    }

    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.setPricingErrors;
                }
            }
        });
    };

    $scope.ManageSetPricing = function () {
        $scope.setPricingErrors = [];
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/SetProductPricingDetails",
            //url: "http://localhost:3460/IntegrationService.asmx/SetProductPricingDetails",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', productPricingDetails: " + JSON.stringify(convertToNewProductPricing($scope.headerNames, $scope.pricingDetails)) + " }",
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                //alert('success');
                var $page = $(xml);
                //you can now use all the jQuery to conquer the world
                $page.find("ArrayOfProcessResult").each(function () {
                    var $data = $(this);
                    var $processResult = $data.find("ProcessResult");
                    var $stringList = $processResult.find('Message');
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.setPricingErrors.push(result);
                    }
                    if ($scope.setPricingErrors.length > 0)
                        alert('Errors Occured. Please click on View Results to learn more');
                    else alert('Successfully Updated Pricing Details in Fred Office.');
                });
            },
            error: function (msg) {
                alert('Errors Occured. Please click on View Results to learn more');
                $scope.setPricingErrors = msg.responseText;
            }
        });
    };

});

app.controller('queueLabelController', function($scope, $modal, stopwatchService, convertExcelToGridService) {
    $scope.webUser = 'Web';
    $scope.webPwd = 'PO2005';
    $scope.gridMessage = 'No Records Found';
    $scope.setLabelErrors = [];
    
    $scope.filterOptions = {
        filterText: ''
    };

    $scope.ApplyFilter = function () {
        $scope.filterOptions.filterText = $scope.query;
    };
    
    var colTemplate = '<div ng-click="col.sort($event)" ng-class="colt + col.index" class="ngHeaderText">{{col.displayName}}</div>' +
                              '<div class="ngSortButtonDown" ng-show="col.showSortButtonDown()"></div>' +
                              '<div class="ngSortButtonUp" ng-show="col.showSortButtonUp()"></div>' +
                              '<div class="ngSortPriority">{{col.sortPriority}}</div>' +
                            '</div>' +
                            '<div ng-show="col.resizable" class="ngHeaderGrip" ng-click="col.gripClick($event)" ng-mousedown="col.gripOnMouseDown($event)"></div>';
    var firstCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-left-radius: 6px;"';
    var lastCol = 'style = "background-color: #383e4b;color: #fff;font-weight: bold;border-top-right-radius: 6px;"';
    var restCol = 'style="background-color: #383e4b;color: #fff;font-weight: bold;"';

    var firstHeaderCellTemplate = '<div ' + firstCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var lastHeaderCellTemplate = '<div ' + lastCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;
    var headerCellTemplate = '<div ' + restCol + ' ng-style="{cursor: col.cursor}" ng-class="{ ngSorted: !noSortVisible }">' + colTemplate;

    $scope.gridLabels = {
        data: 'labelQueue',
        showColumnMenu: true,
        enableColumnResize: true,
        enablePinning: true,
        filterOptions: $scope.filterOptions,
        columnDefs: [{ field: 0, displayName: 'Product HQID', headerCellTemplate: firstHeaderCellTemplate },
            { field: 1, displayName: 'Description', headerCellTemplate: headerCellTemplate },
            { field: 2, displayName: 'Quantity', headerCellTemplate: headerCellTemplate },
            { field: 3, displayName: 'Price', headerCellTemplate: headerCellTemplate },
            { field: 4, displayName: 'Effective Date', headerCellTemplate: headerCellTemplate },
            { field: 5, displayName: 'Dual Location', headerCellTemplate: lastHeaderCellTemplate }
        ]
    };
    
    $scope.excelToGrid = function () {
        var parseOutput = convertExcelToGridService.convert2Grid($scope.excelText);
        $scope.labelQueue = parseOutput.dataGrid;
        $scope.headerNames = parseOutput.headerNames;
    };


    $scope.open = function () {
        $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.setLabelErrors;
                }
            }
        });
    };
    
    function convertToLabelQueue(headerNames, dataArray) {
        var jsonNewPricing = [];
        if (!dataArray) return jsonNewPricing;
        for (var r = 0; r < dataArray.length; r++) {
            // for each row reset the variables
            var ProductHQID = '';
            var Description = '';
            var Quantity = '';
            var Price = '';
            var EffectiveDate = '';
            var DualLocation = false;

            if (dataArray[r]) {

                for (var i = 0; i < headerNames.length; i++) {
                    if ((dataArray[r][i])) {
                        switch (i) {
                            case 0:
                                ProductHQID = dataArray[r][i];
                                break;
                            case 1:
                                Description = dataArray[r][i];
                                break;
                            case 2:
                                Quantity = dataArray[r][i];
                                break;
                            case 3:
                                Price= dataArray[r][i];
                                break;
                            case 4:
                                EffectiveDate = dataArray[r][i];
                                break;
                            case 5:
                                DualLocation = dataArray[r][i];
                                break;
                        }
                    }
                }
                jsonNewPricing.push({ 'ProductHQID': ProductHQID, 'Description': Description, 'LabelQuantity': Quantity, 'LabelPrice': Price, 'EffectiveDate': EffectiveDate, 'IsDualLocation': DualLocation });
            }
        }
        return jsonNewPricing;
    }
    
    $scope.ManageLabelQueue = function () {
        $scope.setPricingErrors = [];
        $.ajax({
            type: "POST",
            url: masterServiceAddress + "/QueueLabels",
            //url: "http://localhost:3460/IntegrationService.asmx/QueueLabels",
            contentType: "application/json; charset=utf-8", // contenttype is the input type
            data: "{ userName : '" + $scope.webUser + "', password: '" + $scope.webPwd + "', labelQueue: " + JSON.stringify(convertToLabelQueue($scope.headerNames, $scope.labelQueue)) + " }",
            dataType: "xml", // datatype is the output expected type
            success: function (xml) {
                var $page = $(xml);
                $page.find("ArrayOfProcessResult").each(function () {
                    var $data = $(this);
                    var $processResult = $data.find("ProcessResult");
                    var $stringList = $processResult.find('Message');
                    for (var index = 0; index < $stringList.length; index++) {
                        var result = $stringList[index].textContent;
                        $scope.setLabelErrors.push(result);
                    }
                    if ($scope.setLabelErrors.length > 0)
                        alert('Errors Occured. Please click on View Results to learn more');
                    else alert('Successfully Created/Updated Label Queue in Fred Office.');
                });
            },
            error: function (msg) {
                alert('Errors Occured. Please click on View Results to learn more');
                $scope.setLabelErrors.push(msg.responseText);
            }
        });
    };
});

