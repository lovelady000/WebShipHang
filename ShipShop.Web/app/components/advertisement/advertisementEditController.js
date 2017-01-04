(function (app) {
    app.controller('advertisementEditController', advertisementEditController);
    advertisementEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance', 'notificationService', 'commonService'];

    function advertisementEditController($scope, apiService, $state, params, $uibModalInstance, notificationService, commonService) {
        var configView = {};
        configView.key = params.key;
        configView.bImage = params.bImage;
        configView.text = params.text;
        var key = params.key;

        function GetDetailPost() {
            var config = {
                params: {
                    key: params.key,
                }
            };

            apiService.get('/api/webInformation/GetConfigByKey', config, function (result) {
                configView.value = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailPost();

        $scope.Config = configView;


        $scope.Update = Update;
        function Update() {
            var config = {
                Key :  $scope.Config.key,
                Value: $scope.Config.value
            };
            apiService.put('/api/webInformation/UpdateConfig', config, function (result) {
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

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filter) {
                $scope.$apply(function () {
                    $scope.Config.value = filter;
                })

            };
            finder.popup();
        }
    };
})(angular.module('onlineshop.advertisement'));