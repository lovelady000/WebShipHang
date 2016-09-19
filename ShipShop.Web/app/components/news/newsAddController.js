(function (app) {
    app.controller('newsAddController', newsAddController);
    newsAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance', 'notificationService'];

    function newsAddController($scope, apiService, $state, $uibModalInstance, notificationService) {
        $scope.newNews = {
            Status:true,
        };

        $scope.AddNews = AddNews;
        function AddNews() {
            apiService.post('/api/news/create', $scope.newNews, function (result) {
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

})(angular.module('onlineshop.news'));