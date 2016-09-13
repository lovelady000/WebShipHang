(function (app) {
    'use strict';
    app.service('authenticationService', ['$http', '$q', '$window','authData',
        function ($http, $q, $window, authData) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                tokenInfo = data;
                $window.localStorage["TokenInfo"] = JSON.stringify(tokenInfo);
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                $window.localStorage["TokenInfo"] = "";
            }

            this.init = function () {
                if ($window.localStorage["TokenInfo"]) {
                    tokenInfo = JSON.parse($window.localStorage["TokenInfo"]);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = tokenInfo.userName;
                    authData.authenticationData.roles = tokenInfo.roles;
                }
            }

            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                }
            }
            this.refreshToken = function refreshToken() {
                var data = "grant_type=refresh_token&client_id=&refresh_token=" + tokenInfo.refresh_token;
                $http.post('/oauth/token', data, {
                    headers:
                       { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (response) {
                    //console.log('old token ' + tokenInfo.accessToken);
                    //console.log(response);
                    tokenInfo.accessToken = response.access_token;
                    tokenInfo.refresh_token = response.refresh_token;
                    $window.localStorage["TokenInfo"] = JSON.stringify(tokenInfo);
                    //console.log('new token ' + tokenInfo.accessToken);
                })
                .error(function (err, status) {
                    console.log(err);
                });
            }

            this.validateRequest = function () {
                var url = '/api/home/TestMethod';
                var deferred = $q.defer();
                this.setHeader();
                $http.get(url).then(function () {
                    deferred.resolve(null);
                }, function (error) {
                    deferred.reject(error);
                });
                return deferred.promise;
            }

            this.init();
        }
    ]);
})(angular.module('onlineshop.common'));