/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.post', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("post", {
            url: "/post",
            parent: 'base',
            templateUrl: "/app/components/post/postListView.html",
            controller: "postListController"
        });
    };

})();