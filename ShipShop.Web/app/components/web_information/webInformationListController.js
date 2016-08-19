/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('webInformationListController', webInformationListController);

    webInformationListController.$inject = ['$scope', 'apiService', 'notificationService', 'popupService', '$ngBootbox', '$state'];

    function webInformationListController($scope, apiService, notificationService, popupService, $ngBootbox, $state) {

        $scope.webInformation = {};

        function getWebInformation() {
            apiService.get('/api/webInformation/getsinger', config, function (result) {
                $scope.webInformation = result.data;
            }, function () {
                console.log('error');
            });
        };
        getWebInformation();

        $scope.Edit = Edit;
        function Edit(id) {
            var params = {
                objectID: id,
            };
            popupService.open('', '/app/components/web_information/webInformationEditView.html', 'webInformationEditController', '');
        };
    };

})(angular.module('onlineshop.webInformation'));