"use strict";


angular.module("app")
    .controller("SettingController",
        [
            "$scope", "$rootScope", "UrlService", "$controller", "BaseService", "CommonHttpService", "AlertService", "$http", "$state",
            function ($scope, $rootScope, urlService, $controller, baseService, commonHttpService, alertService, $state, $http) {
                "use strict";


                var config = function () {
                    baseService.setCurrentApi(urlService.SettingUrls.Setting);
                    baseService.setIsLoadDataFromUrls(true);
                    baseService.setUrls([]);
                    baseService.setIsLoadPagingData(false);
                    baseService.setDataStatus("Active");
                    baseService.setCallerStatus(true);
                    $controller("BaseController", { $scope: $scope });
                }

                var init = function () {
                    commonHttpService.get(urlService.SettingUrls.Setting)
                        .then(function (response) {
                            if (response != null) {

                                $scope.setting = response;

                                $scope.model.Id = $scope.setting.Id;
                                $scope.model.Active = $scope.setting.Active;
                                $scope.model.TimeZone = $scope.setting.TimeZone;
                                $scope.model.Name = $scope.setting.Name;
                                $scope.model.Address = $scope.setting.Address;
                                $scope.model.Phone = $scope.setting.Phone;
                                $scope.model.Email = $scope.setting.Email;
                                $scope.model.Website = $scope.setting.Website;
                                $scope.model.Facebook = $scope.setting.Facebook;
                                $scope.model.Twitter = $scope.setting.Twitter;
                                $scope.model.LinkedIn = $scope.setting.LinkedIn;
                                $scope.model.PoweredBy = $scope.setting.PoweredBy;
                                $scope.model.HasHostingAggreemenet = $scope.setting.HasHostingAggreemenet;

                                var hostingValidTill = new Date($scope.setting.HostingValidTill)
                                $scope.model.HostingValidTill = hostingValidTill;

                                $scope.model.HasServiceAggrement = $scope.setting.HasServiceAggrement;

                                var serviceValidTill = new Date($scope.setting.ServiceValidTill)
                                $scope.model.ServiceValidTill = serviceValidTill;

                                $scope.model.Descriptions = $scope.setting.Descriptions;
                                $scope.model.Slogan = $scope.setting.Slogan;

                                $scope.model.ContactPersonName = $scope.setting.ContactPersonName;
                                $scope.model.ContactPersonPhone = $scope.setting.ContactPersonPhone;
                                $scope.model.ContactPersonEmail = $scope.setting.ContactPersonEmail;

                                timezones();

                                $scope.isUpdateMode = true;
                            }
                            else {
                                timezones();
                                $scope.start();
                            }
                        },
                            function (error) {
                                console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Failed To Load Data", true);
                            });
                }
                var timezones = function () {
                    commonHttpService.get(urlService.DropdownUrl + "?type=Setting")
                        .then(function (response) {
                            $scope.data.settingDropdown = response;
                        },
                            function (error) {
                                console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Failed To Load Data", true);
                            });
                }


                $scope.settingModel = function () {

                    $scope.setting = {
                        Active: true,
                        TimeZone: $scope.model.TimeZone,
                        Name: $scope.model.Name,
                        Address: $scope.model.Address,
                        Phone: $scope.model.Phone,
                        Email: $scope.model.Email,
                        Website: $scope.model.Website,
                        Facebook: $scope.model.Facebook,
                        Twitter: $scope.model.Twitter,
                        LinkedIn: $scope.model.LinkedIn,
                        PoweredBy: $scope.model.PoweredBy,
                        HasHostingAggreemenet: $scope.model.HasHostingAggreemenet,
                        HostingValidTill: $scope.model.HostingValidTill,
                        HasServiceAggrement: $scope.model.HasServiceAggrement,
                        ServiceValidTill: $scope.model.ServiceValidTill,
                        Descriptions: $scope.model.Descriptions,
                        Slogan: $scope.model.Slogan,
                        ContactPersonName: $scope.model.ContactPersonName,
                        ContactPersonPhone: $scope.model.ContactPersonPhone,
                        ContactPersonEmail: $scope.model.ContactPersonEmail

                    };
                };

                $scope.trashSetting = function (size, data, action) {
                    data.Name = "Time Zone :" + data.TimeZone;
                    $scope.trash(size, data, action);
                };
                $scope.start = function () {
                        $scope.model.HostingValidTill = new Date(),
                        $scope.model.ServiceValidTill = new Date(),
                        $scope.setting;
                    
                };

                config();
                init();

            }

        ]);