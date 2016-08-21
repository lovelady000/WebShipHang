(function (app) {
    app.controller('newsAddController', newsAddController);
    newsAddController.$inject = ['$scope','apiService','$state','$uibModalInstance'];

    function newsAddController($scope, apiService, $state, $uibModalInstance) {
        $scope.newNews = {
            Status:true,
        };

        $scope.AddNews = AddNews;
        function AddNews() {
            apiService.post('api/news/create', $scope.newNews, function (result) {
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

})(angular.module('onlineshop.news'));