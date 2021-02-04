"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider.state("root.setup",
                {
                    abstract: true,
                    url: "/setup",
                    template: "<div ui-view class='container'></div>"
                })
                .state("root.setup.service-type",
                    {
                        url: "/service-type",
                        data: { pageTitel: "Setup - Servie Type" },
                        views: {
                            "": {
                                templateUrl: viewBase + "setup/service-type.tpl.html",
                                controller: "ServiceTypeController"
                            }
                        }
                    })
                .state("root.setup.issue-name",
                    {
                        url: "/issue-name",
                        data: { pageTitel: "Setup - Issue Name" },
                        views: {
                            "": {
                                templateUrl: viewBase + "setup/issue-name.tpl.html",
                                controller: "IssueNameController"
                            }
                        }
                    })
                .state("root.setup.service-issue-type",
                    {
                        url: "/service-issue-type",
                        data: { pageTitel: "Setup - Servie Issue Type" },
                        views: {
                            "": {
                                templateUrl: viewBase + "setup/service-issue-type.tpl.html",
                                controller: "ServiceIssueTypeController"
                            }
                        }
                    })
                .state("root.setup.exporter",
                    {
                        url: "/exporter",
                        data: { pageTitel: "Setup - Exporter" },
                        views: {
                            "": {
                                templateUrl: viewBase + "setup/exporter.tpl.html",
                                controller: "ExporterController"
                            }
                        }
                    })
                .state("root.setup.temp-exporter",
                    {
                        url: "/temp-exporter",
                        data: { pageTitel: "Setup - Non Exporter" },
                        views: {
                            "": {
                                templateUrl: viewBase + "setup/tempexporter.tpl.html",
                                controller: "TempExporterController"
                            }
                        }
                    })
                .state("root.setting", {
                    url: "/setting",
                    data: { pageTitle: "Setting" },
                    views: {
                        "": {
                            templateUrl: viewBase + "setup/setting.tpl.html",
                            controller: "SettingController"
                        }
                    }
                })
                .state("root.setting-detail",
                {
                    url: "/setting/:id/detail",
                    templateUrl: viewBase + "setup/setting.tpl.html",
                    controller: "SettingController"
                });
        }
    ]);