(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData',
    function ($http, $q, authenticationService, authData) {
        var userInfo;
        var deferred;

        this.login = function (userName, password) {
            deferred = $q.defer();
            var data = "grant_type=password&username=" + userName + "&password=" + password;
            $http.post('/oauth/token', data, {
                headers:
                   { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (response) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + response.access_token;
                $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                $http.get('/api/applicationRole/checkPermission', null).then(function (result) {
                    
                    userInfo = {
                        accessToken: response.access_token,
                        userName: userName,
                        roles: result.data,
                        refresh_token: response.refresh_token,
                    };
                    //console.log(userInfo);
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;
                    authData.authenticationData.roles = result.data;

                    //setInterval(function () {
                    //    //console.log(response);
                    //    //alert("Hello");
                    //    authenticationService.refreshToken();
                    //}, 1500000);

                    deferred.resolve(null);

                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError('Quyền truy cập bị từ chối!');
                    }
                });
            })
            .error(function (err, status) {
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                authData.authenticationData.roles = [];
                authData.authenticationData.refresh_token = "";
                deferred.resolve(err);
            });
            return deferred.promise;
        }

        this.logOut = function () {
            authenticationService.removeToken();
            authData.authenticationData.IsAuthenticated = false;
            authData.authenticationData.userName = "";
            authData.authenticationData.roles = [];
            authData.authenticationData.refresh_token = "";
        }
    }]);
})(angular.module('onlineshop.common'));