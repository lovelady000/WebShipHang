(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService', 'popupService'];

    function rootController($state, authData, loginService, $scope, authenticationService, popupService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }
        $scope.authentication = authData.authenticationData;
        //authenticationService.validateRequest();
        $scope.changPass = changPass;
        function changPass() {
            popupService.open('', '/app/components/home/changePasswordView.html', 'changePasswordController', '');
        };
    }

})(angular.module('onlineshop'));