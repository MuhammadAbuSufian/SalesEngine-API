"use strict";

angular.module("psapp")
    .controller("AppController", [
        "$rootScope", "$scope", "$state", "$window", "LocalStorageService",
        function ($rootScope, $scope, $state, $window, localStorageService) {

            $scope.isLoggedIn = localStorageService.getUserIsLoggedIn();
            $scope.user = localStorageService.getUserInfo();


            $scope.logout = function () {
                localStorageService.clearUserInfo();
                $scope.isLoggedIn = false;
                $rootScope.$broadcast("loggedOut");
                $state.go("login", {}, { reload: true });
            }

            $rootScope.$on("loggedIn", function (event, args) {
                console.log(event);
                $scope.isLoggedIn = localStorageService.getUserIsLoggedIn();
                $scope.user = localStorageService.getUserInfo();
            });

            $rootScope.$on("loggedOut", function (event, args) {
                console.log(event);
            });

        }
    ]);