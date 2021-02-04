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
                .state("root.branch",
                {
                    url: "/branch",
                    data: { pageTitle: "Branch" },
                    views: {
                        "": {
                            templateUrl: viewBase + "branch/branch.tpl.html",
                            controller: "BranchController"
                        }
                    }
                });

        }
    ]);