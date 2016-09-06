(function (app) {
    app.controller('newsEditController', newsEditController);
    newsEditController.$inject = ['$scope', 'apiService', '$state','params','$uibModalInstance'];

    function newsEditController($scope, apiService, $state, params, $uibModalInstance) {
        $scope.news = {};

        function GetDetailnews() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };
            apiService.get('/api/news/getbyid', config, function (result) {
                $scope.news = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailnews();

        $scope.UpdateNews = UpdateNews;
        function UpdateNews() {
            apiService.put('/api/news/update', $scope.news, function (result) {
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
})(angular.module('onlineshop.news'));