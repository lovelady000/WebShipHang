(function (app) {
    app.controller('postEditController', postEditController);
    postEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance', 'notificationService', 'commonService'];

    function postEditController($scope, apiService, $state, params, $uibModalInstance, notificationService, commonService) {
        $scope.post = {};

        function getPostCategory() {
            apiService.get('/api/postcategory/getallnopaging', null, function (result) {
                $scope.Category = result.data;
            }, function (error) {
                console.log(error);
            });
        }
        getPostCategory();

        function GetDetailPost() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };

            apiService.get('/api/post/getbyid', config, function (result) {
                $scope.post = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailPost();

        $scope.Update = Update;
        function Update() {
            apiService.put('/api/post/Update', $scope.post, function (result) {
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
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
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
})(angular.module('onlineshop.postCategory'));