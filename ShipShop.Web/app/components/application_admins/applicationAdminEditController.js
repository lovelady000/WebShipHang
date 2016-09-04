(function (app) {
    'use strict';

    app.controller('applicationAdminEditController', applicationAdminEditController);

    applicationAdminEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'params', '$uibModalInstance','$state'];

    function applicationAdminEditController($scope, apiService, notificationService, $location, params, $uibModalInstance, $state) {
        $scope.account = {}

        $scope.updateAccount = updateAccount;

        function updateAccount() {
            apiService.put('/api/applicationAdmin/update', $scope.account, addSuccessed, addFailed);
        }
        function loadDetail() {
            apiService.get('/api/applicationAdmin/detail/' + params.objectID, null,
            function (result) {
                $scope.account = result.data;
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.account.FullName + ' đã được cập nhật thành công.');

            $state.reload();
            $uibModalInstance.close();
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }
        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });

        }

        loadGroups();
        loadDetail();
        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }
})(angular.module('onlineshop.application_admins'));