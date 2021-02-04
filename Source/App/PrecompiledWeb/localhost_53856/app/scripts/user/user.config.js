﻿angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterProvider, $stateProvider) {
            "use strict";

            $stateProvider
                .state("root.user", {
                    url: "/user",
                    data : {pageTitle: "User"},
                    templateUrl: "app/views/user/user.tpl.html",
                    controller: "UserController"
                });
        }
    ]);