/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.advertisement', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("advertisement", {
            url: "/advertisement",
            parent: 'base',
            templateUrl: "/app/components/advertisement/advertisementListView.html",
            controller: "advertisementListController"
        });
    };

})();