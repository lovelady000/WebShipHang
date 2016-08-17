(function (app) {
    app.controller('menuAddController', menuAddController);
    menuAddController.$inject = ['$scope', 'apiService', '$state','$uibModalInstance'];

    function menuAddController($scope, apiService, $state,$uibModalInstance) {
        $scope.newMenu = {
            Status: true,
        };
        $scope.menuGroup = [];

        function loadMenuGroup() {
            apiService.get('api/menu/getallgroup', null, function (result) {
                $scope.menuGroup = result.data;
            }, function () {
                console.log("service error");
            });
        }
        loadMenuGroup();
        $scope.AddMenu = AddMenu;
        function AddMenu() {
            console.log($scope.newMenu);
            apiService.post('api/menu/create', $scope.newMenu, function (result) {
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