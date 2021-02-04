"use strict";

angular.module("psapp")
    .service("RoleDataService", [
         function() {
             return {
                 get: function () {
                     this.roles = [
                         { Id: 1, Name: "Admin" },
                         { Id: 2, Name: "Exporter" }
                     ];

                     return this.roles;
                 },

                 getUserRole: function(roleId) {

                     var roles = this.get();

                     for (var r in roles) {
                         if (roles.hasOwnProperty(r)) {
                             if (roles[r].Id === roleId)
                                 return roles[r];
                         }
                     }

                     return null;
                 }
             }
        }
    ]);