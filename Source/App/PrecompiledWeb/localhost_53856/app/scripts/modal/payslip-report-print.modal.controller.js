angular.module("app")
    .controller("PayslipReportPrintModalInstanceController", [
        "$scope", "$uibModalInstance", "action", "data", "$controller", "CommonHttpService", "UrlService", "$filter",
        function ($scope, $uibModalInstance, action, data, $controller, commonHttpService, urlService, $filter) {
            "use strict";

            $controller("BaseController", { $scope: $scope });

            $scope.action = action;
            $scope.data = data;
            $scope.data.today = new Date();
            var total = 0;

            for (var i = 0; i < data.list.length; i++) {
                total = total + data.list[i].ServiceAmount;
            }
            $scope.totalAmount = total;

            //$scope.response = {
            //    action: $scope.action,
            //    data: $scope.data,
            //    isConfirm: true
            //};

            $scope.ok = function () {
                $uibModalInstance.close($scope.response);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss("cancel");
            };

        }
    ]);