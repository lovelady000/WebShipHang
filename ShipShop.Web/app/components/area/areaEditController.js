(function (app) {
    app.controller('areaEditController', areaEditController);
    areaEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance'];

    function areaEditController($scope, apiService, $state, params, $uibModalInstance) {
        $scope.area = {};

        function GetDetailnews() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };
            apiService.get('/api/area/getbyid', config, function (result) {
                $scope.area = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailnews();

        $scope.UpdateArea = UpdateArea;
        function UpdateArea() {
            apiService.put('/api/area/update', $scope.area, function (result) {
                $uibModalInstance.close();
                $state.reload();
            }, function (error) {
                console.log(error);
            });
        };
       
        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    };
})(angular.module('onlineshop.area'));