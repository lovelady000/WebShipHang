/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('areaListController', areaListController);

    areaListController.$inject = ['$scope', 'apiService', 'notificationService', 'popupService', '$ngBootbox', '$state'];

    function areaListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

        $scope.listArea = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getArea = getArea;

        function getArea(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/area/getall', config, function (result) {
                $scope.listArea = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('error');
            });
        };
        $scope.getArea();

        $scope.Add = Add;
        function Add() {
            popupService.open('', '/app/components/area/areaAddView.html', 'areaAddController', '');
        };
        $scope.Edit = Edit;
        function Edit(id) {
            var params = {
                objectID: id,
            };
            popupService.open('', '/app/components/area/areaEditView.html', 'areaEditController', params);
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
                   apiService.del('/api/area/delete', config, function () {
                       notificationService.displaySuccess('Đã xóa thành công.');
                       $state.reload();
                   },
                   function () {
                       notificationService.displayError('Xóa không thành công.');
                   });
               });
        };

    };

})(angular.module('onlineshop.area'));