(function (app) {
    app.controller('applicationUserResetPassController', applicationUserResetPassController);
    applicationUserResetPassController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance','notificationService'];

    function applicationUserResetPassController($scope, apiService, $state, params, $uibModalInstance, notificationService) {
        $scope.user = {
            Id: params.objectID,
        };
        $scope.ResetPass = ResetPass;
        function ResetPass() {
            apiService.put('/api/applicationUser/resetPass', $scope.user, function (result) {
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
})(angular.module('onlineshop.application_users'));