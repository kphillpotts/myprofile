var app = angular.module('WebClientApp', ['ngGrid', 'ui.bootstrap']);

app.config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/linkedproduct',
            {
                controller: 'linkedProductController',
                templateUrl: './app/partials/linkedproduct.html'
            })
        .when('/linkedsupplier',
            {
                controller: 'linkedSupplierController',
                templateUrl: './app/partials/linkedsupplier.html'
            })
        .when('/managesupplieritems',
            {
                controller: 'manageSupplierItemController',
                templateUrl: './app/partials/managesupplieritems.html'
            })
      .when('/manageproducts',
            {
                controller: 'manageProductsController',
                templateUrl: './app/partials/manageproducts.html'
            })
        .when('/managedepartments',
            {
                controller: 'manageDepartmentController',
                templateUrl: './app/partials/managedepartments.html'
            })
        .when('/managecategories',
            {
                controller: 'manageCategoryController',
                templateUrl: './app/partials/managecategories.html'
            })
     .when('/offerdetails',
            {
                controller: 'offerDetailsController',
                templateUrl: './app/partials/offerdetails.html'
            })
    .when('/getpricing',
        {
            controller: 'getProductPricingController',
            templateUrl: './app/partials/getpricing.html'
        })
    .when('/setpricing',
        {
            controller: 'setProductPricingController',
            templateUrl: './app/partials/setpricing.html'
        })
     .when('/campaignheaders',
            {
                controller: 'campaignHeaderController',
                templateUrl: './app/partials/campaignheaders.html'
            })
    .when('/queuelabel',
        {
            controller: 'queueLabelController',
            templateUrl: './app/partials/queuelabel.html'
        })
        .when('/intro',
            {
                controller: 'introController',
                templateUrl: './app/partials/intro.html'
            })
        .otherwise({ redirectTo: '/intro' });
});

