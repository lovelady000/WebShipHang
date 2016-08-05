﻿/// <reference path="../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module("onlineshop",
        [
        'onlineshop.common',
        'onlineshop.menu'
        ]).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("home", {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/admin');
    };
})();