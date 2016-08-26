(function (app) {
    app.controller('changePasswordController', changePasswordController);
    changePasswordController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance', 'notificationService'];

    function changePasswordController($scope, apiService, $state, params, $uibModalInstance, notificationService) {
        $scope.user = {
            //Id: params.objectID,
        };
        $scope.ChangePass = ChangePass;
        function ChangePass() {
            apiService.post('/api/account/changePass', $scope.user, function (result) {
                $uibModalInstance.close();
                $state.reload();
                notificationService.displaySuccess('Thay đổi mật khẩu thành công!');
            }, function (error) {
                console.log(error);
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    };
})(angular.module('onlineshop.application_users'));