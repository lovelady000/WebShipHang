(function (app) {
    app.controller('slideAddController', slideAddController);
    slideAddController.$inject = ['$scope', 'apiService', '$state', '$uibModalInstance','notificationService'];

    function slideAddController($scope, apiService, $state, $uibModalInstance, notificationService) {
        $scope.slide = {
        };

        $scope.AddSlide = AddSlide;
        function AddSlide() {
            apiService.post('/api/slide/create', $scope.slide, function (result) {
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
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filter) {
                $scope.$apply(function () {
                    $scope.slide.Image = filter;
                });
            };
            finder.popup();
        }
    };

})(angular.module('onlineshop.slide'));