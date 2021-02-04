"use strict";


angular.module("app")
    .controller("ServiceIssueTypeController",
    [
        "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "$http",
        function($scope, $rootScope, urlService, $controller, baseService, $http) {
            "use strict";


            var config = function() {
                baseService.setCurrentApi(urlService.ServiceIssueTypeUrls.ServiceIssueType);
                baseService.setIsLoadDataFromUrls(true);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(false);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            }

            var init = function() {
                $scope.addCall(urlService.ServiceIssueTypeUrls.ServiceIssueType);
                $scope.addCall(urlService.DropdownUrl + "?type=ServiceType");
                $scope.addCall(urlService.DropdownUrl + "?type=IssueName");

                var dataProperties = ["list", "serviceTypeDropdown", "issueNameDropdown"];

                baseService.setUrls($scope.urls);
                baseService.setDataProperties(dataProperties);

                $scope.baseCaller();
                $scope.start();
            };

            $scope.$on("parentResetEventCaller",
                function(event, args) {
                    $scope.start();
                });

            $scope.$on("afterLoadSingleDataEventCaller",
                function (event, args) {
                    $scope.serviceIssueTypeHourlyCostModel();

                    //$scope.checkIssueName();
                });

            $scope.start = function () {
                $scope.booleanStatus = [{ Id: true, Name: "Yes" }, { Id: false, Name: "No" }];
                $scope.hours = [{ Id: 12, Name: "12" }, { Id: 24, Name: "24" }, { Id: 48, Name: "48" }];

                //$scope.serviceIssues = [
                //    "Certificate Issue", "Re-Issue", "Duplicate Issue", "Certificate Cancel", "New Registration",
                //    "Registration Renewal", "Registration Renewal(Late)", "Others", "Not Listed"
                //];

                if (!$scope.isUpdateMode) $scope.model.Id = $scope.guid();
                $scope.model.ServiceTypeId = "";
                $scope.model.Name = "";

                //$scope.model.NewName = "";

                $scope.model.IsHourlyService = false;
                $scope.model.IsMostSaleableService = false;
                $scope.model.Cost = 0;                
                $scope.model.ServiceIssueTypeHourlyCosts = [];

                $scope.serviceIssueTypeHourlyCostModel();

                //$scope.addCustomName = false;
                //$scope.removeCustomName = false;
            };
            

            $scope.serviceIssueTypeHourlyCostModel = function() {
                $scope.serviceIssueTypeHourlyCost = {
                    Id: $scope.guid(),
                    Created: new Date(),
                    CreatedBy: $scope.user.Id,
                    Modified: new Date(),
                    ModifiedBy: $scope.user.Id,
                    Active : true,

                    ServiceIssueTypeId: $scope.model.Id,
                    ServiceHour: "",
                    Cost : 0
                };
            };

            //$scope.checkIssueName = function() {
            //    $scope.addCustomName = false;
            //    if ($scope.model.Name === "Not Listed") {
            //        $scope.addCustomName = true;
            //    }
            //    else if ($scope.isUpdateMode && ($scope.model.NewName !== "" && $scope.model.NewName !== null && $scope.model.NewName !== undefined) && !$scope.removeCustomName) {
            //        $scope.addCustomName = true;
            //        $scope.bindNewType();
            //    }
            //    else if ($scope.removeCustomName) {
            //        $scope.model.NewName = "";
            //    }
            //};

            //$scope.resetIssueName = function() {
            //    $scope.addCustomName = false;
            //    $scope.removeCustomName = true;
            //    $scope.model.Name = "";
            //}

            //$scope.bindNewType = function() {
            //    $scope.model.Name = $scope.model.NewName;
            //};


            $scope.add = function () {
                $scope.model.Cost = 0;
                $scope.isExistingListData = false;
                for (var i = 0; i < $scope.model.ServiceIssueTypeHourlyCosts.length; i++) {
                    if ($scope.model.ServiceIssueTypeHourlyCosts[i].ServiceHour ===
                        $scope.serviceIssueTypeHourlyCost.ServiceHour) {
                        $scope.isExistingListData = true;

                        $scope.model.ServiceIssueTypeHourlyCosts[i].Cost = $scope.serviceIssueTypeHourlyCost.Cost;
                    }
                        
                }

                if (!$scope.isExistingListData)
                    $scope.model.ServiceIssueTypeHourlyCosts.push($scope.serviceIssueTypeHourlyCost);

                $scope.serviceIssueTypeHourlyCostModel();
            };


            $scope.remove = function (l) {
                for (var i = 0; i < $scope.model.ServiceIssueTypeHourlyCosts.length; i++) {
                    if ($scope.model.ServiceIssueTypeHourlyCosts[i].Id === l.Id &&
                        $scope.model.ServiceIssueTypeHourlyCosts[i].ServiceHour === l.ServiceHour)
                        $scope.model.ServiceIssueTypeHourlyCosts.splice(i, 1);
                }
            };

            $scope.filter = function () {
                $scope.searchRequest.ServiceTypeId = $scope.model.ServiceTypeId;
                $scope.searchRequest.Name = $scope.model.Name;

                $scope.pagingRequest.ServiceTypeId = $scope.searchRequest.ServiceTypeId;
                $scope.pagingRequest.Name = $scope.searchRequest.Name;

                $scope.search();
            };

            config();
            init();


        }
    ]);