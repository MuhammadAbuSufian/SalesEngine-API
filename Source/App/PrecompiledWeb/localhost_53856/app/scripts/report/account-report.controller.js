"use strict";


angular.module("app")
    .controller("AccountReportController",
    [
         "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService",
        function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService) {
            "use strict";


            var config = function () {
                baseService.setCurrentApi(urlService.ReportUrls.AccountReport);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(false);
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
                $scope.searchRequest.SelectedExporter = null;

                $scope.searchRequest.DateFrom = new Date();
                $scope.searchRequest.DateTo = new Date();
                $scope.searchRequest.ExporterId = "";
                $scope.searchRequest.AuditThrashold = 0;
                $scope.searchRequest.OrderBy = "DepositDate";
                $scope.serviceOffices = [
                    {Id:5, Name:"All"}, { Id: 1, Name: "Dhaka" }, { Id: 2, Name: "Narayanganj" }
                ];
                //$scope.loadDropdown("exporter");
                $scope.filter();
            };


            $scope.onSelect = function ($item, $model, $label) {
                var item = $item;
                var model = $model;
                var label = $label;
                $scope.searchRequest.ExporterId = $model.Id;
            };

            $scope.filter = function() {
                $scope.pagingRequest.DateFrom = $scope.searchRequest.DateFrom.toLocaleString();
                $scope.pagingRequest.DateTo = $scope.searchRequest.DateTo.toLocaleString();
                $scope.pagingRequest.ExporterId = $scope.searchRequest.ExporterId;
                $scope.pagingRequest.AuditThrashold = $scope.searchRequest.AuditThrashold;

                $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                $scope.search();
            };            


            $scope.openPrintModal = function(size, tpl, ctrl, url) {
                if (!url) url = $scope.currentApiUrl;

                var configuration = {
                    template: "app/views/modal/" + tpl + "print.modal.tpl.html",
                    controller: ctrl + "PrintModalInstanceController"
                }
                var action = "Print Preview";

                var data = [];
                data.list = $scope.data.list;
                data.searchRequest = $scope.searchRequest;

                alertService.printDialog(size, data, action, configuration)
                    .then(function(response) {
                            console.log(response);
                            if (response.isConfirm) {
                                alertService.showAlert(alertService.alertType.success, "Success", false);
                            }
                        },
                        function(error) {
                            console.log(error);
                        });

            };

            config();
            init();

        }
    ]);