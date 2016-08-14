(function (app) {
    app.controller('orderDetailListController', orderDetailListController);

    orderDetailListController.$inject = ['$scope', 'apiService', 'notificationService','$stateParams'];

    function orderDetailListController($scope, apiService, notificationService, $stateParams) {

        $scope.orderDetail = [];
        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getOrderDetail = getOrderDetail;
        $scope.orderID = $stateParams.orderID;
        function getOrderDetail(page) {
            page = page || 0;
            var config = {
                params: {
                    orderID: $stateParams.orderID,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/order/getorderdetail', config, function (result) {
                $scope.orderDetail = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                notificationService.displaySuccess('Thành công');
            }, function () {
                console.log('error');
            });
        };

        $scope.getOrderDetail();
    };

})(angular.module('onlineshop.orderDetail'));