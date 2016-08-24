(function (app) {
    app.controller('regionAddController', regionAddController);
    regionAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance'];

    function regionAddController($scope, apiService, $state, $uibModalInstance) {
        $scope.region = {
        };
        $scope.listAreas = [];

        (function () {
            apiService.get('api/area/getallnopaging', null, function (result) {
                $scope.listAreas = result.data;
            }, function (error) {
                console.log(error);
            });
        })();

        $scope.AddNews = AddNews;
        function AddNews() {
            apiService.post('api/region/create', $scope.region, function (result) {
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    };

})(angular.module('onlineshop.region'));