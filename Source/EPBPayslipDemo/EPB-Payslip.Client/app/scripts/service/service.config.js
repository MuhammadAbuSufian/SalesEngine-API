"use strict";

angular.module("psapp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("service",
                {
                    abstract: true,
                    url: "/service",
                    templateUrl: viewBase + "service/service.tpl.html"
                })
                .state("service.form",
                {
                    url: "",
                    templateUrl: viewBase + "service/service.form.tpl.html",
                    controller: "ServiceController"
                })
                .state("service.requested",
                {
                    url: "/requested",
                    templateUrl: viewBase + "service/service.requested.tpl.html",
                    controller: "ServiceController"
                })
                .state("service.approved",
                {
                    url: "/approved",
                    templateUrl: viewBase + "service/service.approved.tpl.html",
                    controller: "ServiceController"
                })
                .state("service.print",
                {
                    url: "/print",
                    templateUrl: viewBase + "service/print.tpl.html",
                    controller: "ServiceController"
                });

        }
    ]);