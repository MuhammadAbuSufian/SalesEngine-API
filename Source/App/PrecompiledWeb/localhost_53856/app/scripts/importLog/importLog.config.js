"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterPrvider, $stateProvider) {

            var viewBase = "app/views/";

            $stateProvider.state("root.import-log",
                {
                    
                    url: "/importLog",
                    data: { pageTitle: "ImportLog" },
                    views: {
                        "": {
                            templateUrl: viewBase + "importLog/importLog.tpl.html",
                            controller: "ImportLogController"
                        }
                    }
                })
        }
    ]);