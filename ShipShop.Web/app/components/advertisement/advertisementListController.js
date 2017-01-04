/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('advertisementListController', advertisementListController);

    advertisementListController.$inject = ['$scope', 'apiService', 'notificationService', 'popupService', '$ngBootbox', '$state'];

    function advertisementListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

        $scope.Edit = Edit;
        function Edit(key,text,bImage) {
            var params = {
                key: key,
                bImage: bImage,
                text: text,
            };
            popupService.open('', '/app/components/advertisement/advertisementEditView.html', 'advertisementEditController', params);
        };

    };

})(angular.module('onlineshop.advertisement'));