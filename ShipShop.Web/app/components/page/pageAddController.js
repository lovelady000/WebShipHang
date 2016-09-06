(function (app) {
    app.controller('pageAddController', pageAddController);
    pageAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance','commonService'];

    function pageAddController($scope, apiService, $state, $uibModalInstance,commonService) {
        $scope.page = {
            Status:true,
        };

        $scope.AddPage = AddPage;
        function AddPage() {
            apiService.post('/api/page/create', $scope.page, function (result) {
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
        };

        $scope.ChangeName = function () {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };
        $scope.ckeditorOption = {
            languague: 'vi',
            height:'300px',
        };
    };

})(angular.module('onlineshop.page'));