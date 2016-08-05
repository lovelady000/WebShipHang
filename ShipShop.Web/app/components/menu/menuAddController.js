(function (app) {
    app.controller('menuAddController', menuAddController);
    menuAddController.$inject = ['$scope','apiService','$state'];

    function menuAddController($scope, apiService, $state) {
        $scope.newMenu = {
            Status:true,
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
            apiService.post('api/menu/create', $scope.newMenu, function (result) {
                console.log(result.data.ID);
                $state.go('menu');
            }, function (error) {
                console.log(error);
            });
        };

    };
})(angular.module('onlineshop.menu'));