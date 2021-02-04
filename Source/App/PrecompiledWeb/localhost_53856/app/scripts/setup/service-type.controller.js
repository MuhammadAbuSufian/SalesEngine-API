"use strict";


angular.module("app")
    .controller("ServiceTypeController",
    [
        "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "$http",
        function($scope, $rootScope, urlService, $controller, baseService, $http) {
            "use strict";


            var config = function() {
                baseService.setCurrentApi(urlService.ServiceTypeUrls.ServiceType);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(false);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            }

            var init = function() {
                //$scope.addCall(urlService.HomeUrls.RmgSearch);
                ////$scope.addCall(urlService.DropdownUrl + "?type=Category");

                //var dataProperties = ["list"];

                //baseService.setUrls($scope.urls);
                //baseService.setDataProperties(dataProperties);

                $scope.baseCaller();
                $scope.start();
            };

            $scope.$on("parentResetEventCaller",
                function(event, args) {
                    $scope.start();
                });


            $scope.start = function () {
            };
            

            config();
            init();


        }
    ]);