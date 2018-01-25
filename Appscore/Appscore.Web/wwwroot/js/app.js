var app = angular.module('Appscore', []);

app.directive("displaySearchResults", function ($parse) {
    return {
        restrict: 'EA',
        scope: {            
            results: '='
        },
        template: '<div>\
            <table border="1" ng-if="results.length > 0" style="width:100%">\
                <thead>\
                    <tr style="background-color:olive; color:white">\
                        <td>ID</td>\
                        <td>NAME</td>\
                        <td>GENDER</td>\
                        <td>BIRTHPLACE</td>\
                    </tr>\
                </thead>\
                <tbody>\
                    <tr ng-repeat="r in results">\
                        <td>{{ r.id }}</td>\
                        <td>{{ r.name }}</td>\
                        <td>{{ r.gender == 2 ? \'Female\' : \'Male\' }}</td>\
                        <td>{{ r.birthPlace }}</td>\
                    </tr>\
                </tbody>\
            </table>\
        </div>',
        link: function (scope, element, attrs) {
        }
    };
});