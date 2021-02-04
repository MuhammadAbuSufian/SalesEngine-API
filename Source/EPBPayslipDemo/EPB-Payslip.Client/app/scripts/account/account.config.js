"use strict";

angular.module("psapp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("account",
                {
                    abstract: true,
                    url: "/account",
                    templateUrl: viewBase + "account/account.tpl.html"
                })
                .state("account.form",
                {
                    url: "",
                    templateUrl: viewBase + "account/account.form.tpl.html",
                    controller: "AccountController"
                });

        }
    ]);