angular.module('onlineshop.orderDetail', ['onlineshop.common']).config(config);

config.$inject = ["$stateProvider", "$urlRouterProvider"];

function config($stateProvider, $urlRouterProvider) {
    $stateProvider.state("orderDetail", {
        url: "/orderDetail/:orderID",
        parent: 'base',
        templateUrl: "/app/components/orderDetail/orderDetailListView.html",
        controller: "orderDetailListController"
    })
};