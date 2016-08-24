(function (app) {
    app.controller('regionEditController', regionEditController);
    regionEditController.$inject = ['$scope', 'apiService', '$state','params','$uibModalInstance'];

    function regionEditController($scope, apiService, $state, params, $uibModalInstance) {
        $scope.region;
        $scope.listAreas = [];

        (function () {
            apiService.get('api/area/getallnopaging', null, function (result) {
                $scope.listAreas = result.data;
            }, function (error) {
                console.log(error);
            });
        })();

        function GetDetailnews() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };
            apiService.get('api/region/getbyid', config, function (result) {
                $scope.region = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailnews();

        $scope.UpdateRegion = UpdateRegion;
        function UpdateRegion() {
            apiService.put('api/region/update', $scope.region, function (result) {
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
})(angular.module('onlineshop.region'));