﻿/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('menuListController', productListController);

    productListController.$inject = ['$scope', 'apiService','notificationService','popupService', '$ngBootbox', '$state'];

    function productListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

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
                //notificationService.displaySuccess('Thành công');
            }, function () {
                console.log('error');
            });
        };
        $scope.getMenu();

        $scope.Add = Add;
        function Add() {
            popupService.open('', '/app/components/menu/menuAddView.html', 'menuAddController', '');
        };
        $scope.Edit = Edit;
        function Edit(id) {
            var params = {
                objectID: id,
            };
            popupService.open('', '/app/components/menu/menuEditView.html', 'menuEditController', params);
        };

        $scope.Delete = Delete;
        function Delete(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
               .then(function () {
                   var config = {
                       params: {
                           id: id
                       }
                   }
                   apiService.del('/api/menu/delete', config, function () {
                       notificationService.displaySuccess('Đã xóa thành công.');
                       $state.reload();
                   },
                   function () {
                       notificationService.displayError('Xóa không thành công.');
                   });
               });
        };
    };
    
})(angular.module('onlineshop.menu'));