/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http','notificationService'];
    function apiService($http, notificationService) {
        return {
            get: get,
            post:post,
        };
        function get(url, params, success, failed) {
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {

                
                failed(error);
            });
        }
        function post(url, data, success, failed) {
            $http.post(url, data).then(function (result) {
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