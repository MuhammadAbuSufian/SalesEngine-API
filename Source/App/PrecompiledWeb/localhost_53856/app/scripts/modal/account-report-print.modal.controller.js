 angular.module("app")
    .controller("AccountReportPrintModalInstanceController", [
        "$scope", "$uibModalInstance", "action", "data", "$controller", "CommonHttpService", "UrlService","$filter",
        function ($scope, $uibModalInstance, action, data, $controller, commonHttpService, urlService,$filter) {
            "use strict";

            $controller("BaseController", { $scope: $scope });

            $scope.action = action;
            $scope.data = data;
            $scope.data.today = new Date();
            var total = 0;
            $scope.loadReport = function () {
                commonHttpService.find(urlService.ReportUrls.AccountReport, JSON.stringify($scope.data.searchRequest))
                    .then(function(response) {
                        $scope.data.list = response;

                          for(var i =0; i < response.length; i++)
                            {
                               total = total +  response[i].TotalAmount;
                            }
                            $scope.totalAmount = total;
                        },
                        function(error) {
                            console.log(error);
                        });
            };
            $scope.loadReport();

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