(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function orderListController($scope, apiService, notificationService) {

        $scope.order = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getOrder = getOrder;
        $scope.search = search;


        function search() {
            getOrder();
        }

        function getOrder(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10,
                }
            };

            apiService.get('/api/order/getall', config, function (result) {
                $scope.order = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                notificationService.displaySuccess('Thành công');
            }, function () {
                console.log('error');
            });
        };

        $scope.getOrder();
    };

})(angular.module('onlineshop.order'));