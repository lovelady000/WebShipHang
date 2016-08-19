(function (app) {
    app.controller('webInformationEditController', webInformationEditController);
    webInformationEditController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance'];

    function webInformationEditController($scope, apiService, $state, $uibModalInstance) {
        $scope.webInformation = {};

        function GetDetailnews() {
            apiService.get('/api/webInformation/getsinger', config, function (result) {
                $scope.webInformation = result.data;
            }, function () {
                console.log('error');
            });
        };
        GetDetailnews();

        $scope.UpdateWebInfo = UpdateWebInfo;
        function UpdateWebInfo() {
            apiService.put('api/webInformation/update', $scope.webInformation, function (result) {
                $uibModalInstance.close();
                $state.reload();
            }, function (error) {
                console.log(error);
            });
        };
       
        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
        $scope.ckeditorOptions = {
            languague: 'vi',
            height:'200px',
        };
    };
})(angular.module('onlineshop.webInformation'));