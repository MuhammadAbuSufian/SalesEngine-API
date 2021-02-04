"use strict";


angular.module("psapp")
    .controller("DashboardController",
    [
        "$scope", "$state", "GridData", "AuthenticationService", "DashboardService", "LocalStorageService",
        function($scope, $state, gridData, authenticationService, dashboardService, localStorageService) {

            $scope.mySelections = [];

            $scope.user = localStorageService.getUserInfo();

            if ($scope.user.Username === "exporter")
                $scope.data = dashboardService.exporterData();
            else
                $scope.data = gridData;


            var columnDefs = [
                {
                    field: "RequestId",
                    displayName: "Request Id",
                    cellTemplate:
                        "<a  ng-click=\"detail(row.entity)\" style=\"padding-left: 5px\" ng-bind=\"row.getProperty(col.field)\"></a>"
                },
                { field: "ExporterNo", displayName: "Exporter No" },
                { field: "ExporterName", displayName: "Exporter Name" },
                { field: "Date", displayName: "Date" },
                { field: "Quantity", displayName: "Quantity" },
                { field: "Amount", displayName: "Amount (BDT)" },
                {
                    field: "State.Name",
                    displayName: "State"
                }
            ];

            $scope.gridOptions = {
                data: "data",
                columnDefs: columnDefs,
                enablePinning: true,
                multiSelect: false,
                selectedItems: $scope.mySelections,

                //enableCellSelection: true,
                enableRowSelection: true
            };
            $scope.detail = function(row) {
                //alert(row.Id);

                if (row.State.Id === 1) {
                    $state.go("service.requested");
                } else if (row.State.Id === 2) {
                    $state.go("service.approved");
                } else if (row.State.Id === 3) {
                    $state.go("service.rejected");
                }
            };


        }
    ]);