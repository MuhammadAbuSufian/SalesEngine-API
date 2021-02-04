"use strict";

angular.module("app")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterPrvider, $stateProvider) {
           
            var viewBase = "app/views/";

            $stateProvider.state("root.import",
                {
                    url: "/import",
                    data: { pageTitle: "Import" },
                    views: {
                        "": {
                            templateUrl: viewBase + "import/import.tpl.html",
                            controller: "ImportController"
                        }
                    }
                })
        }
    ]);