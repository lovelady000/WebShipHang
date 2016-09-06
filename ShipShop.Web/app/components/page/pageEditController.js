(function (app) {
    app.controller('pageEditController', pageEditController);
    pageEditController.$inject = ['$scope', 'apiService', '$state', 'params', '$uibModalInstance'];

    function pageEditController($scope, apiService, $state, params, $uibModalInstance) {
        $scope.page = {};

        function GetDetailnews() {
            var config = {
                params: {
                    id: params.objectID,
                }
            };
            apiService.get('/api/page/getbyid', config, function (result) {
                $scope.page = result.data;
            }, function (error) {
                console.log('error');
            });
        };
        GetDetailnews();

        $scope.UpdateNews = UpdateNews;
        function UpdateNews() {
            apiService.put('/api/page/update', $scope.page, function (result) {
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
})(angular.module('onlineshop.page'));