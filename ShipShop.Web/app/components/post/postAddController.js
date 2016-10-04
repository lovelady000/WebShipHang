(function (app) {
    app.controller('postAddController', postAddController);
    postAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance', 'commonService', 'notificationService'];

    function postAddController($scope, apiService, $state, $uibModalInstance, commonService, notificationService) {
        $scope.post = {
            Status:true,
        };
        $scope.Category = [];

        $scope.AddPost = AddPost;
        function AddPost() {
            apiService.post('/api/post/create', $scope.post, function (result) {
                notificationService.displaySuccess('Thêm mới thành công !');
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
        };

        function getPostCategory() {
            apiService.get('/api/postcategory/getallnopaging', null, function (result) {
                $scope.Category = result.data;
            }, function (error) {
                console.log(error);
            });
        }
        getPostCategory();
        $scope.ChangeName = function () {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filter) {
                $scope.$apply(function () {
                    $scope.post.Image = filter;
                })

            };
            finder.popup();
        }
    };

})(angular.module('onlineshop.post'));