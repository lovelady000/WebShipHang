(function (app) {
    app.service('popupService', popupService);
    popupService.$inject = ['$uibModal'];
    function popupService($uibModal) {
        return {
            open: openPopup,
        };
        function openPopup(size, templateUrl, controller, params) {
            var modalInstance = $uibModal.open({
                //animation: $scope.animationsEnabled,
                backdrop: 'static',
                windowTemplateUrl: '/Assets/admin/libs/angular-ui-bootstrap/template/modal/window.html',
                templateUrl: templateUrl,
                controller: controller,
                size: size,
                resolve: {
                    params: function () {
                        return params;
                    }
                }
            });
        };
    }


})(angular.module('onlineshop.common'))