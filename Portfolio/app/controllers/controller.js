app.controller('indexController', function ($rootScope, $scope) {
    $scope.views = [
      {
          id: 1,
          name: 'The Cubby',
          url: 'http://thecubby.azurewebsites.net',
          thumbnail: 'images/projects/previews/thecubby.png',
          image:'images/projects/details/thecubby.png',
          description: 'blah...blah...blah.... The cubby',
          technology: 'a,b,c',
          views: 56,
          likes: 24
      },
      {
          id: 2,
          name: 'Back Office Client',
          url: 'http://vsbackoffice.azurewebsites.net',
          thumbnail: 'images/projects/previews/webclient.png',
          image: 'images/projects/details/webclient.png',
          description: 'blah...blah...blah....the web client',
          technology: 'a,b,c',
          views: 34,
          likes: 10
      },
      {
          id: 3,
          name: 'Back Office Service',
          url: 'http://vsbackoffice.azurewebsites.net/backofficeservice.asmx',
          thumbnail: 'images/projects/previews/webservice.png',
          image: 'images/projects/details/webservice.png',
          description: 'blah...blah...blah.... the service',
          technology: 'a,b,c',
          views: 48,
          likes: 34
      },
      {
          id: 4,
          name: 'Tasty Foods',
          url: 'http://tastyfoods.azurewebsites.net',
          thumbnail: 'images/projects/previews/tastyfoods.png',
          image: 'images/projects/details/tastyfoods.png',
          description: 'blah...blah...blah.... Tasty foods',
          technology: 'a,b,c',
          views: 22,
          likes: 04
      },
      {
          id: 5,
          name: 'Time Sheet App',
          url: 'http://myprofile.azurewebsites.net/ComingSoon.html?siteName=TimeSheet_App',
          thumbnail: 'images/projects/previews/timesheet.png',
          image: 'images/projects/details/timesheet.png',
          description: 'blah...blah...blah.... Time sheet app',
          technology: 'a,b,c',
          views: 112,
          likes: 94
      },
      {
          id: 6,
          name: 'Waiting Scripts',
          url: 'http://myprofile.azurewebsites.net/ComingSoon.html?siteName=Waiting_Scripts',
          thumbnail: 'images/projects/previews/waitingscripts.png',
          image: 'images/projects/details/waitingscripts.png',
          description: 'blah...blah...blah.... waiting scripts',
          technology: 'a,b,c',
          views: 112,
          likes: 94
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