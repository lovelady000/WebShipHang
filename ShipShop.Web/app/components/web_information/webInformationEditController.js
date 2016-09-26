(function (app) {
    app.controller('webInformationEditController', webInformationEditController);
    webInformationEditController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance','notificationService'];

    function webInformationEditController($scope, apiService, $state, $uibModalInstance, notificationService) {
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
            apiService.put('/api/webInformation/update', $scope.webInformation, function (result) {
                notificationService.displaySuccess('Sửa thông tin thành công !');
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


        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filter) {
                $scope.$apply(function () {
                    $scope.webInformation.Logo = filter;
                })
                
            };
            finder.popup();
        }
    };
})(angular.module('onlineshop.webInformation'));