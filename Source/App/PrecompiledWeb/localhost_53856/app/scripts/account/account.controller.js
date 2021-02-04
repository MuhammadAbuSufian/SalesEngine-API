"use strict";


angular.module("app")
    .controller("AccountController",
    [
         "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService","$filter",
        function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService, $filter) {
            "use strict";


            var config = function () {
                baseService.setCurrentApi(urlService.AccountUrls.EpbAccount);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            }

            var init = function () {
                //$scope.addCall(urlService.AccountUrls.EpbAccount);
                //$scope.addCall(urlService.DropdownUrl + "?type=Exporter");

                //var dataProperties = ["list", "exporterDropdown"];

                //baseService.setUrls($scope.urls);
                //baseService.setDataProperties(dataProperties);

                //baseService.setCallerStatus(true);
                //$scope.baseCaller();
                $scope.start();
            };

            

            $scope.start = function () {
                $scope.transectionStatus = [
                    { Id: 0, Name: "All" }, { Id: 1, Name: "Pending" }, { Id: 2, Name: "Verified" }
                ];

                $scope.paymentMethods = [{ Id: 1, Name: "Bank Deposit" }, { Id: 2, Name: "Payment Order" }];

                $scope.banks = [
                    { Id: 1, Name: "Brac Bank Limited", AccountNumber: "1503202417031001", AccountName: "Export Promotion Bureau" },
                    { Id: 2, Name: "Janata Bank Limited", AccountNumber: "004000442", AccountName: "EPB Narayangonj" }
                ];
                
                $scope.SelectedExporter = "";
                $scope.exporter = {};

                $scope.searchRequest.TransectionStatus = 0;

                $scope.model.AccountNumber = "1503202417031001";
                $scope.model.AccountName = "Export Promotion Bureau";
                $scope.model.BankName = "Brac Bank Limited";
                $scope.model.BranchName = "";
                $scope.model.DepositDate = new Date();
                $scope.model.PaymentMethod = 1;
                $scope.model.TransectionStatus = 1;

                if (baseService.getTransectionDate() && baseService.getTransectionID()) {
                    console.log('setting date');
                    console.log(baseService.getTransectionDate());
                    $scope.model.DepositDate = baseService.getTransectionDate();
                    console.log($scope.model.DepositDate);
                    $scope.model.TransectionNumber = baseService.getTransectionID();
                    console.log('done');
                }
               
                $scope.isVerifyMode = false;
                $scope.isPayOrder = false;
            };

            $scope.toggleFields = function() {
                if ($scope.model.PaymentMethod === 1) {
                    $scope.model.AccountNumber = "1503202417031001";
                    $scope.model.AccountName = "Export Promotion Bureau";
                    $scope.model.BankName = "Brac Bank Limited";

                    $scope.isPayOrder = false;
                }
                    
                if ($scope.model.PaymentMethod === 2) {
                    $scope.model.AccountNumber = "";
                    $scope.model.AccountName = "";
                    $scope.model.BankName = "";
                    $scope.isPayOrder = true;
                }
                   
            };

            var getBank = function(bankName) {
                for (var i = 0; i < $scope.banks.length; i++) {
                    if ($scope.banks[i].Name === bankName)
                        return $scope.banks[i];
                }
                return null;
            };

            $scope.bindBankProperties = function() {
                var bank = getBank($scope.model.BankName);
                $scope.model.AccountNumber = bank.AccountNumber;
                $scope.model.AccountName = bank.AccountName;
            };

            $scope.transactionAmount = function (model) {
                //alert(Id);
                model.TransectionDate = model.DepositDate;
                model.TransectionId = model.TransectionNumber;
                if (model.TransectionId) {
                    console.log('sending DATA');
                    console.log($scope.model.DepositDate);
                    commonHttpService.post(urlService.ImportUrls.GetByTransaction, model)
                        .then(function(response) {
                                console.log(response);
                            if (response) {
                                $scope.model.TotalAmount = response.Deposit;
                                alertService.showAlert(alertService.alertType.success, " Amount Found Successfully", true);
                            } else {
                                $scope.model.TotalAmount = "";
                                alertService.showAlert(alertService.alertType.danger, "This " + model.TransectionId + " and " + $filter('date')(model.DepositDate, "(dd-MMMM-yyyy)") + "  have no Amount", true);
                            }
                              
                            },
                            function(error) {
                                console.log(error);
                            });
                    
                } else {
                    $scope.model.TotalAmount = "";
                    //  alert("Please enter a valid Transection Id / Deposit Slip No");
                    alertService.showAlert(alertService.alertType.danger, "Invalid Transection Date and Deposit Slip No", true);
                }
                
                    

            };

            $scope.onSelect = function ($item, $model, $label) {
                var item = $item;
                var model = $model;
                var label = $label;
                $scope.model.ExporterId = $item.Id;
                $scope.loadExporter();
            };

            $scope.loadExporter = function() {
                commonHttpService.call(urlService.ExporterUrls.Exporter + "?id=" + $scope.model.ExporterId)
                    .then(function(response) {
                            $scope.exporter = response;
                            if (!$scope.isUpdateMode && $scope.model.ExporterId !== "") {
                                $scope.model.DepositorName = $scope.exporter.CommercialManagerName;
                                $scope.model.DepositorAddress = $scope.exporter.CommercialManagerAddress;
                                $scope.model.DepositorPhone = $scope.exporter.CommercialManagerPhone;
                            }

                            $scope.SelectedExporter = { Id: $scope.exporter.Id, Name: $scope.exporter.ExporterNumber };
                        },
                        function(error) {
                            console.log(error);
                        });
            };


            $scope.checkAvailability = function() {
                commonHttpService.post(urlService.AccountUrls.CheckAvailability, $scope.model)
                    .then(function(response) {

                            if (!response.IsSuccess) {
                                if ($scope.isUpdateMode && response.Count === 1) {
                                    $scope.save();
                                    
                                } else {
                                    alertService.showAlert(alertService.alertType.danger, response.Message, true);
                                }
                            } else if (response.IsSuccess && response.Warning !== "") {
                                //alertService.showAlert(alertService.alertType.warning, response.Warning, true);

                                $scope.continue("md", response.Warning, "continue");

                            } else {
                                $scope.save();
                            }
                        },
                        function(error) {
                            console.log(error);
                        });
            };


            $scope.continue = function (size, data, action) {

                var configuration = {
                    template: "app/views/modal/continue.modal.tpl.html",
                    controller: "ConfirmModalInstanceController"
                }

                alertService.showConfirmDialog(size, data, action, configuration)
                    .then(function (response) {
                        console.log(response);
                        if (response.isConfirm) {
                            $scope.save();
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            };

            $scope.trashAccount = function (size, data, action) {

                var configuration = {
                    template: "app/views/modal/exporter-confirm.modal.tpl.html",
                    controller: "ConfirmModalInstanceController"
                }

                $scope.trash(size, data, action, configuration);
            };


            $scope.$on("runUpdateEvent",
                function(event, args) {
                    if ($scope.isVerifyMode) {
                        $scope.model.TransectionStatus = 2;
                        $scope.update();
                    }
                    $scope.isVerifyMode = false;                    
                });

            $scope.verify = function (size, data, action) {
                data.Name = data.EpbPaymentReceiptNumber;


                var configuration = {
                    template: "app/views/modal/exporter-confirm.modal.tpl.html",
                    controller: "ConfirmModalInstanceController"
                }

                alertService.showConfirmDialog(size, data, action, configuration)
                    .then(function(response) {
                            console.log(response);
                            if (response.isConfirm) {
                                $scope.loadSingleData(data.Id);
                                $scope.isVerifyMode = true;
                            }
                        },
                        function(error) {
                            console.log(error);
                        });
            };

           

            $scope.filter = function () {
                $scope.pagingRequest.TransectionStatus = $scope.searchRequest.TransectionStatus;

                $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                $scope.search();
            }


            $scope.$on("parentResetEventCaller",
                function (event, args) {
                    $scope.start();
                });

            $scope.$on("afterLoadSingleDataEventCaller",
                function (event, args) {
                    //$scope.SelectedExporter = $scope.getObject($scope.model.ExporterId, "Id", $scope.data.exporterDropdown);
                    $scope.model.DepositDate = new Date($scope.model.DepositDate.toString());
                    if(!$scope.isVerifyMode) $scope.loadExporter();
                });


            //$scope.$on("displayErrorBroadcust",
            //    function (event, args) {
            //        alertService.showAlert(alertService.alertType.danger, args.Message, true);
            //    });

            //$scope.$on("displayWarningBroadcust",
            //    function (event, args) {
            //        alertService.showAlert(alertService.alertType.warning, args, true);
            //    });
  

            config();
            init();


        }
    ]);