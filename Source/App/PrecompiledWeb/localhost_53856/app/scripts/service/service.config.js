"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("root.service",
                {
                    url: "/service",
                    data: { pageTitel: "Service" },
                    views: {
                        "": {
                            templateUrl: viewBase + "service/service.tpl.html",
                            controller: "ServiceController"
                        }
                    }
                })
                .state("root.service-detail",
                {
                    url: "/service/:id/detail",
                    templateUrl: viewBase + "service/service-detail.tpl.html",
                    controller: "ServiceDetailController"
                });
            //.state("root.service.approved",
            //{
            //    url: "/approved",
            //    templateUrl: viewBase + "service/service.approved.tpl.html",
            //    controller: "ServiceController"
            //})
            //.state("root.service.print",
            //{
            //    url: "/print",
            //    templateUrl: viewBase + "service/print.tpl.html",
            //    controller: "ServiceController"
            //});

        }
    ]);