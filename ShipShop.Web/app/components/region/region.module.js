/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.region', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("region", {
            url: "/region",
            parent: 'base',
            templateUrl: "/app/components/region/regionListView.html",
            controller:"regionListController"
        }).state("add_region", {
            url: "/add_region",
            parent: 'base',
            templateUrl: "/app/components/region/regionAddView.html",
            controller:"regionAddController"
        }).state("edit_region", {
            url: "/edit_region/:id",
            parent: 'base',
            templateUrl: "/app/components/region/regionEditView.html",
            controller: "regionEditController"
        });
    };

})();