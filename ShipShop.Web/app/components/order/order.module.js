angular.module('onlineshop.order', ['onlineshop.common']).config(config);

config.$inject = ["$stateProvider", "$urlRouterProvider"];

function config($stateProvider, $urlRouterProvider) {
    $stateProvider.state("order", {
        url: "/order",
        parent: 'base',
        templateUrl: "/app/components/order/orderListView.html",
        controller: "orderListController"
    })
};