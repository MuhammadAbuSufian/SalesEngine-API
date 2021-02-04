"use strict";

angular.module("psapp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("dashboard",
                {
                    parent: "site",
                    url: "/",
                    data: {
                        roles: ["User"]
                    },
                    resolve: {
                        GridData: [
                            "DashboardService", function(dashboardService) {
                                return dashboardService.get();
                            }
                        ]
                    },
                    views: {
                        '': {
                            templateUrl: viewBase + "dashboard/dashboard.tpl.html",
                            controller: "DashboardController"
                        }
                    }
                })
                .state("import",
                {
                    url: "/import",
                    templateUrl: viewBase + "import/import.tpl.html",
                })
                .state("import-result",
                {
                    url: "/import/result",
                    templateUrl: viewBase + "import/import-result.tpl.html",
                });

        }
    ]);