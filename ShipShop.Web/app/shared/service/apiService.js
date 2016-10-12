/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http','notificationService','authenticationService'];
    function apiService($http, notificationService,authenticationService) {
        return {
            get: get,
            post:post,
            put: put,
            del: del
        };
        function get(url, params, success, failed) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                success(result);
                //authenticationService.refreshToken();
            }, function (error) {
                 if (error.status === 401) {
                    notificationService.displayError('Quyền truy cập bị từ chối!');
                }
                failed(error);
            });
        }
        function post(url, data, success, failed) {
            authenticationService.setHeader();
            
            $http.post(url, data).then(function (result) {
                success(result);
                //authenticationService.refreshToken();
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Quyền truy cập bị từ chối!');
                }
                failed();
            })
        }
        function put(url, data, success, failed) {
            authenticationService.setHeader();

            $http.put(url, data).then(function (result) {
                success(result);
                //authenticationService.refreshToken();
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Quyền truy cập bị từ chối!');
                }
                failed();
            })
        }
        function del(url, data, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
                success(result);
                //authenticationService.refreshToken();
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }

    };
})(angular.module('onlineshop.common'));