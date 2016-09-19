(function (app) {
    app.controller('areaAddController', areaAddController);
    areaAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance','notificationService'];

    function areaAddController($scope, apiService, $state, $uibModalInstance, notificationService) {
        $scope.area = {
        };

        $scope.AddArea = AddArea;
        function AddArea() {
            apiService.post('/api/area/create', $scope.area, function (result) {
                notificationService.displaySuccess('Thêm mới thành công !');
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

})(angular.module('onlineshop.area'));