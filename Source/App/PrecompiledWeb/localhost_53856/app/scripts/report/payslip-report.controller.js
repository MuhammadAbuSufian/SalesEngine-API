"use strict";

angular.module("app")
    .controller("PayslipReportController",
        [
            "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService",
            function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService) {
                "use strict";


                var config = function () {
                    baseService.setCurrentApi(urlService.ReportUrls.PayslipReport);
                    baseService.setIsLoadDataFromUrls(true);
                    baseService.setUrls([]);
                    baseService.setIsLoadPagingData(true);
                    baseService.setDataStatus("Active");
                    baseService.setCallerStatus(false);
                    $controller("BaseController", { $scope: $scope });
                }
                var init = function () {
                    $scope.start();
                };
                $scope.$on("parentResetEventCaller",
                    function (event, args) {
                        $scope.start();
                    });

               

                $scope.onSelect = function ($item, $model, $label) {
                    var item = $item;
                    var model = $model;
                    var label = $label;
                    $scope.searchRequest.ExporterId = $model.Id;
                };

                $scope.start = function () {
                    $scope.searchRequest.DateFrom = new Date();
                    $scope.searchRequest.DateTo = new Date();
                    $scope.serviceHours = [
                        { Id: 12, Name: "12" }, { Id: 24, Name: "24" }, { Id: 48, Name: "48" }
                    ];
                    $scope.serviceOffices = [
                        { Id: 5, Name: "All" }, { Id: 1, Name: "Dhaka" }, { Id: 2, Name: "Narayanganj" }
                    ];
                    $scope.filter();
                };

                $scope.filter = function () {
                    $scope.pagingRequest.DateFrom = $scope.searchRequest.DateFrom.toLocaleString();
                    $scope.pagingRequest.DateTo = $scope.searchRequest.DateTo.toLocaleString();
                    $scope.pagingRequest.ServiceHour = $scope.searchRequest.ServiceHour;
                    $scope.pagingRequest.ServiceOffice = $scope.searchRequest.ServiceOffice;
                    $scope.search();
                };

                $scope.openPrintModal = function (size, tpl, url) {
                    if (!url) url = $scope.currentApiUrl;
                    
                    var configuration = {
                        template: "app/views/modal/payslip-report-print.modal.tpl.html",
                        controller:"PayslipReportPrintModalInstanceController"
                    }
                    var action = "Print Preview";

                    var data = [];
                    data.list = $scope.data.list;
                   
                    data.searchRequest = $scope.searchRequest;
                    
                    alertService.printDialog(size, data, action, configuration)
                        .then(function (response) {
                           
                            console.log(response);
                            if (response.isConfirm) {
                                alertService.showAlert(alertService.alertType.success, "Success", false);
                            }
                        },
                            function (error) {
                                console.log(error);
                            });

                };

                config();
                init();

            }
        ]);