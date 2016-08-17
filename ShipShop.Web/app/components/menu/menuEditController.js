(function (app) {
    app.controller('menuEditController', menuEditController);
    menuEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance'];

    function menuEditController($scope, apiService, $state, params, $uibModalInstance) {
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
            apiService.get('api/menu/getbyid/' + params.objectID, null, function (result) {
                $scope.newMenu = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailMenu();

        $scope.UpdateMenu = UpdateMenu;
        function UpdateMenu() {
            apiService.put('api/menu/update', $scope.newMenu, function (result) {
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

    };
})(angular.module('onlineshop.menu'));