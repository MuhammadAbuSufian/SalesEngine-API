"use strict";


angular.module("app")
    .controller("TempExporterController",
    [
        "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService",
        function($scope, $rootScope, urlService, $controller, baseService, commonHttpService) {
            "use strict";

            var config = function() {
                baseService.setCurrentApi(urlService.NonExporterUrls.NonExporter);
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

            $scope.saveRegistration = function() {
                commonHttpService.post(urlService.NonExporterUrls.NonExporterSave, $scope.model)
                   .then(function (response) {

                   },
                       function (error) {
                           console.log(error);
                       });
            };

            $scope.start = function () {
                

                //$scope.exporterModel();
                $scope.model.RegDate = new Date();
                $scope.expandForm = false;
            };
        /*    $scope.exporterModel = function () {
                //if (!$scope.isUpdateMode) $scope.model.Id = $scope.guid();
                //$scope.model.ExporterCategory = "";
                //$scope.model.ExporterNumber = "";
                $scope.model.RegDate = new Date();
                //$scope.model.PeriodofValidation = new Date();
               // $scope.model.RegistrationValidSince = $scope.model.PeriodofValidation;
            };*/

            /*$scope.bindEpbRefNo = function() {
                $scope.model.EpbReferenceNo = $scope.model.ServiceOffice;
            };*/

            $scope.toggleForm = function(value) {
                $scope.expandForm = value;
            };


            function getExporterFirstTwoChar(exporterNumber) {
                return exporterNumber.substring(0, 2);
            };

          /*  $scope.generateTemportyExporterNumner = function() {
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
            };*/


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