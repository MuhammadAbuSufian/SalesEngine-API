"use strict";


angular.module("psapp", ["ui.router", "ngGrid", "ui.bootstrap"])   
    .config([
        "$urlRouterProvider", "$stateProvider",
        function($urlRouterProvider, $stateProvider) {            

            $urlRouterProvider.otherwise("/");

            var viewBase = "app/views/";

            $stateProvider
                .state("site", {
                    'abstract': true,                                        
                    template: "<div ui-view class=\"container slide\"></div>",
                    controller: "AppController"

                    
                })
                .state("about", {
                    parent: "site",
                    url: "/about",
                    views: {
                        "": {
                            templateUrl: viewBase + "about/about.tpl.html",
                            //controller: "AboutController"
                        }
                    }
                })
                .state("contact", {
                    parent: "site",
                    url: "/contact",
                    views: {
                        "": {
                            templateUrl: viewBase + "contact/contact.tpl.html",
                            //controller: "ContactController"
                        }
                    }
                });
        }
    ])
     .run([
        "$rootScope", "$state", "$stateParams", "AuthorizationService", "AuthenticationService",
        function ($rootScope, $state, $stateParams, authorizationService, authenticationService) {

            $rootScope.$on("$stateChangeStart", function (event, toState, toStateParams) {

                var isLogin = toState.name === "login";
                if (isLogin) return;

                var isAccessDenied = toState.name === "denied";
                if (isAccessDenied) return;


                if (authenticationService.authenticate()) {
                    if (!authorizationService.authorize(toState)) {
                        event.preventDefault();
                        $state.go("denied");
                    }
                } else {
                    event.preventDefault();
                    $state.go("login");
                }


            });

        }
     ]);