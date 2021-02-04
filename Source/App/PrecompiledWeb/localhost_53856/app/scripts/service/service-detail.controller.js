angular.module("app")
    .controller("ServiceDetailController",
        [
            "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService",
            "$stateParams",
            function ($scope,
                $rootScope,
                urlService,
                $controller,
                baseService,
                commonHttpService,
                alertService,
                $stateParams) {
                "use strict";


                var config = function () {
                    baseService.setCurrentApi(urlService.EpbServiceUrls.EpbService);
                    baseService.setIsLoadDataFromUrls(false);
                    baseService.setUrls([]);
                    baseService.setIsLoadPagingData(false);
                    baseService.setDataStatus("Active");
                    baseService.setCallerStatus(false);
                    $controller("BaseController", { $scope: $scope });
                };
                var init = function () {
                    $scope.start();
                };


                //##region broadcust
                $scope.$on("parentResetEventCaller",
                    function (event, args) {
                        $scope.start();
                    });

                $scope.$on("afterLoadSingleDataEventCaller",
                    function (event, args) {
                        $scope.loadDropdown("serviceType");
                    });

                $scope.$on("serviceTypeDropdownSuccssLoadBroadcust",
                    function (event, args) {
                        $scope.loadDropdown("serviceIssueType");
                        $scope.exporterEpbServiceModel();
                    });
                //##region end broadcust            

                $scope.start = function () {
                    $scope.epbServiceModel();
                    $scope.exporterEpbServiceModel();

                    $scope.isDisableMofify = true;


                    if ($stateParams["id"]) {
                        $scope.edit($stateParams["id"]);
                    } 
                };

                $scope.epbServiceModel = function () {

                    if (!$scope.isUpdateMode) $scope.model.Id = $scope.guid();
                    $scope.model.PayslipPublicNumber = ""; //gen on server
                    //$scope.model.PayslipBarcodNumber = ""; //gen on server
                    $scope.model.BarcodeNumber = "";
                    $scope.model.ExporterId = "";
                    $scope.model.ExporterNumber = "";
                    $scope.model.ExporterName = "";
                    $scope.model.ServiceRequestedDate = new Date();
                    $scope.model.ServiceApprovedDate = new Date();
                    $scope.model.ServiceRejectedDate = new Date();
                    $scope.model.TotalServiceQuantity = 0; //calculation
                    $scope.model.TotalServiceAmount = 0; //calculation
                    $scope.model.ServiceStatus = 1;

                    $scope.model.ExporterEpbServices = [];

                    $scope.model.InitialCurrentBalance = 0; //fixed
                    $scope.model.AfterServiceTakenBalance = 0; //calculation
                };

                $scope.exporterEpbServiceModel = function () {
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
                        ServiceAmount: 0,


                    };
                };

                $scope.bindServiceCost = function () {
                    var obj = $scope.getObject($scope.exporterEpbService.ServiceIssueTypeId,
                        "Id",
                        $scope.data.serviceIssueNameDropdown);
                    $scope.exporterEpbService.ServiceAmount = parseFloat(obj.Common);
                };
                $scope.bindHourlyCost = function () {
                    var obj = $scope.getObject($scope.exporterEpbService.ServiceHour,
                        "Name",
                        $scope.data.serviceIssueNameHourlyCostDropdown);
                    $scope.exporterEpbService.ServiceAmount = parseFloat(obj.Common);
                };

                $scope.calculateServiceAmount = function () {
                    $scope.bindHourlyCost();
                    $scope.exporterEpbService.ServiceAmount = $scope.exporterEpbService.ServiceAmount *
                        $scope.exporterEpbService.Quantity;
                };


                var calculateTotal = function () {
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
                    $scope.model.AfterServiceTakenBalance = $scope.model.InitialCurrentBalance -
                        $scope.model.TotalServiceAmount;

                    $scope.isDisableMofify = false;
                };

                var validator = function () {
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
                };


                $scope.add = function () {
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

                        if (!$scope.isExistingListData)
                            $scope.model.ExporterEpbServices.push($scope.exporterEpbService);

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


                $scope.loadExporter = function () {
                    commonHttpService.call(urlService.ExporterUrls.Exporter + "?id=" + $scope.model.ExporterId)
                        .then(function (response) {
                            $scope.exporter = response;

                            $scope.exporterEpbService.ExporterId = $scope.exporter.Id;

                            $scope.model.ExporterNumber = $scope.exporter.ExporterNumber;
                            $scope.model.ExporterName = $scope.exporter.CompanyOrFactoryName;
                            $scope.model.InitialCurrentBalance = $scope.exporter.CurrentBalance;
                            $scope.model.AfterServiceTakenBalance = $scope.exporter.CurrentBalance;

                            calculateTotal();
                        },
                            function (error) {
                                console.log(error);
                            });
                };


                
                $scope.changeState = function (serviceStatus) {
                    
                    if (serviceStatus === 2)
                        //if (validator()) {
                        //    $scope.model.ServiceApprovedDate = new Date();
                        //}
                        //alert("Balance Insuffient");
                        $scope.model.ServiceApprovedDate = new Date();

                    if (serviceStatus === 3)
                        $scope.model.ServiceRejectedDate = new Date();

                    $scope.model.ServiceStatus = serviceStatus;

                    $scope.update();
                    
                };


                $scope.openPrintModal = function (size, id, tpl, ctrl) {
                    $scope.printModal(size, id, tpl, ctrl, urlService.ExporterEpbServiceUrls.ExporterEpbService);
                };

                $scope.openAllPrintModal = function (size, id, tpl, ctrl) {
                    $scope.printModal(size, id, tpl, ctrl, urlService.EpbServiceUrls.EpbService);
                };


                $scope.$on("reloadEventCaller",
                    function (event, args) {
                        config();
                        init();
                    });


                $scope.updateUsedReferance = function (payslip) {
                    if (payslip.IsUsed) {
                        payslip.IsUsed = false;
                        payslip.UsedReference = "";
                    }
                    else {
                        payslip.IsUsed = true;
                    }
                    $scope.promise = commonHttpService.update(urlService.ExporterEpbServiceUrls.ExporterEpbService,
                        payslip)
                        .then(function (response) {
                            console.log(response);
                            $rootScope.$broadcast("reloadEventCaller");
                        },
                            function (error) {
                                console.log(error);
                            });
                };

                $scope.showLog = function (size, data, action) {

                    var configuration = {
                        template: "app/views/modal/log.modal.tpl.html",
                        controller: "LogModalInstanceController"
                    };

                    alertService.showConfirmDialog(size, data, action, configuration)
                        .then(function (response) {
                            console.log(response);
                            if (response.isConfirm) {
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                };


                $scope.trashExporterEbpService = function (size, data, action) {


                    alertService.showConfirmDialog(size, data, action, false)
                        .then(function (response) {
                            console.log(response);
                            if (response.isConfirm) {
                                remove("Trash", response.data.Id);
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                }


                var remove = function (actionType, id) {
                    $scope.promise = commonHttpService.remove(urlService.ExporterEpbServiceUrls.ExporterEpbService, actionType, id)
                        .then(function (data) {
                            //console.log(data);
                            init();
                            //$rootScope.$broadcast("afterRemoveEventCaller");
                            //$rootScope.$broadcast("parentResetEventCaller");
                            alertService.showAlert(alertService.alertType.success, "Success", false);
                        },
                            function (error) {
                                //console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Error Occured !! Not Delete ", true);
                            });
                };

                $scope.filter = function () {

                    $scope.pagingRequest.ServiceStatus = $scope.searchRequest.ServiceStatus;

                    $scope.currentApiUrl = urlService.ExporterEpbServiceUrls.ExporterEpbService;

                    $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;

                    $scope.search();
                    
                };

                config();
                init();
            }
        ]);