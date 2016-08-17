/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('newsListController', newsListController);

    newsListController.$inject = ['$scope', 'apiService', 'notificationService', 'popupService', '$ngBootbox', '$state'];

    function newsListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

        $scope.listNews = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getNews = getNews;

        function getNews(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/news/getall', config, function (result) {
                $scope.listNews = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                //notificationService.displaySuccess('Thành công');
            }, function () {
                console.log('error');
            });
        };
        $scope.getNews();

        $scope.Add = Add;
        function Add() {
            popupService.open('', '/app/components/news/newsAddView.html', 'newsAddController', '');
        };
        $scope.Edit = Edit;
        function Edit(id) {
            var params = {
                objectID: id,
            };
            popupService.open('', '/app/components/news/newsEditView.html', 'newsEditController', params);
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
                   apiService.del('/api/news/delete', config, function () {
                       notificationService.displaySuccess('Đã xóa thành công.');
                       $state.reload();
                   },
                   function () {
                       notificationService.displayError('Xóa không thành công.');
                   });
               });
        };

    };

})(angular.module('onlineshop.news'));