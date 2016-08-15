/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('applicationGroupListController', applicationGroupListController);

    applicationGroupListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function applicationGroupListController($scope, apiService, notificationService) {

        $scope.listGroup = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getGroup = getGroup;
        $scope.search = search;


        function search() {
            getGroup();
        }

        function getGroup(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/applicationGroup/getlistpaging', config, function (result) {
                $scope.listGroup = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                notificationService.displaySuccess('Thành công');
            }, function () {
                console.log('error');
            });
        };

        $scope.getGroup();
    };
    
})(angular.module('onlineshop.application_groups'));