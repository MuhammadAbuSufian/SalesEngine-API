"use strict";


angular.module("app")
    .controller("IssueNameController",
        [
            "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "$http",
            function ($scope, $rootScope, urlService, $controller, baseService, $http) {
                "use strict";


                var config = function () {
                    baseService.setCurrentApi(urlService.IssueNameUrls.IssueName);
                    baseService.setIsLoadDataFromUrls(false);
                    baseService.setUrls([]);
                    baseService.setIsLoadPagingData(false);
                    baseService.setDataStatus("Active");
                    baseService.setCallerStatus(true);
                    $controller("BaseController", { $scope: $scope });
                }

                var init = function () {
                    
                    $scope.baseCaller();
                    $scope.start();
                };

                $scope.$on("parentResetEventCaller",
                    function (event, args) {
                        $scope.start();
                    });


                $scope.start = function () {
                };


                config();
                init();


            }
        ]);