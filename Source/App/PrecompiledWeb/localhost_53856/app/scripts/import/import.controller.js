"use strict";


angular.module("app")
    .controller("ImportController",
        [
            "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "Upload", "$timeout", "AlertService", "$location",
            function ($scope, $rootScope, urlService, $controller, baseService, Upload, $timeout, alertService, $location) {
                "use strict";


                var config = function () {

                    baseService.setCurrentApi(urlService.ImportUrls.Import);

                    baseService.setIsLoadDataFromUrls(false);
                    baseService.setUrls([]);
                    baseService.setIsLoadPagingData(true);
                    baseService.setDataStatus("Active");
                    $controller("BaseController", { $scope: $scope });
                }

                var init = function () {

                    //$scope.addCall(urlService.ImportUrl);
                    ////$scope.addCall(urlService.DropdownUrl + "?type=Category");

                    //var dataProperties = ["list"];

                    //baseService.setUrls($scope.urls);
                    //baseService.setDataProperties(dataProperties);
                    // alert("init");
                    //$scope.baseCaller();
                    $scope.start();
                };

                $scope.$on("parentResetEventCaller",
                    function (event, args) {
                        $scope.start();
                    });


                $scope.start = function () {
                    $scope.searchRequest.OrderBy = "TransectionId";

                    $scope.searchRequest.Date = new Date();
                };

                $scope.backToAccount = function (model) {
                    console.log(model);
                    baseService.setTransectionID(model.TransectionId);
                    baseService.setTransectionDate(model.TransectionDate);

                    $location.path("/account");
                }


                $scope.uploadPic = function (file) {

                    var splite = file.name.split(".");
                    if (splite.length === 2) {
                        var filePrefix = "File";
                        var newName = filePrefix + "." + splite[1];
                        Upload.rename(file, newName);
                    }

                    file.upload = Upload.upload({
                        url: urlService.ImportUrls.UploadFile,
                        data: { username: $scope.username, file: file },
                    });

                    $scope.promise = file.upload.then(function (response) {
                        $timeout(function () {
                            file.result = response.data;
                            alertService.showAlert(alertService.alertType.success, "Success", false);
                            init();
                        });
                    }, function (response) {
                        if (response.status > 0)
                            $scope.errorMsg = response.status + ': ' + response.data;
                        alertService.showAlert(alertService.alertType.danger, "Falied", false);
                    }, function (evt) {
                        // Math.min is to fix IE which reports 200% sometimes
                        file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                    });
                }

                $scope.filter = function () {
                    //$scope.pagingRequest.Date = $scope.searchRequest.Date;

                    $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                    $scope.search();
                }


                config();
                init();

            }
        ]);