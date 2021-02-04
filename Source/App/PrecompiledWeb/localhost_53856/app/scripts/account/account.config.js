"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                //.state("root.account",
                //{
                //    abstract: true,
                //    url: "/account",
                //    templateUrl: viewBase + "account/account.tpl.html"
                //})
                .state("root.account",
                {
                    url: "/account",
                    data: { pageTitle: "Account" },
                    views: {
                        "": {
                            templateUrl: viewBase + "account/account.tpl.html",
                            controller: "AccountController"
                        }
                    }
                });

        }
    ]);