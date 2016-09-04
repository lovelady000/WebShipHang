(function (app) {
    app.service('permissionService', permissionService);
    permissionService.$inject = ['apiService'];
    function permissionService(apiService) {
        return {
            checkRole: checkRole,
        };
        function checkRole(role) {
            console.log(role);
        }
        //function checkRole(role) {
        //    var config = {
        //        params: {
        //            permission: role,
        //        }
        //    };
        //    apiService.get('/api/applicationRole/checkPermission', config, function (result) {
        //        console.log(result.data);
        //    }, function () {
        //        console.log('Không thể kiểm tra quyền!');
        //    });
        //}
    }


})(angular.module('onlineshop.common'))