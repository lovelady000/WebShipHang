/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.dvtb', ['onlineshop.common']).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("dvtb", {
            url: "/dvtb",
            parent: 'base',
            templateUrl: "/app/components/don_vi_tieu_bieu/dvtbListView.html",
            controller:"dvtbListController"
        }).state("add_dvtb", {
            url: "/add_dvtb",
            parent: 'base',
            templateUrl: "/app/components/don_vi_tieu_bieu/dvtbAddView.html",
            controller:"dvtbAddController"
        }).state("edit_dvtb", {
            url: "/edit_dvtb/:id",
            parent: 'base',
            templateUrl: "/app/components/don_vi_tieu_bieu/dvtbEditView.html",
            controller: "dvtbEditController"
        });
    };

})();