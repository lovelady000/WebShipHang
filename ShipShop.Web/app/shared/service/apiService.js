/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http','notificationService','authenticationService'];
    function apiService($http, notificationService,authenticationService) {
        return {
            get: get,
            post:post,
            put:put,
        };
        function get(url, params, success, failed) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {

                
                failed(error);
            });
        }
        function post(url, data, success, failed) {
            authenticationService.setHeader();
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 404) {
                    notificationService.displayError('Quyền truy cập bị từ chối!');
                }
                failed();
            })
        }
        function put(url, data, success, failed) {
            authenticationService.setHeader();
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 404) {
                    notificationService.displayError('Quyền truy cập bị từ chối!');
                }
                failed();
            })
        }
    };
})(angular.module('onlineshop.common'));