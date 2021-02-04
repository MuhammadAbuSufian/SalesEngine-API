angular.module("app")
    .controller("AllServicePrintModalInstanceController", [
        "$scope", "$uibModalInstance", "action", "data", "$controller", "CommonHttpService", "UrlService", "BaseService", "$rootScope",
        function ($scope, $uibModalInstance, action, data, $controller, commonHttpService, urlService, baseService, $rootScope) {
            "use strict";

            var config = function () {
                baseService.setCallerStatus(false);
                $controller("BaseController", { $scope: $scope });
            };
            config();
            
            $scope.serverUrl = urlService.baseUrl;

            $scope.action = action;
            $scope.data = data;
            $scope.data.today = new Date();

            $scope.printableData = [];
            var filterPrintableData = function() {
                for (var i = 0; i < $scope.data.ExporterEpbServices.length; i++) {
                    if ($scope.data.ExporterEpbServices[i].IsUsed) continue;
                    if ($scope.data.ExporterEpbServices[i].PrintCount === 0 && $scope.user.RoleNames[0] === "DealingAssistance")
                        $scope.printableData.push($scope.data.ExporterEpbServices[i]);
                    if ($scope.user.RoleNames[0] === "SystemAdmin" || $scope.user.RoleNames[0] === "Admin")
                        $scope.printableData.push($scope.data.ExporterEpbServices[i]);
                }
            };
            filterPrintableData();

            $scope.loadServiceType = function() {
                commonHttpService.find(urlService.DropdownUrl +"?type=ServiceType")
                    .then(function(response) {
                            $scope.data.ServiceTypes = response;
                            $scope.loadServiceIssueType();
                        },
                        function(error) {
                            console.log(error);
                        });
            };
            $scope.loadServiceType();

            $scope.loadServiceIssueType = function() {
                commonHttpService.find(urlService.DropdownUrl + "?type=ServiceIssueType")
                    .then(function(response) {
                        $scope.data.ServiceIssueTypes = response;
                            $scope.loadExporter();
                        },
                        function(error) {
                            console.log(error);
                        });
            };
            
            $scope.loadExporter = function () {
                commonHttpService.find(urlService.ExporterUrls.Exporter, $scope.data.ExporterId)
                    .then(function (response) {
                        $scope.data.Exporter = response;
                    },
                        function (error) {
                            console.log(error);
                        });
            };


            $scope.response = {
                action: $scope.action,
                data: $scope.data,
                isConfirm: true
            };

            $scope.ok = function () {
                $uibModalInstance.close($scope.response);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss("cancel");
            };


            var updateEpbService = function() {
                $scope.epbService.PrintCount += 1;

                /*for (var i=0;i< $scope.epbService.ExporterEpbServices.length;i++) {
                    $scope.epbService.ExporterEpbServices[i].PrintCount += 1;
                }*/
                commonHttpService.update(urlService.EpbServiceUrls.EpbService,
                        $scope.epbService)
                    .then(function(response) {
                            console.log(response);
                            $rootScope.$broadcast("reloadEventCaller");
                        },
                        function(error) {
                            console.log(error);
                        });
            };

            var loadEpbService = function () {
                commonHttpService.find(urlService.EpbServiceUrls.EpbService, $scope.data.Id)
                    .then(function (response) {
                        $scope.epbService = response;
                        updateEpbService();
                    },
                        function (error) {
                            console.log(error);
                        });
            };


            $scope.printAllWithEpbServiceUpdate = function (printableDiv) {
                loadEpbService();
                $scope.print(printableDiv);
            };

        }
    ]);