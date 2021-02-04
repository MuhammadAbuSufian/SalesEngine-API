"use strict";
angular.module("app")
    .controller("ServiceController",
    [
        "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService", "$state",
        function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService, $state) {
            "use strict";


            var config = function() {
                baseService.setCurrentApi(urlService.EpbServiceUrls.EpbService);
                baseService.setIsLoadDataFromUrls(true);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            };
            var init = function() {
                $scope.addCall(urlService.EpbServiceUrls.EpbService);
                $scope.addCall(urlService.DropdownUrl + "?type=ServiceType");
                $scope.addCall(urlService.DropdownUrl + "?type=ServiceIssueType");
                //$scope.addCall(urlService.DropdownUrl + "?type=Exporter");

                var dataProperties = ["list", "serviceTypeDropdown", "serviceIssueTypeDropdown"]; //, "exporterDropdown"

                baseService.setUrls($scope.urls);
                baseService.setDataProperties(dataProperties);

                $scope.baseCaller();
                $scope.start();
            };

            $scope.$on("parentResetEventCaller",
                function(event, args) {
                    //$scope.start();
                });

            $scope.$on("afterSaveEventCaller",
                function(event, args) {
                    //$state.go("root.dashboard");
                    $scope.detail($scope.response);
                });


            $scope.start = function() {

                $scope.serviceStatues = [
                    { Id: 0, Name: "All" }, { Id: 1, Name: "Requested" }, { Id: 4, Name: "Modified" }, { Id: 2, Name: "Accepted" }, { Id: 3, Name: "Rejected" }
                ];

                $scope.searchRequest.ServiceStatus = 0;

                if (!$scope.isUpdateMode) $scope.model.Id = $scope.guid();
                $scope.model.PayslipPublicNumber = ""; //gen on server
                $scope.model.PayslipBarcodNumber = ""; //gen on server
                $scope.model.ExporterId = "";
                $scope.model.ExporterNumber = "";
                $scope.model.ExporterName = "";
                $scope.model.ServiceRequestedDate = new Date();
                $scope.model.ServiceApprovedDate = new Date();
                $scope.model.ServiceRejectedDate = new Date();
                $scope.model.TotalServiceQuantity = 0; //calculation
                $scope.model.TotalServiceAmount = 0; //calculation
                $scope.model.ServiceStatus = 1; //always requested

                $scope.model.ExporterEpbServices = [];

                $scope.model.ExporterEpbServices.PayslipNumber = 0;

                $scope.model.InitialCurrentBalance = 0; //fixed
                $scope.model.AfterServiceTakenBalance = 0; //calculation

                $scope.exporterEpbServiceModel();
            };

            $scope.exporterEpbServiceModel = function() {
                $scope.exporterEpbService = {
                    Id: $scope.guid(),
                    Created: new Date(),
                    CreatedBy: $scope.user.Id,
                    Modified: new Date(),
                    ModifiedBy: $scope.user.Id,
                    Active: true,

                    EpbServiceId: $scope.model.Id,
                    ExporterId: $scope.model.ExporterId,
                    Quantity: 1,
                    ServiceHour: 0,
                    ServiceAmount: 0
                };
            };

            $scope.onSelect = function($item, $model, $label) {
                var item = $item;
                var model = $model;
                var label = $label;
                $scope.model.ExporterId = $model.Id;
                $scope.loadExporter();
            };

            $scope.loadExporter = function() {
                commonHttpService.call(urlService.ExporterUrls.Exporter + "?id=" + $scope.model.ExporterId)
                    .then(function(response) {
                            $scope.exporter = response;

                            $scope.exporterEpbService.ExporterId = $scope.exporter.Id;

                            $scope.model.ExporterNumber = $scope.exporter.ExporterNumber;
                            $scope.model.ExporterName = $scope.exporter.CompanyOrFactoryName;
                            $scope.model.InitialCurrentBalance = $scope.exporter.CurrentBalance;
                            $scope.model.AfterServiceTakenBalance = $scope.exporter.CurrentBalance;

                            $scope.isExpiredExporter();
                        },
                        function(error) {
                            console.log(error);
                        });
            };

            $scope.loadServiceIssueTypes = function () {
                $scope.data.serviceIssueNameHourlyCostDropdown = [];
                commonHttpService.call(urlService.DropdownUrl + "?type=ServiceIssueName&id=" + $scope.exporterEpbService.ServiceTypeId)
                    .then(function(response) {
                        $scope.data.serviceIssueNameDropdown = response;
                            var serviceIssueType = $scope.getObject("True", "ExtraData", $scope.data.serviceIssueNameDropdown);
                            if (serviceIssueType.Id) {
                                $scope.exporterEpbService.ServiceIssueTypeId = serviceIssueType.Id;
                                $scope.loadDropdown("serviceIssueNameHourlyCost", $scope.exporterEpbService.ServiceIssueTypeId);
                                $scope.bindServiceCost();
                            }
                        },
                        function(error) {
                            console.log(error);
                        });
            };

            $scope.isExpiredExporter = function() {
                if ($scope.exporter !== undefined) {
                    $scope.today = new Date();
                    $scope.periodofValidation = new Date($scope.exporter.PeriodofValidation);
                    //if ($scope.periodofValidation.getTime() >= $scope.today.getTime()) {
                    if ($scope.periodofValidation.getTime() <= new Date("2017-12-31 23:59:59")) {
                        return false;
                    }
                    else if ($scope.periodofValidation.getTime() >= $scope.today.getTime()) {
                        return false;
                    }
                   else {
                        return true;
                    }
                }
            };


            $scope.resetServiceQuantity = function () {
                $scope.exporterEpbService.Quantity = 1;
            }

            $scope.bindServiceCost = function () {
                var obj = $scope.getObject($scope.exporterEpbService.ServiceIssueTypeId, "Id", $scope.data.serviceIssueNameDropdown);
                $scope.exporterEpbService.ServiceAmount = parseFloat(obj.Common);
            };
            $scope.bindHourlyCost = function () {                
                var obj = $scope.getObject($scope.exporterEpbService.ServiceHour, "Name", $scope.data.serviceIssueNameHourlyCostDropdown);
                if (obj.Common) {
                    $scope.exporterEpbService.ServiceAmount = parseFloat(obj.Common);
                }
                else {
                    $scope.bindServiceCost();
                }
            };


            $scope.calculateServiceAmount = function() {
                $scope.bindHourlyCost();
                $scope.exporterEpbService.ServiceAmount = $scope.exporterEpbService.ServiceAmount *
                    $scope.exporterEpbService.Quantity;
            };


            var calculateTotal = function() {
                //TotalServiceQuantity
                //TotalServiceAmount
                //AfterServiceTakenBalance

                $scope.model.TotalServiceQuantity = 0;
                $scope.model.TotalServiceAmount = 0;
                $scope.model.AfterServiceTakenBalance = 0;
                for (var i = 0; i < $scope.model.ExporterEpbServices.length; i++) {
                    $scope.model.TotalServiceQuantity += $scope.model.ExporterEpbServices[i].Quantity;
                    $scope.model.TotalServiceAmount += $scope.model.ExporterEpbServices[i].ServiceAmount;
                }
                $scope.model.AfterServiceTakenBalance = $scope.model.InitialCurrentBalance - $scope.model.TotalServiceAmount;

                if ($scope.model.ExporterEpbServices.length==0) {
                    $scope.isExistingListData = false;
                }
            };

            var validator = function () {
                if ($scope.isExpiredExporter()) {
                    alertService.showAlert(alertService.alertType.danger,
                            "Your registration is expired. Please contact with EPB.",
                            true);
                    return false;
                } else {
                    if ($scope.model.ExporterId) {
                        if ($scope.model.InitialCurrentBalance >=
                        ($scope.model.TotalServiceAmount + $scope.exporterEpbService.ServiceAmount)) {
                            return true;
                        } else {
                            alertService.showAlert(alertService.alertType.danger,
                                "Your are unable to add more service due to insufficient balance. Please try later after request and verify more balance",
                                true);
                            return false;
                        }

                    } else {
                        alertService.showAlert(alertService.alertType.danger,
                            "Please select an exporter first",
                            true);
                        return false;
                    }
                }
            };

            $scope.add = function() {
                $scope.model.Cost = 0;
                $scope.isExistingListData = false;

                if (validator()) {
                    for (var i = 0; i < $scope.model.ExporterEpbServices.length; i++) {
                        if ($scope.model.ExporterEpbServices[i].ServiceTypeId ===
                            $scope.exporterEpbService.ServiceTypeId &&
                            $scope.model.ExporterEpbServices[i].ServiceIssueTypeId ===
                            $scope.exporterEpbService.ServiceIssueTypeId &&
                            $scope.model.ExporterEpbServices[i].ServiceHour === $scope.exporterEpbService.ServiceHour)
                            $scope.isExistingListData = true;
                        
                    }

                    if (!$scope.isExistingListData) {
                        $scope.model.ExporterEpbServices.push(angular.copy($scope.exporterEpbService));
                        $scope.isExistingListData = true;
                    }
                        

                    alertService.showAlert(alertService.alertType.success,
                        "Added",
                        false);

                    $scope.exporterEpbServiceModel();
                    calculateTotal();
                }
            };


            $scope.remove = function (l) {
                for (var i = 0; i < $scope.model.ExporterEpbServices.length; i++) {
                    if ($scope.model.ExporterEpbServices[i].Id === l.Id &&
                        $scope.model.ExporterEpbServices[i].ServiceTypeId === l.ServiceTypeId &&
                        $scope.model.ExporterEpbServices[i].ServiceIssueTypeId === l.ServiceIssueTypeId &&
                        $scope.model.ExporterEpbServices[i].ServiceHour === l.ServiceHour)
                        $scope.model.ExporterEpbServices.splice(i, 1);
                }
                calculateTotal();
            };


            $scope.detail = function(id) {
                $state.go("root.service-detail", { id: id });
            };

            $scope.accept = function(size, data, action) {
                alertService.showConfirmDialog(size, data, action, false)
                   .then(function (response) {
                       console.log(response);
                       if (response.isConfirm) {
                           $scope.changeState(2);
                       }
                   },
                       function (error) {
                           console.log(error);
                       });
            };

            $scope.changeState = function (serviceStatus) {
                $scope.model.ServiceStatus = serviceStatus;
                $scope.update();
            };


            $scope.filter = function() {
                $scope.pagingRequest.ServiceStatus = $scope.searchRequest.ServiceStatus;

                $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                $scope.search();
            };


            config();
            init();


        }
    ]);