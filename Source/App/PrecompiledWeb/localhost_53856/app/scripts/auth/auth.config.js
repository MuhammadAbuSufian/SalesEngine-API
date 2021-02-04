angular.module("app")
    .config([
        "$stateProvider", "$urlRouterProvider",
        function($stateProvider, $urlRouterProvider) {
            "use strict";


            $stateProvider
                .state("root.login",
                {
                    url: "/login",
                    data: { pageTitle: "Sign in" },
                    views: {
                        '': {
                            templateUrl: "app/views/auth/login.tpl.html",
                            controller: "LoginController"
                        }
                    }
                })
                .state("root.access-denied",
                {
                    url: "/access-denied",
                    data: { pageTitle: "Access Denied" },
                    views: {
                        '': {
                            templateUrl: "app/views/auth/access-denied.tpl.html",
                            controller: "AccessDeniedController"
                        }
                    }
                })
                .state("root.profile",
                {
                    url: "/profile",
                    data: { pageTitle: "Profile" },
                    views: {
                        "": {
                            templateUrl: "app/views/auth/profile.tpl.html",
                            controller: "ProfileController"
                        }
                    }
                })
                .state("root.role",
                {
                    url: "/role",
                    data: { pageTitle: "Role" },
                    views: {
                        "": {
                            templateUrl: "app/views/auth/role.tpl.html",
                            controller: "RoleController"
                        }
                    }
                })
                .state("root.resource",
                {
                    url: "/resource",
                    data: { pageTitle: "Resource" },
                    views: {
                        "": {
                            templateUrl: "app/views/auth/resource.tpl.html",
                            controller: "ResourceController"
                        }
                    }
                })
                .state("root.permission",
                {
                    url: "/permission",
                    data: { pageTitle: "Permission" },
                    views: {
                        "": {
                            templateUrl: "app/views/auth/permission.tpl.html",
                            controller: "PermissionController"
                        }
                    }
                });

        }
    ]);