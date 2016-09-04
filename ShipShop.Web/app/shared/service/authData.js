(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: "",
            roles : [],
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('onlineshop.common'));