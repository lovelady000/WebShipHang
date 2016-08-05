(function (app) {
    app.filter('status', function () {
        return function (input) {
            if (input) {
                return 'Kích hoạt';
            } else {
                return 'Khóa';
            }
        };
    });
})(angular.module('onlineshop.common'));