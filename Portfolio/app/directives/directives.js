app.directive('container', function () {

    return {
        restrict: 'A',
        scope: {project:'='},
        replace: true,
        controller: 'indexController',
        template: '<div class="views projects">' +
          '    <div style="display: inline-block;" ng-repeat="project in views" project="project">' +
          '        <div  class="project-view" view="project-view" ></div>' +
          '    </div>' +
          '</div>',

        link: function (scope, elm) {

          
        }
    }
});

app.directive('projectView', function () {
    return {
        restrict: 'A',
        scope: {},
        replace: true,
        template: '<div class="project-container">' +
                    '       <div class="project-shot">' +
                    '           <a href="#"><img src="{{$parent.$parent.project.thumbnail}}"/></a>' +
                    '       </div>' +
                    '       <div class="project-description">' +
                    '       <div class="name">{{$parent.$parent.project.name}}</div>' +
                    '           <div class="desc">{{$parent.$parent.project.description}}</div>' +
                    '    <div class="actions">' +
                    '        <a class="btn btn-mini btn-inverse" href="#/project/:{{$parent.$parent.project.id}}">' +
                    '        View Details' +
                    '        </a>' +
                    '    </div>' +
                    '</div>' +
                    '<div class="project-stats">' +
                    '    <span class="views">' +
                    '        <a href="#" rel="tooltip" title="Project views" class="tip"><i class="icon-eye-open"></i></a> {{$parent.$parent.project.views}}' +
                    '    </span>' +
                    '    <span class="likes">' +
                    '        <a href="#" rel="tooltip" title="Project likes" class="tip"><i class="icon-heart"></i></a> {{$parent.$parent.project.likes}}' +
                    '    </span>' +
                    '</div>' +
                '</div>'
    }
});

app.directive('myDirTwo', function () {
    return {
        restrict: 'A',
        scope: {},
        replace: true,
        template: '<div>This is directive two.</div>'
    }
});

app.directive('myDirThree', function () {
    return {
        restrict: 'A',
        scope: {},
        replace: true,
        template: '<div>This is directive three.</div>'
    }
});

app.directive('view', ['$compile', function (compile) {

    return {
        restrict: 'A',
        scope: {
            view: '@'
        },
        replace: true,
        template: '<div class="view"></div>',

        controller: ['$scope', function (scope) {
            scope.$watch('view', function (value) {
                scope.buildView(value);
            });
        }],

        link: function (scope, elm, attrs) {

            scope.buildView = function (viewName) {
                var view = compile('<div ' + viewName + '></div>')(scope);
                elm.append(view);
            }
        }
    }
}]);