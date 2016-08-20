(function (app) {
    app.controller('pageAddController', pageAddController);
    pageAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance'];

    function pageAddController($scope, apiService, $state, $uibModalInstance) {
        $scope.page = {
            Status:true,
        };

        $scope.AddPage = AddPage;
        function AddPage() {
            apiService.post('api/page/create', $scope.page, function (result) {
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
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