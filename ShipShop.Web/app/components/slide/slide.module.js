/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.slide', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("slide", {
            url: "/slide",
            parent: 'base',
            templateUrl: "/app/components/slide/slideListView.html",
            controller:"slideListController"
        }).state("add_slide", {
            url: "/add_slide",
            parent: 'base',
            templateUrl: "/app/components/slide/slideAddView.html",
            controller:"slideAddController"
        }).state("edit_slide", {
            url: "/edit_slide/:id",
            parent: 'base',
            templateUrl: "/app/components/slide/slideEditView.html",
            controller: "slideEditController"
        });
    };

})();