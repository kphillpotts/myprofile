var app = angular.module('PortFolioApp', []);

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/project/:projectId',
            {
                controller: 'projectController',
                templateUrl: './app/partials/project.html'
            })
         .when('/index',
            {
                controller: 'indexController',
                templateUrl: './app/partials/index.html'
            }).otherwise({ redirectTo: '/index' });
    //// use the HTML5 History API
    //$locationProvider.html5Mode(true);
});


