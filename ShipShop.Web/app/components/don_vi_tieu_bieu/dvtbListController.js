/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('dvtbListController', dvtbListController);

    dvtbListController.$inject = ['$scope', 'apiService', 'notificationService', 'popupService', '$ngBootbox', '$state'];

    function dvtbListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

        $scope.listDVTB = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getDVTB = getDVTB;

        function getDVTB(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/dvtb/getall', config, function (result) {
                $scope.listDVTB = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('error');
            });
        };
        $scope.getDVTB();

        $scope.Add = Add;
        function Add() {
            popupService.open('', '/app/components/don_vi_tieu_bieu/dvtbAddView.html', 'dvtbAddController', '');
        };
        $scope.Edit = Edit;
        function Edit(id) {
            var params = {
                objectID: id,
            };
            popupService.open('', '/app/components/don_vi_tieu_bieu/dvtbEditView.html', 'dvtbEditController', params);
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
                   apiService.del('/api/dvtb/delete', config, function () {
                       notificationService.displaySuccess('Đã xóa thành công.');
                       $state.reload();
                   },
                   function () {
                       notificationService.displayError('Xóa không thành công.');
                   });
               });
        };

    };

})(angular.module('onlineshop.dvtb'));