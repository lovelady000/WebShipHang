(function (app) {
    app.controller('slideAddController', slideAddController);
    slideAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance'];

    function slideAddController($scope, apiService, $state, $uibModalInstance) {
        $scope.slide = {
        };

        $scope.AddSlide = AddSlide;
        function AddSlide() {
            apiService.post('api/slide/create', $scope.slide, function (result) {
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
                $scope.slide.Image = filter;
            };
            finder.popup();
        }
    };

})(angular.module('onlineshop.slide'));