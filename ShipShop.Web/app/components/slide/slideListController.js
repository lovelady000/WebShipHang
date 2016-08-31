/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService', 'popupService', '$ngBootbox', '$state'];

    function slideListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

        $scope.listSlide = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getSlide = getSlide;

        function getSlide(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/slide/getall', config, function (result) {
                $scope.listSlide = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('error');
            });
        };
        $scope.getSlide();

        $scope.Add = Add;
        function Add() {
            popupService.open('', '/app/components/slide/slideAddView.html', 'slideAddController', '');
        };
        $scope.Edit = Edit;
        function Edit(id) {
            var params = {
                objectID: id,
            };
            popupService.open('', '/app/components/slide/slideEditView.html', 'slideEditController', params);
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
                   apiService.del('/api/slide/delete', config, function () {
                       notificationService.displaySuccess('Đã xóa thành công.');
                       $state.reload();
                   },
                   function () {
                       notificationService.displayError('Xóa không thành công.');
                   });
               });
        };

    };

})(angular.module('onlineshop.slide'));