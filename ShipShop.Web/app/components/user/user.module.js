(function () {
    angular.module('onlineshop.user', ['onlineshop.common']).config(config);

    config.$inject = ['$stateProvider'];
    function config($stateProvider) {
        $stateProvider.state("user", {
            url: "/user",
            parent: 'base',
            templateUrl: "/app/components/user/userListView.html",
            controller: "userListController"
        })
    }
})()