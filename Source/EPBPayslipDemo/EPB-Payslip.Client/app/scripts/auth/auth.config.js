"use strict";

angular.module("psapp")
    .config([
        "$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRoutterProvider) {

            var viewBase = "app/views/";

            $stateProvider
                .state("login", {
                    parent: "site",
                    url: "/login",
                    resolve: {
                        Roles : ["RoleDataService", 
                            function(RoleDataService) {
                                return RoleDataService.get();
                            }
                        ]
                    },
                    views: {
                        '': {
                            templateUrl: viewBase  + "auth/login.tpl.html",
                            controller: "LoginController"
                        }
                    }
                })
                .state("admin", {
                    parent: "site",
                    url: "/admin",                   
                    views: {
                        '': {
                            templateUrl: viewBase + "admin/admin.tpl.html"
                        }
                    }
                })
                .state("denied", {
                    parent: "site",
                    url: "/access-denied",                   
                    views: {
                        '': {
                            templateUrl: viewBase + "auth/denied.tpl.html"
                        }
                    }
                });

        }
    ]);