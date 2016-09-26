(function (app) {
    app.controller('dvtbEditController', dvtbEditController);
    dvtbEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance','notificationService'];

    function dvtbEditController($scope, apiService, $state, params, $uibModalInstance, notificationService) {
        $scope.dvtb = {};

        function GetDetailDVTB() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };
            apiService.get('/api/dvtb/getbyid', config, function (result) {
                $scope.dvtb = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailDVTB();

        $scope.UpdateDVTB = UpdateDVTB;
        function UpdateDVTB() {
            apiService.put('/api/dvtb/update', $scope.dvtb, function (result) {
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

                    $scope.dvtb.Image = filter;
                });
            };
            finder.popup();
        }
    };
})(angular.module('onlineshop.dvtb'));