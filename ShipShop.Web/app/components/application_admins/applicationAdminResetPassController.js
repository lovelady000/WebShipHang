(function (app) {
    app.controller('applicationAdminResetPassController', applicationAdminResetPassController);
    applicationAdminResetPassController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance','notificationService'];

    function applicationAdminResetPassController($scope, apiService, $state, params, $uibModalInstance, notificationService) {
        $scope.user = {
            Id: params.objectID,
        };
        $scope.ResetPass = ResetPass;
        function ResetPass() {
            apiService.put('/api/applicationAdmin/resetPass', $scope.user, function (result) {
                $uibModalInstance.close();
                $state.reload();
                notificationService.displaySuccess('Khôi phục mật khẩu thành công!');
            }, function (error) {
                console.log(error);
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    };
})(angular.module('onlineshop.application_admins'));