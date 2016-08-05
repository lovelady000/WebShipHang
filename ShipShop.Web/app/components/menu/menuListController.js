/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('menuListController', productListController);

    productListController.$inject = ['$scope', 'apiService','notificationService'];

    function productListController($scope, apiService, notificationService) {

        $scope.menu = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getMenu = getMenu;
        $scope.search = search;


        function search() {
            getMenu();
        }

        function getMenu(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/menu/getall', config, function (result) {
                $scope.menu = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                notificationService.displaySuccess('Thành công');
            }, function () {
                console.log('error');
            });
        };

        $scope.getMenu();
    };
    
})(angular.module('onlineshop.menu'));