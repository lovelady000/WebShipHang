(function (app) {
    app.controller('rootController', ['$state', 'authData', 'loginService', '$scope', 'authenticationService',
        'popupService', 'permissionService', 'apiService',
        function ($state, authData, loginService, $scope, authenticationService, popupService, permissionService, apiService) {
            $scope.logOut = function () {
                loginService.logOut();
                $state.go('login');
            }
            console.log('root controller');
            $scope.authentication = authData.authenticationData;
            authenticationService.validateRequest();
            $scope.changPass = changPass;
            function changPass() {
                popupService.open('', '/app/components/home/changePasswordView.html', 'changePasswordController', '');
            };


            var hub = $.connection.orderHub;
            hub.client.broadcastMessage = function (name, message) {
                // Html encode display name and message. 
                //var encodedName = $('<div />').text(name).html();
                //var encodedMsg = $('<div />').text(message).html();
                // Add the message to the page. 
                //$('#discussion').append('<li><strong>' + encodedName
                //+ '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
                alert(message);
            };


            $.connection.hub.start().done(function () {

                setTimeout(function () {

                    hub.server.newOrder("hello", "Xin chao");
                }, 5000);
                //$('#sendmessage').click(function () {
                // Call the Send method on the hub. 

                // Clear text box and reset focus for next comment. 
                //$('#message').val('').focus();
                //});
            });


            var constValue = {
                ACCOUNT_ADMINISTRATOR: "administrator",

                ROLES_FULL_CONTROL: "full_control",
                ROLES_ADMIN: "Admin",
                ROLES_USER: "User",


                ROLES_ADD_NEW_USER: "add_new_user",
                ROLES_EDIT_USER: "edit_user",
                ROLES_GET_LIST_USER: "get_list_user",

                ROLES_GET_LIST_ADMIN: "get_list_admin",
                ROLES_ADD_ADMIN: "add_admin",
                ROLES_EDIT_ADMIN: "edit_admin",
                ROLES_DELETE_ADMIN: "delete_admin",


                //Application group
                ROLES_GET_LIST_GROUP: "get_list_group",
                ROLES_ADD_GROUP: "add_group",
                ROLES_EDIT_GROUP: "edit_group",
                ROLES_DELETE_GROUP: "delete_group",

                ROLES_ADD_PERMISSION_ADMIN: "add_permission_user",
                ROLES_GET_LIST_ROLES: "get_list_roles",



                //Area
                ROLES_GET_LIST_AREA: "get_list_area",
                ROLES_ADD_AREA: "add_area",
                ROLES_EDIT_AREA: "edit_area",
                ROLES_DELETE_AREA: "delete_area",

                //donvitieubieu

                ROLES_GET_LIST_DVTB: "get_list_dvtb",
                ROLES_ADD_DVTB: "add_dvtb",
                ROLES_EDIT_DVTB: "edit_dvtb",
                ROLES_DELETE_DVTB: "delete_dvtb",

                //Menu

                ROLES_GET_LIST_MENU: "get_list_menu",
                ROLES_ADD_MENU: "add_menu",
                ROLES_EDIT_MENU: "edit_menu",
                ROLES_DELETE_MENU: "delete_menu",

                //NEWS
                ROLES_GET_LIST_NEWS: "get_list_news",
                ROLES_ADD_NEWS: "add_news",
                ROLES_EDIT_NEWS: "edit_news",
                ROLES_DELETE_NEWS: "delete_news",

                //ORDER

                ROLES_GET_LIST_ORDER: "get_list_order",
                ROLES_ADD_ORDER: "add_order",
                ROLES_EDIT_ORDER: "edit_order",
                ROLES_DELETE_ORDER: "delete_order",

                //page

                ROLES_GET_LIST_PAGE: "get_list_page",
                ROLES_ADD_PAGE: "add_page",
                ROLES_EDIT_PAGE: "edit_page",
                ROLES_DELETE_PAGE: "delete_page",

                //REGION

                ROLES_GET_LIST_REGION: "get_list_region",
                ROLES_ADD_REGION: "add_region",
                ROLES_EDIT_REGION: "edit_region",
                ROLES_DELETE_REGION: "delete_region",

                //SLIDE
                ROLES_GET_LIST_SLIDE: "get_list_slide",
                ROLES_ADD_SLIDE: "add_slide",
                ROLES_EDIT_SLIDE: "edit_slide",
                ROLES_DELETE_SLIDE: "delete_slide",

                //WEBINFO
                ROLES_EDIT_WEBINFO: "edit_webinfo",

                //post
                ROLES_GET_LIST_POST: "get_list_post",
                ROLES_ADD_POST: "add_post",
                ROLES_EDIT_POST: "edit_post",
                ROLES_DELETE_POST: "delete_post",
                //postCategory
                ROLES_GET_LIST_POSTCATEGORY: "get_list_postcategory",
                ROLES_ADD_POSTCATEGORY: "add_postcategory",
                ROLES_EDIT_POSTCATEGORY: "edit_postcategory",
                ROLES_DELETE_POSTCATEGORY: "delete_postcategory",
            };
            $scope.ROLES = constValue;

        }]);

})(angular.module('onlineshop'));