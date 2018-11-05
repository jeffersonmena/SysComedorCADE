var app = angular.module('myApp2', []);

app.controller('namesCtrl', function ($scope) {
    $scope.names = [
         { name: 'Jani', country: 'Norway' },
         { name: 'Hege', country: 'Sweden' },
         { name: 'Kai', country: 'Denmark' }
    ];
});