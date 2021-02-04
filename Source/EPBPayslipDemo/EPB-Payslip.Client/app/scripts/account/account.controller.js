"use strict";


angular.module("psapp")
    .controller("AccountController",
    [
        "$scope", "$state", function($scope, $state) {

            $scope.back = function() {
                $state.go("dashboard");
            };

            $scope.today = new Date();

            $scope.view = "exporter";
            var user = JSON.parse(localStorage.getItem("userInfo"));

            if (user.RoleId === 1)
                $scope.view = "admin";

            //payslip.form
            $scope.payslip = {
                AccountNo: "1503202417031001",
                AccountName: "Export Promotion Bureau",
                ExporterNo: "BD0001",

                DepositDate: new Date(),
                DepositorName: "",
                BankName: "Brack Bank Ltd.",
                DepositorAddress: "",
                BranchName: "",
                DepositorPhone: "",
                TransectionId: "",
                TotalAmount: "",
                AmountInWords: "",
                SlipImageUrl: ""
            };

            $scope.payslips = [];
            $scope.request = function() {
                $scope.payslips.push($scope.payslip);
                $state.go("dashboard");
            };
            $scope.save = function() {
                $state.go("dashboard");
            };
        }
    ]);