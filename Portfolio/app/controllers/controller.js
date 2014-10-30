app.controller('indexController', function ($rootScope, $scope) {
    $scope.views = [
      {
          id: 1,
          name: 'The Cubby',
          url: 'http://thecubby.azurewebsites.net',
          thumbnail: 'images/projects/previews/thecubby.png',
          image:'images/projects/details/thecubby.png',
          description: 'The cubby is a very casual web site built for a Cafe style business.',
          technology: 'ASP.Net MVC 5, Custom CMS technique',
          views: 56,
          likes: 24
      },
      {
          id: 2,
          name: 'Back Office Client',
          url: 'http://vsbackoffice.azurewebsites.net',
          thumbnail: 'images/projects/previews/webclient.png',
          image: 'images/projects/details/webclient.png',
          description: 'If you are running your business and would like to manage the business entities the back office client is a perfect app for you.' +
              'Allows Inventory Management, Product Pricing Management, Promotion Management, Labelling etc. ',
          technology: 'AngularJS, AngularJS UI (Grid), jQuery, Bootstrap CSS, WSDL, json',
          views: 34,
          likes: 10
      },
      {
          id: 3,
          name: 'Back Office Service',
          url: 'http://vsbackoffice.azurewebsites.net/backofficeservice.asmx',
          thumbnail: 'images/projects/previews/webservice.png',
          image: 'images/projects/details/webservice.png',
          description: 'The BackOffice service is the web service that feeds the clients with the business management.' +
          'The service is responsible for interacting with the Data Layer and stands out as an example for Business Logic Layer',
          technology: 'WSDL, Json, XML',
          views: 48,
          likes: 34
      },
      {
          id: 4,
          name: 'Tasty Foods',
          url: 'http://tastyfoods.azurewebsites.net',
          thumbnail: 'images/projects/previews/tastyfoods.png',
          image: 'images/projects/details/tastyfoods.png',
          description: 'Tasty foods is a real life website implemented (of course with a different name and is intended to portrait a type of Cuisine in great detail.'+
          'The best part of this is the SEO techniques to make the website stand out as top results during a web search',
          technology: 'ASP.NET MVC 4, jQuery, json',
          views: 22,
          likes: 14
      },
      {
          id: 5,
          name: 'Point O Sale',
          url: 'http://vspos.azurewebsites.net/main.html',
          thumbnail: 'images/projects/previews/timesheet.png',
          image: 'images/projects/details/timesheet.png',
          description: 'Point of Sale - Angular JS',
          technology: 'Service Stack (REST), AngularJS, jQuery, Bootstrap CSS',
          views: 10,
          likes: 8
      },
      {
          id: 6,
          name: 'Chat Away...',
          url: 'http://laughatme.azurewebsites.net',
          thumbnail: 'images/projects/previews/waitingscripts.png',
          image: 'images/projects/details/waitingscripts.png',
          description: 'Chat Away is a SignalR based web application used as a chat client. This is still in progress....',
          technology: 'ASP.NET, SignalR, jQuery',
          views: 18,
          likes: 15
      }
    ];
    $rootScope.views = $scope.views;
});

app.controller('projectController', function ($scope, $rootScope, $routeParams) {
    $scope.project_id = parseInt($routeParams.projectId.replace(":","")) - 1;
    var project = $rootScope.views[$scope.project_id];
    $scope.projectName = project.name;
    $scope.projectUrl = project.url;
    $scope.projectDesc = project.description;
    $scope.projectImage = project.image;
    $scope.projectTech = project.technology;
});

app.filter('Range', function () {
    return function (input) {
        var lowBound, highBound;
        switch (input.length) {
            case 1:
                lowBound = 0;
                highBound = parseInt(input[0]) - 1;
                break;
            case 2:
                lowBound = parseInt(input[0]);
                highBound = parseInt(input[1]);
                break;
            default:
                return input;
        }
        var result = [];
        for (var i = lowBound; i <= highBound; i++)
            result.push(i);
        return result;
    };
});