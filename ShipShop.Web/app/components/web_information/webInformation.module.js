/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.webInformation', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("webInformation", {
            url: "/webInformation",
            parent: 'base',
            templateUrl: "/app/components/web_information/webInformationListView.html",
            controller: "webInformationListController"
        });
    };

})();