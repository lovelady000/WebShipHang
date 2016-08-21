/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.area', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("area", {
            url: "/area",
            parent: 'base',
            templateUrl: "/app/components/area/areaListView.html",
            controller:"areaListController"
        }).state("add_area", {
            url: "/add_area",
            parent: 'base',
            templateUrl: "/app/components/area/areaAddView.html",
            controller:"areaAddController"
        }).state("edit_area", {
            url: "/edit_area/:id",
            parent: 'base',
            templateUrl: "/app/components/area/areaEditView.html",
            controller: "areaEditController"
        });
    };

})();