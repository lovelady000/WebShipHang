(function (app) {
    app.controller('dvtbAddController', dvtbAddController);
    dvtbAddController.$inject = ['$scope','apiService','$state','$uibModalInstance'];

    function dvtbAddController($scope, apiService, $state, $uibModalInstance) {
        $scope.dvtb = {};

        $scope.AddDVTB = AddDVTB;
        function AddDVTB() {
            apiService.post('/api/dvtb/create', $scope.dvtb, function (result) {
                $state.reload();
                $uibModalInstance.close();
            }, function (error) {
                console.log(error);
            });
        };


        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filter) {
                $scope.dvtb.Image = filter;
                console.log($scope.dvtb.Image);

            };
            finder.popup();
        }
    };

})(angular.module('onlineshop.dvtb'));