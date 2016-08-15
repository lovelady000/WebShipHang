/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.application_groups', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("application_groups", {
            url: "/application_groups",
            parent: 'base',
            templateUrl: "/app/components/application_group/applicationGroupListView.html",
            controller: "applicationGroupListController"
        }).state("add_application_group", {
            url: "/add_application_group",
            parent: 'base',
            templateUrl: "/app/components/application_group/applicationGroupAddView.html",
            controller: "applicationGroupAddController"
        }).state("edit_application_group", {
            url: "/edit_application_group/:id",
            parent: 'base',
            templateUrl: "/app/components/application_group/applicationGroupEditView.html",
            controller: "applicationGroupEditController"
        });
    };

})();