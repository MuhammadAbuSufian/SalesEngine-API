"use strict";

angular.module("psapp")
    .service("PermissionDataService", [
        "LocalStorageService",
        function(LocalStorageService) {

            return {
                get: function() {
                    this.permissionList = [
                        { Id: 1, UserId: 1, RoleId: 1, Permission: "dashboard" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "account.form" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "service.form" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "service.requested" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "service.approved" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "service.print" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "import" },
                        { Id: 2, UserId: 1, RoleId: 1, Permission: "import-result" },

                        { Id: 5, UserId: 2, RoleId: 2, Permission: "dashboard" },
                        { Id: 6, UserId: 2, RoleId: 2, Permission: "account.form" },
                        { Id: 6, UserId: 2, RoleId: 2, Permission: "service.form" },
                        { Id: 6, UserId: 2, RoleId: 2, Permission: "service.requested" },
                        { Id: 6, UserId: 2, RoleId: 2, Permission: "service.approved" },
                        //{ Id: 2, UserId: 2, RoleId: 2, Permission: "import" },
                        //{ Id: 2, UserId: 2, RoleId: 2, Permission: "import-result" },
                    ];

                    return this.permissionList;
                },


                getUserPermissions: function(userInfo) {

                    var permissions = this.get();
                    var userPermission = [];

                    if (userInfo) {

                        for (var p in permissions) {
                            if (permissions[p].UserId === userInfo.Id && permissions[p].RoleId === userInfo.RoleId)
                                userPermission.push(permissions[p]);
                        }
                    }
                    return userPermission;
                },


                checkUserPermission: function(userInfo, permissionObject) {

                    var permissions = this.get();

                    if (userInfo && permissionObject) {

                        for (var p in permissions) {

                            if (permissions[p].UserId === userInfo.Id && permissions[p].RoleId === userInfo.RoleId && permissions[p].Permission === permissionObject.name)
                                return true;
                        }
                    }
                    return false;
                },


                checkUserTokenBasedPermission: function(permissionObject) {

                    var permissions = this.get();
                    var userInfo = LocalStorageService.getUserInfo();

                    if (permissionObject) {

                        for (var p in permissions) {

                            if (permissions[p].UserId === userInfo.Id && permissions[p].RoleId === userInfo.RoleId && permissions[p].Permission === permissionObject.name)
                                return true;
                        }
                    }
                    return false;
                }
            }
        }
    ]);