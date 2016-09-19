(function (app) {
    app.controller('slideEditController', slideEditController);
    slideEditController.$inject = ['$scope', 'apiService', '$state','params','$uibModalInstance','notificationService'];

    function slideEditController($scope, apiService, $state, params, $uibModalInstance, notificationService) {
        $scope.slide = {};

        function GetDetailSlide() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };
            apiService.get('/api/slide/getbyid', config, function (result) {
                $scope.slide = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailSlide();

        $scope.UpdateSlide = UpdateSlide;
        function UpdateSlide() {
            apiService.put('/api/slide/update', $scope.slide, function (result) {
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
    };
})(angular.module('onlineshop.slide'));