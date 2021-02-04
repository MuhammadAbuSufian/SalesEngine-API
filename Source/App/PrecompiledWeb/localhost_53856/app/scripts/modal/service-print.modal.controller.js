angular.module("app")
    .controller("ServicePrintModalInstanceController",
    [
        "$scope", "$uibModalInstance", "action", "data", "$controller", "CommonHttpService", "UrlService",
        "BaseService", "$rootScope",
        function ($scope, $uibModalInstance, action, data, $controller, commonHttpService, urlService, baseService, $rootScope) {
            "use strict";

            var config = function() {
                baseService.setCallerStatus(false);
                $controller("BaseController", { $scope: $scope });
            };
            config();

            $scope.serverUrl = urlService.baseUrl;

            $scope.action = action;
            $scope.data = data;
            $scope.data.today = new Date();


            $scope.loadExporter = function() {
                commonHttpService.find(urlService.ExporterUrls.Exporter, $scope.data.ExporterId)
                    .then(function(response) {
                            $scope.data.Exporter = response;
                            $scope.loadEpbService();
                        },
                        function(error) {
                            console.log(error);
                        });
            };
            $scope.loadExporter();

            $scope.loadEpbService = function() {
                commonHttpService.find(urlService.EpbServiceUrls.EpbService, $scope.data.EpbServiceId)
                    .then(function(response) {
                            $scope.data.EpbService = response;
                        },
                        function(error) {
                            console.log(error);
                        });
            };


            var updateExporterEpbService = function() {
                $scope.exporterEpbService.PrintCount += 1;
                commonHttpService.update(urlService.ExporterEpbServiceUrls.ExporterEpbService,
                        $scope.exporterEpbService)
                    .then(function(response) {
                            console.log(response);
                            $rootScope.$broadcast("reloadEventCaller");
                        },
                        function(error) {
                            console.log(error);
                        });
            };

            var loadExporterEpbService = function() {
                commonHttpService.find(urlService.ExporterEpbServiceUrls.ExporterEpbService, $scope.data.Id)
                    .then(function(response) {
                            $scope.exporterEpbService = response;
                            updateExporterEpbService();
                        },
                        function(error) {
                            console.log(error);
                        });
            };

            $scope.printWithExporterEpbServiceUpdate = function(printableDiv) {
                loadExporterEpbService();
                $scope.print(printableDiv);
            };


            $scope.response = {
                action: $scope.action,
                data: $scope.data,
                isConfirm: true
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.response);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };


        }
    ]);