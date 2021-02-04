"use strict";


angular.module("app")
    .controller("DashboardController",
    [
        "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService", "$state", "LocalDataStorageService",
        function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService, $state, localDataStorageService) {
            "use strict";


            var config = function () {
                baseService.setCurrentApi(urlService.EpbServiceUrls.EpbService);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            };
            var init = function () {                
                $scope.start();
            };           


            $scope.start = function () {

                $scope.serviceStatues = [
                    { Id: 0, Name: "All" }, { Id: 1, Name: "Requested" }, { Id: 4, Name: "Modified" }, { Id: 2, Name: "Accepted" }, { Id: 3, Name: "Rejected" }
                ];

                $scope.searchRequest.ServiceStatus = 0;

                localStorage.setItem("Controller", angular.toJson("Dashboard"));
               
            };


            $scope.detail = function (id) {
                $state.go("root.service-detail", { id: id });
            };


            $scope.filter = function () {
                $scope.pagingRequest.ServiceStatus = $scope.searchRequest.ServiceStatus;

                $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                $scope.search();
            };

            $scope.trashEpbService = function (size, data, action) {
                data.Name = "Requested ID " + data.PayslipRequestId + " of " + data.ExporterNumber + "(" + data.ExporterName + ")";
                $scope.trash(size, data, action);
            };



            config();
            init();


        }
    ]);