"use strict";


angular.module("app")
    .controller("ImportLogController",
        [
            "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "$timeout", "AlertService", "$location",
            function ($scope, $rootScope, urlService, $controller, baseService, $timeout, alertService, $location) {
                "use strict";


                var config = function () {
                    
                    baseService.setCurrentApi(urlService.ImportLogUrls.ImportLog);
                    baseService.setIsLoadDataFromUrls(false);
                    baseService.setUrls([]);
                    baseService.setIsLoadPagingData(true);
                    baseService.setDataStatus("Active");
                    $controller("BaseController", { $scope: $scope });
                }

                var init = function () {
                    $scope.start();
                };
                
                //$scope.$on("parentResetEventCaller",
                //    function (event, args) {
                //        $scope.start();
                //    });


                $scope.start = function () {
                    $scope.searchRequest.OrderBy = "TransectionId";

                    $scope.searchRequest.Date = new Date();
                };

                $scope.backToAccount = function (model) {
                    console.log(model);
                    baseService.setTransectionID(model.TransectionId);
                    baseService.setTransectionDate(model.TransectionDate);

                    $location.path("/account");
                }

                $scope.filter = function () {
                    //$scope.pagingRequest.Date = $scope.searchRequest.Date;

                    $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                    $scope.search();
                }


                config();
                init();

            }
        ]);