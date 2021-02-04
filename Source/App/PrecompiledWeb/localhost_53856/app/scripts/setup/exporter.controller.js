"use strict";


angular.module("app")
    .controller("ExporterController",
    [
        "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService",
        function($scope, $rootScope, urlService, $controller, baseService, commonHttpService) {
            "use strict";

            var config = function() {
                baseService.setCurrentApi(urlService.ExporterUrls.Exporter);
                baseService.setIsLoadDataFromUrls(false);
                baseService.setUrls([]);
                baseService.setIsLoadPagingData(true);
                baseService.setDataStatus("Active");
                baseService.setCallerStatus(true);
                $controller("BaseController", { $scope: $scope });
            }

            var init = function() {
                //$scope.addCall(urlService.HomeUrls.RmgSearch);
                ////$scope.addCall(urlService.DropdownUrl + "?type=Category");

                //var dataProperties = ["list"];

                //baseService.setUrls($scope.urls);
                //baseService.setDataProperties(dataProperties);

                //$scope.baseCaller();
                $scope.start();
            };

           

            $scope.start = function () {
                $scope.factoryTypes = ["Knit", "Woven", "Knit & Woven", "Others"];
                $scope.exporterType = [{ Id: 1, Name: "Textile" }, { Id: 2, Name: "Non-Textile" }];
                $scope.serviceOffices = [
                    { Id: 1, Name: "Dhaka" }, { Id: 2, Name: "Narayanganj" }, { Id: 3, Name: "Chittagong" }, { Id: 4, Name: "Comilla" }, { Id: 5, Name: "EPZ" }
                ];
                $scope.epbRefNumbers = [
                    { Id: 1, Name: "DH" }, { Id: 2, Name: "NR" }, { Id: 3, Name: "CH" }, { Id: 4, Name: "CO" }, { Id: 5, Name: "ED" }
                ];

                $scope.exporterModel();

                $scope.expandForm = false;
                $scope.isDealingAssistantUser = ($scope.user.RoleNames[0] === "DealingAssistance") ? true : false;
            };

            $scope.exporterModel = function () {
                if (!$scope.isUpdateMode) $scope.model.Id = $scope.guid();
                $scope.model.ExporterCategory = "";
                $scope.model.ExporterNumber = "";
                $scope.model.RegDate = new Date();
                $scope.model.PeriodofValidation = new Date();
                $scope.model.RegistrationValidSince = $scope.model.PeriodofValidation;
            };

            $scope.bindEpbRefNo = function() {
                $scope.model.EpbReferenceNo = $scope.model.ServiceOffice;
            };

            $scope.toggleForm = function(value) {
                $scope.expandForm = value;
            };


            function getExporterFirstTwoChar(exporterNumber) {
                return exporterNumber.substring(0, 2);
            };

            $scope.generateTemportyExporterNumner = function() {
                if ($scope.model.ExporterCategory !== undefined &&
                    $scope.model.ExporterCategory !== "" &&
                    $scope.model.ExporterCategory !== null &&
                    $scope.model.ExporterType !== undefined &&
                    $scope.model.ExporterType !== "" &&
                    $scope.model.ExporterType !== null) {
                    commonHttpService.call(urlService.AutoGenUrl +
                            "?type=ExporterNumber&category=" +
                            $scope.model.ExporterCategory +
                            "&regType=" +
                            $scope.model.ExporterType)
                        .then(function(response) {
                                if (!$scope.isUpdateMode) {
                                    $scope.model.ExporterNumber = response;
                                }
                                if ($scope.isUpdateMode) {
                                    if (getExporterFirstTwoChar($scope.model.ExporterNumber) === "NR") {
                                        $scope.model.ExporterNumber = response;
                                    }
                                }
                            },
                            function(error) {
                                console.log(error);
                            });
                }
            };


            $scope.$on("parentResetEventCaller",
               function (event, args) {
                   $scope.start();
               });

            $scope.$on("afterLoadSingleDataEventCaller",
               function (event, args) {
                   $scope.model.RegDate = new Date($scope.model.RegDate);
                   $scope.toggleForm(true);
               });


            config();
            init();


        }
    ]);