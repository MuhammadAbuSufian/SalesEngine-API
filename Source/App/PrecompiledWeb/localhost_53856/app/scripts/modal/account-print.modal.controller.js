angular.module("app")
    .controller("AccountPrintModalInstanceController", [
        "$scope", "$uibModalInstance", "action", "data", "$controller", "CommonHttpService", "UrlService",
        function ($scope, $uibModalInstance, action, data, $controller, commonHttpService, urlService) {
            "use strict";

            $controller("BaseController", { $scope: $scope });

            $scope.action = action;
            $scope.data = data;
            $scope.data.today = new Date();

            $scope.loadExporter = function (){
                commonHttpService.find(urlService.ExporterUrls.Exporter, $scope.data.ExporterId)
                    .then(function(response) {
                        $scope.data.Exporter = response;

                        },
                        function(error) {
                            console.log(error);
                        });
            };
            $scope.loadExporter();

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

        }
    ]);