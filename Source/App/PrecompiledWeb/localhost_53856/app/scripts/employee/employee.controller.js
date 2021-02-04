"use strict";


angular.module("app")
    .controller("EmployeeController",
    [
         "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService",
        function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService) {
            "use strict";


            var config = function () {
                baseService.setCurrentApi(urlService.EmployeeUrls.Employee);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            }

            var init = function () {
                //$scope.addCall(urlService.AccountUrls.EpbAccount);
                //$scope.addCall(urlService.DropdownUrl + "?type=Exporter");

                //var dataProperties = ["list", "exporterDropdown"];

                //baseService.setUrls($scope.urls);
                //baseService.setDataProperties(dataProperties);

                //$scope.baseCaller();
                $scope.start();
            };

            $scope.start = function() {

            };


            config();
            init();


        }
    ]);