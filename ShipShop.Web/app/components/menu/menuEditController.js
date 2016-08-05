(function (app) {
    app.controller('menuEditController', menuEditController);
    menuEditController.$inject = ['$scope', 'apiService', '$state', '$stateParams'];

    function menuEditController($scope, apiService, $state, $stateParams) {
        $scope.newMenu = {};

        $scope.menuGroup = [];

        function loadMenuGroup() {
            apiService.get('api/menu/getallgroup', null, function (result) {
                $scope.menuGroup = result.data;
            }, function () {
                console.log("service error");
            });
        }

        loadMenuGroup();

        function GetDetailMenu() {
            apiService.get('api/menu/getbyid/' + $stateParams.id,null, function (result) {
                $scope.newMenu = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailMenu();

        $scope.UpdateMenu = UpdateMenu;
        function UpdateMenu() {
            apiService.put('api/menu/update', $scope.newMenu, function (result) {
                $state.go('menu');
            }, function (error) {
                console.log(error);
            });
        };
       

    };
})(angular.module('onlineshop.menu'));