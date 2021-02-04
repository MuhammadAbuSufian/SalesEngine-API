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
                .state("root.employee",
                {
                    url: "/employee",
                    data: { pageTitle: "Employee" },
                    views: {
                        "": {
                            templateUrl: viewBase + "employee/employee.tpl.html",
                            controller: "EmployeeController"
                        }
                    }
                });

        }
    ]);