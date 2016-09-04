(function (app) {
    'use strict';

    app.controller('applicationAdminAddController', applicationAdminAddController);
    
    applicationAdminAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService','$uibModalInstance','$state'];

    function applicationAdminAddController($scope, apiService, notificationService, $location, commonService, $uibModalInstance,$state) {
        $scope.account = {
            Groups: []
        }

        $scope.addAccount = addAccount;

        function addAccount() {
            apiService.post('/api/applicationAdmin/add', $scope.account, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.account.Name + ' đã được thêm mới.');
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
        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
        loadGroups();

    }
})(angular.module('onlineshop.application_admins'));