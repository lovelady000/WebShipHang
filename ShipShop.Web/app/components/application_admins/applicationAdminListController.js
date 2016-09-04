﻿(function (app) {
    'use strict';

    app.controller('applicationAdminListController', applicationAdminListController);

    applicationAdminListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', 'popupService'];

    function applicationAdminListController($scope, apiService, notificationService, $ngBootbox, popupService) {

        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;

        $scope.AddNewAccountAdmin = AddNewAccountAdmin;
        function AddNewAccountAdmin() {
            popupService.open('', '/app/components/application_admins/applicationAdminAddView.html', 'applicationAdminAddController', '');
        }

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/applicationUser/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                    function () {
                        notificationService.displayError('Xóa không thành công.');
                    });
                });
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterExpression
                }
            }

            apiService.get('/api/applicationAdmin/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' items found');
            }
        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }

        $scope.search();

        $scope.ResetPass = ResetPass;
        function ResetPass(id,userName) {
            var params = {
                objectID: id,
                userName: userName,
            };
            popupService.open('', '/app/components/application_admins/applicationAdminResetPassView.html', 'applicationAdminResetPassController', params);
        };

        $scope.AddPermission = AddPermission;
        function AddPermission(id, userName) {
            var params = {
                objectID: id,
                userName: userName,
            };
            popupService.open('', '/app/components/application_admins/applicationAdminEditView.html', 'applicationAdminEditController', params);
        }

    }
})(angular.module('onlineshop.application_admins'));