/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('onlineshop.application_admins', ['onlineshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_admins', {
            url: "/application_admins",
            templateUrl: "/app/components/application_admins/applicationAdminListView.html",
            parent: 'base',
            controller: "applicationAdminListController"
        });
    }
})();