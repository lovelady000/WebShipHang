(function (app) {
    app.controller('postCategoryEditController', postCategoryEditController);
    postCategoryEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance', 'notificationService','commonService'];

    function postCategoryEditController($scope, apiService, $state, params, $uibModalInstance, notificationService, commonService) {
        $scope.postCate = {};

        function GetDetailPostCate() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };

            apiService.get('/api/postcategory/getbyid', config, function (result) {
                $scope.postCate = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailPostCate();

        $scope.Update = Update;
        function Update() {
            apiService.put('/api/postcategory/Update', $scope.postCate, function (result) {
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
        $scope.ChangeName = function () {
            $scope.postCate.Alias = commonService.getSeoTitle($scope.postCate.Name);
        };
    };
})(angular.module('onlineshop.postCategory'));