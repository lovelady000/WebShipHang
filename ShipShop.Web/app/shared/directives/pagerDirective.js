(function (app) {
    'use strict';

    app.directive('pagerDirective', pagerDirective);

    function pagerDirective() {
        var temp = '<div>';
        temp += ' <div ng-hide="(!pagesCount || pagesCount < 2)" style="display:inline">';
        temp += ' <ul class="pagination pagination-sm">';
        temp += '<li><a ng-hide="page == 0" ng-click="search(0)"><<</a></li>';
        temp += '<li><a ng-hide="page == 0" ng-click="search(page-1)"><</a></li>';
        temp += '<li ng-repeat="n in range()" ng-class="{active: n == page}">';
        temp += '<a ng-click="search(n)" ng-if="n != page">{{n+1}}</a>';
        temp += '<span ng-if="n == page">{{n+1}}</span>';
        temp += '</li>';
        temp += '<li><a ng-hide="page == pagesCount - 1" ng-click="search(pagePlus(1))">></a></li>';
        temp += '<li><a ng-hide="page == pagesCount - 1" ng-click="search(pagesCount - 1)">>></a></li>';
        temp += '</ul>';
        temp += '</div>';
        temp += '</div>';
        return {
            scope: {
                page: '@',
                pagesCount: '@',
                totalCount: '@',
                searchFunc: '&',
                customPath: '@'
            },
            replace: true,
            restrict: 'E',
            //templateUrl: '/app/shared/directives/pagerDirective.html',
            template: temp,
            controller: [
                '$scope', function ($scope) {
                    $scope.search = function (i) {
                        if ($scope.searchFunc) {
                            $scope.searchFunc({ page: i });
                        }
                    };

                    $scope.range = function () {
                        if (!$scope.pagesCount) { return []; }
                        var step = 2;
                        var doubleStep = step * 2;
                        var start = Math.max(0, $scope.page - step);
                        var end = start + 1 + doubleStep;
                        if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                        var ret = [];
                        for (var i = start; i != end; ++i) {
                            ret.push(i);
                        }

                        return ret;
                    };

                    $scope.pagePlus = function (count) {
                        return +$scope.page + count;
                    }

                }]
        }
    }

})(angular.module('onlineshop.common'));