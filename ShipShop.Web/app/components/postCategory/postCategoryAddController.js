(function (app) {
    app.controller('postCategoryAddController', postCategoryAddController);
    postCategoryAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance', 'commonService', 'notificationService'];

    function postCategoryAddController($scope, apiService, $state, $uibModalInstance, commonService, notificationService) {
        $scope.postCate = {
            Status:true,
        };

        $scope.AddPostCate = AddPostCate;
        function AddPostCate() {
            apiService.post('/api/postcategory/create', $scope.postCate, function (result) {
                notificationService.displaySuccess('Thêm mới thành công !');
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
        };

        $scope.ChangeName = function () {
            $scope.postCate.Alias = commonService.getSeoTitle($scope.postCate.Name);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
    };

})(angular.module('onlineshop.page'));