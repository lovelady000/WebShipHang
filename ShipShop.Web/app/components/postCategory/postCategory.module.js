/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.postCategory', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("postCategory", {
            url: "/post",
            parent: 'base',
            templateUrl: "/app/components/postCategory/postCategoryListView.html",
            controller: "postCategoryListController"
        });
    };

})();