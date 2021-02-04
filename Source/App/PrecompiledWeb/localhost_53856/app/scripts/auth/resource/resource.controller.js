angular.module("app")
    .controller("ResourceController", [
        "$scope", "UrlService", "AuthHttpService", "AlertService",
        function ($scope, urlService, authHttpService, alertService) {
            "use strict";

            var init = function () {
                $scope.promise = null;
                $scope.model = { Id: "", Name: "", Route: "", IsPublic: false };
                $scope.list = [];
                $scope.isPublicEnum = { True: true, False: false };
                $scope.isUpdateMode = false;
                $scope.loadResources();
            };


            $scope.loadResources = function() {
                $scope.promise = authHttpService.get(urlService.ResourceUrls.Resource).then(function (data) {
                    console.log(data);
                    $scope.list = data;
                }, function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Failed to load resources, please tray agin or refresh your page or check you internet connection", true);
                });
            };

            $scope.save = function() {
                if ($scope.isUpdateMode) $scope.update();

                else {
                    $scope.promise = authHttpService.add(urlService.ResourceUrls.Resource, $scope.model).then(function (data) {
                        console.log(data);
                        init();
                        alertService.showAlert(alertService.alertType.success, "Success", false);
                    }, function(error) {
                        console.log(error);
                    });
                }
            };

            $scope.loadResource = function(id) {
                $scope.promise = authHttpService.getByParams(urlService.ResourceUrls.Resource, { request: id }).then(function (data) {
                    console.log(data);
                    $scope.model = data;
                }, function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Failed!, please tray agin or refresh your page or check you internet connection", true);
                });
            };

            $scope.edit = function(id) {
                $scope.loadResource(id);
                $scope.isUpdateMode = true;
            };


            $scope.update = function() {
                $scope.promise = authHttpService.update(urlService.ResourceUrls.Resource, $scope.model).then(function (data) {
                    console.log(data);
                    init();
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                }, function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Failed!, please tray agin or refresh your page or check you internet connection", true);
                });
            };

            $scope.delete = function(id) {
                $scope.promise = authHttpService.remove(urlService.ResourceUrls.Resource, id).then(function (data) {
                    console.log(data);
                    init();
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                }, function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Failed!, please tray agin or refresh your page or check you internet connection", true);
                });
            };

            $scope.remove = function(size, data, action) {
                alertService.showConfirmDialog(size, data, action, false).then(function(response) {
                    console.log(response);
                    if (response.isConfirm) {
                        $scope.delete(response.data.Id);
                    }
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.cancel = function () {
                init();
            };

            init();

        }
    ]);