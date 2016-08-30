﻿(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function orderListController($scope, apiService, notificationService, $state) {

        $scope.sortType = 'CreatedDate'; // set the default sort type
        $scope.sortReverse = true;  // set the default sort order
        $scope.typeOfOrder = "0";


        $scope.order = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getOrder = getOrder;
        $scope.search = search;
        $scope.totalCOD = 0;

        $scope.addCommas = addCommas;
        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }

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
                    typeOrder: $scope.typeOfOrder,
                }
            };

            apiService.get('/api/order/getall', config, function (result) {
                $scope.order = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                notificationService.displaySuccess('Thành công');
                var total = 0;
                for (var i = 0; i < result.data.Items.length; ++i) {
                    total += result.data.Items[i].PayCOD;
                }
                $scope.totalCOD = total;
            }, function () {
                console.log('error');
            });
        };

        $scope.getOrder();
        $scope.cancelOrder = cancelOrder;
        function cancelOrder(id) {
            var obj = {
                ID: id,
            };
            apiService.put('/api/order/changeOrderStatus', obj, function (result) {
                $state.reload();
                notificationService.displaySuccess('Hủy đơn hàng thành công');
            }, function () {
                console.log('error');
            });
        }

        $scope.changeTypeOrder = changeTypeOrder;
        function changeTypeOrder() {
            getOrder(0);
        };

    };

})(angular.module('onlineshop.order'));