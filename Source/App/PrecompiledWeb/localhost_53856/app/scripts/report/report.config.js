"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("root.report",
                    {
                        abstract: true,
                        url: "/report",
                        data: { pageTitle: "Report" },
                        views: {
                            "": {
                                template: "<div ui-view></div>"
                                //templateUrl: viewBase + "report/report.tpl.html",
                                //controller: "ReportController"
                            }
                        }
                    })
                .state("root.report.account",
                    {
                        url: "/account",
                        data: { pageTitle: "Account Report" },
                        views: {
                            "": {
                                templateUrl: viewBase + "report/account-report.tpl.html",
                                controller: "AccountReportController"
                            }
                        }
                    })
                .state("root.report.service",
                    {
                        url: "/account",
                        data: { pageTitle: "Service Report" },
                        views: {
                            "": {
                                templateUrl: viewBase + "report/service-report.tpl.html",
                                controller: "ServiceReportController"
                            }
                        }
                    })
                .state("root.report.audit",
                    {
                        url: "/audit",
                        data: { pageTitle: "Audit Report" },
                        views: {
                            "": {
                                templateUrl: viewBase + "report/audit-report.tpl.html",
                                controller: "AuditReportController"
                            }
                        }
                    })
                .state("root.report.payslip", {
                    url: "/payslip",
                    data: { pageTitle: "Payslip Report" },
                    views: {
                        "": {
                            templateUrl: viewBase + "report/payslip-report.tpl.html",
                            controller: "PayslipReportController"
                        }
                    }
                });

        }
    ]);