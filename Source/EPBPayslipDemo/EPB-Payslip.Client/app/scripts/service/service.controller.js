"use strict";


angular.module("psapp")
    .controller("ServiceController",
    [
        "$scope", "$state", function($scope, $state) {

            $scope.back = function() {
                $state.go("dashboard");
            };

            $scope.today = new Date();

            

            //payslip.form
            $scope.payslip = {
                AccountNo: "1503202417031001",
                AccountName: "Export Promotion Bureau",
                ExporterNo: "",

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

            $scope.isDisabled = false;
            $scope.user = JSON.parse(localStorage.getItem("userInfo"));
            if ($scope.user.Username === "exporter") {
                $scope.isDisabled = true;
                $scope.payslip.ExporterNo = "BD0001";
            }

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