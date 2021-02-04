"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("root.dashboard",
                {
                    url: "/dashboard",
                    data: { pageTitle: "Dashboard" },
                    views: {
                        '': {
                            templateUrl: viewBase + "dashboard/dashboard.tpl.html",
                            controller: "DashboardController"                            
                        }
                    }
                });

        }
    ]);