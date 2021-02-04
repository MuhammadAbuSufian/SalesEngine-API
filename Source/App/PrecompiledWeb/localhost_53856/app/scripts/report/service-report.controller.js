"use strict";


angular.module("app")
    .controller("ServiceReportController",
    [
         "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "$http",
        function ($scope, $rootScope, urlService, $controller, baseService, $http) {
            "use strict";


            var config = function () {
                baseService.setCurrentApi(urlService.HomeUrls.RmgSearch);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                $controller("BaseController", { $scope: $scope });
            }

            var init = function () {
                //$scope.addCall(urlService.HomeUrls.RmgSearch);
                ////$scope.addCall(urlService.DropdownUrl + "?type=Category");

                //var dataProperties = ["list"];

                //baseService.setUrls($scope.urls);
                //baseService.setDataProperties(dataProperties);

                //$scope.baseCaller();
                $scope.start();
            };

            $scope.$on("parentResetEventCaller",
                function (event, args) {
                    $scope.start();
                });


            $scope.start = function () {
                $scope.searchRequest.IsBuyer = true;
                $scope.searchRequest.OrderBy = "CompanyOrFactoryName";
            };

            $scope.filter = function () {
                $scope.pagingRequest.Product = $scope.searchRequest.Product;
                $scope.pagingRequest.HsCode = $scope.searchRequest.HsCode;
                $scope.pagingRequest.Factory = $scope.searchRequest.Factory;
                $scope.pagingRequest.IsBuyer = $scope.searchRequest.IsBuyer;

                $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                $scope.search();
            }


            config();
            init();

        }
    ]);