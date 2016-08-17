/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.news', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("news", {
            url: "/news",
            parent: 'base',
            templateUrl: "/app/components/news/newsListView.html",
            controller:"newsListController"
        }).state("add_news", {
            url: "/add_news",
            parent: 'base',
            templateUrl: "/app/components/news/newsAddView.html",
            controller:"newsAddController"
        }).state("edit_news", {
            url: "/edit_news/:id",
            parent: 'base',
            templateUrl: "/app/components/news/newsEditView.html",
            controller: "newsEditController"
        });
    };

})();