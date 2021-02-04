angular.module("app")
    .controller("BaseController",
        [
            "$scope", "UrlService", "AlertService", "CommonHttpService", "BaseService", "LocalDataStorageService", "$controller", "$rootScope", "$http",
            function ($scope, urlService, alertService, commonHttpService, baseService, localDataStorageService, $controller, $rootScope, $http) {
                "use strict";


                var caller = function () {
                    if ($scope.isLoadDataFromUrls) {
                        if ($scope.isLoadPagingData) {
                            if ($scope.urls.length > 0)
                                $scope.loadPagingDataFromUrls();
                        } else {
                            if ($scope.urls.length > 0)
                                $scope.loadDataFromUrls();
                        }
                    } else {
                        if ($scope.isLoadPagingData) {
                            $scope.loadListOfPagingData();
                        } else {
                            $scope.loadListOfData();
                        }
                    }
                };

                var init = function () {
                    $scope.promise = null;
                    $scope.isUpdateMode = false;
                    $scope.data = [];
                    $scope.count = 0;
                    $scope.totalPages = 1;
                    $scope.pageCounts = [5, 10, 20, 30, 50, 100];
                    $scope.model = {
                        Id: "",
                        Created: new Date(),
                        CreatedBy: localDataStorageService.getUserInfo().Id,
                        Modified: new Date(),
                        ModifiedBy: localDataStorageService.getUserInfo().Id
                    };
                    $scope.searchRequest = {
                        Keyword: "",
                        OrderBy: "Modified",
                        IsAscending: true
                    };
                    $scope.pagingRequest = {
                        Keyword: "",
                        OrderBy: "Modified",
                        IsAscending: true,
                        Page: baseService.getPage(),
                        PageCount: baseService.getPageCount()
                    };
                    $scope.searchType = baseService.getSearchType();
                    $scope.currentApiUrl = baseService.getCurrentApi();
                    $scope.isLoadDataFromUrls = baseService.getIsLoadDataFromUrls();
                    $scope.isLoadPagingData = baseService.getIsLoadPagingData();
                    $scope.dataStatus = baseService.getDataStatus();
                    $scope.urls = baseService.getUrls();
                    $scope.dataProperties = baseService.getDataProperties();
                    $scope.user = localDataStorageService.getUserInfo();
                    if (baseService.getCallerStatus()) {
                        caller();
                    }
                };

                $scope.isNullOrEmpty = function (item) {
                    if (item === "" || item === null || item === undefined)
                        return true;
                    return false;
                }

                $scope.addCall = function (url) {
                    $scope.urls.push({ url: url });
                };

                $scope.updateModel = function () {
                    $scope.model.Modified = new Date();
                    $scope.model.ModifiedBy = localDataStorageService.getUserInfo().Id;
                };

                var dateBinder = function () {
                    if ($scope.data.list !== null) {
                        for (var i = 0; i < $scope.data.list.length; i++) {
                            if ($scope.data.list[i].Created !== undefined)
                                $scope.data.list[i].Created = new Date($scope.data.list[i].Created);
                            if ($scope.data.list[i].Modified !== undefined)
                                $scope.data.list[i].Modified = new Date($scope.data.list[i].Modified);
                        }
                    }
                };

                /*
                 *************************PAGING BLOCK START*****************************************
                 */

                $scope.setPageCount = function () {
                    baseService.setPageCount($scope.pagingRequest.PageCount);
                };

                var setPage = function () {
                    baseService.setPage($scope.pagingRequest.Page);
                };

                var callDataLoader = function () {
                    setPage();
                    if ($scope.searchRequest.Keyword === "")
                        $scope.loadListOfPagingData();
                    else
                        $scope.search();

                };

                $scope.first = function () {
                    if ($scope.pagingRequest.Page > 1) {
                        $scope.pagingRequest.Page = 1;
                        callDataLoader();
                    }
                };

                $scope.last = function () {
                    if ($scope.pagingRequest.Page < $scope.totalPages) {
                        $scope.pagingRequest.Page = $scope.totalPages;
                        callDataLoader();
                    }
                };

                $scope.prev = function () {
                    if ($scope.pagingRequest.Page > 1) {
                        $scope.pagingRequest.Page = $scope.pagingRequest.Page - 1;
                        callDataLoader();
                    }
                };
                $scope.next = function () {
                    if ($scope.pagingRequest.Page < $scope.totalPages) {
                        $scope.pagingRequest.Page = $scope.pagingRequest.Page + 1;
                        callDataLoader();
                    }
                };

                $scope.loadListOfPagingData = function (type) {
                    var dataStatus = $scope.dataStatus;
                    if (type !== undefined)
                        dataStatus = type;

                    $scope.promise = commonHttpService.getPagingData($scope.currentApiUrl, dataStatus, $scope.pagingRequest)
                        .then(function (data) {
                            //console.log(data);
                            $scope.data.list = data.Data;
                            $scope.count = data.Count;
                            $scope.totalPages = Math.ceil($scope.count / $scope.pagingRequest.PageCount);
                            dateBinder();
                        },
                            function (error) {
                                console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Failed To Load Data", true);
                            });
                };

                $scope.loadPagingDataFromUrls = function (type) {
                    var dataStatus = $scope.dataStatus;
                    if (type !== undefined)
                        dataStatus = type;

                    $scope.urls[0].url = $scope.currentApiUrl +
                        "?status=" +
                        dataStatus +
                        "&request=" +
                        JSON.stringify($scope.pagingRequest);

                    $scope.promise = commonHttpService.loadPagingDataFromUrls($scope.urls)
                        .then(function (results) {
                            //console.log(results);
                            var i;
                            for (i = 0; i < $scope.dataProperties.length; i++) {
                                if (i === 0) {
                                    $scope.data[$scope.dataProperties[i]] = results[i].data.Data;
                                    $scope.count = results[i].data.Count;
                                    $scope.totalPages = Math.ceil($scope.count / $scope.pagingRequest.PageCount);
                                } else {
                                    $scope.data[$scope.dataProperties[i]] = results[i].data;
                                }
                            }

                            dateBinder();
                            $rootScope.$broadcast("afterLoadPagingDataFromUrlsEventCaller");
                        },
                            function (error) {
                                console.log(error);
                            });
                };


                //*********************SEARCH*********************************************
                $scope.search = function (type) {
                    var dataStatus = $scope.dataStatus;
                    if (type !== undefined)
                        dataStatus = type;

                    $scope.pagingRequest.Keyword = $scope.searchRequest.Keyword;
                    $scope.pagingRequest.OrderBy = $scope.searchRequest.OrderBy;
                    $scope.pagingRequest.IsAscending = $scope.searchRequest.IsAscending;
                    $scope.promise = commonHttpService.pagingSearch($scope.currentApiUrl,
                        $scope.searchType,
                        dataStatus,
                        $scope.pagingRequest)
                        .then(function (data) {

                            $scope.data.list = data.Data;
                            $scope.model.ExporterEpbServices = data.Data;
                            $scope.count = data.Count;
                            $scope.totalPages = Math.ceil($scope.count / $scope.pagingRequest.PageCount);
                            dateBinder();
                        },
                            function (error) {
                                console.log(error);
                            });
                };

                /*
                 ***********************PAGING BLOCK END******************************************* 
                 */


                $scope.loadDataFromUrls = function (type) {
                    var dataStatus = $scope.dataStatus;
                    if (type !== undefined)
                        dataStatus = type;
                    $scope.urls[0].url = $scope.currentApiUrl + "?type=" + dataStatus;

                    $scope.promise = commonHttpService.loadDataFromUrls($scope.urls)
                        .then(function (results) {
                            //console.log(results);
                            for (var i = 0; i < $scope.dataProperties.length; i++) {
                                $scope.data[$scope.dataProperties[i]] = results[i].data;
                            }

                            dateBinder();
                            $rootScope.$broadcast("afterLoadDataFromUrlsEventCaller");
                        },
                            function (error) {
                                console.log(error);
                            });
                };

                $scope.loadListOfData = function (type) {
                    var dataStatus = $scope.dataStatus;
                    if (type !== undefined)
                        dataStatus = type;
                    $scope.promise = commonHttpService.get($scope.currentApiUrl, dataStatus)
                        .then(function (data) {
                            //console.log(data);
                            $scope.data.list = data;
                            dateBinder();
                        },
                            function (error) {
                                console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Failed To Load Data", true);
                            });
                };

                $scope.loadSingleData = function (id) {
                    $scope.promise = commonHttpService.find($scope.currentApiUrl, id)
                        .then(function (data) {
                            //console.log(data);
                            $scope.model = data;
                            if ($scope.model.Created !== undefined)
                                $scope.model.Created = new Date($scope.model.Created.toString());
                            if ($scope.model.Modified !== undefined)
                                $scope.model.Modified = new Date($scope.model.Modified.toString());

                            $rootScope.$broadcast("afterLoadSingleDataEventCaller");
                            $rootScope.$broadcast("runUpdateEvent");
                            console.log($scope.model);
                        },
                            function (error) {
                                console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Failed To Load Data", true);
                            });
                };

                $scope.save = function () {
                   
                    if ($scope.isUpdateMode) $scope.update();

                    else {
                        $scope.promise = commonHttpService.post($scope.currentApiUrl, $scope.model)
                            .then(function (data) {
                                //console.log(data);
                                
                                $scope.response = data;
                                alertService.showAlert(alertService.alertType.success, "Success", false);
                                init();
                                $rootScope.$broadcast("afterSaveEventCaller");
                                $rootScope.$broadcast("parentResetEventCaller");
                                $rootScope.$broadcast("displayWarningBroadcust", data);
                            },
                                function (error) {
                                    alertService.showAlert(alertService.alertType.danger, "Failed To Save Data", true);
                                    console.log(error);
                                    $rootScope.$broadcast("displayErrorBroadcust", error.data);
                                });
                    }
                };

                $scope.saveEntries = function (entries) {
                    //if ($scope.isUpdateMode) $scope.update();
                    //else {
                    $scope.promise = commonHttpService.post($scope.currentApiUrl + "/SaveEntries", entries)
                        .then(function (data) {
                            //console.log(data);
                            alertService.showAlert(alertService.alertType.success, "Success", false);
                            init();
                            $rootScope.$broadcast("afterSaveEntriesEventCaller");
                            $rootScope.$broadcast("parentResetEventCaller");
                        },
                            function (error) {
                                alertService.showAlert(alertService.alertType.danger, "Failed To Save Data", true);
                                console.log(error);
                            });
                    //}
                };

                $scope.edit = function (id) {
                    $scope.loadSingleData(id);
                    $scope.isUpdateMode = true;
                };

                $scope.update = function () {
                    $scope.updateModel();
                    $scope.promise = commonHttpService.update($scope.currentApiUrl, $scope.model)
                        .then(function (data) {
                            //console.log(data);
                            init();
                            $rootScope.$broadcast("afterUpdateEventCaller");
                            $rootScope.$broadcast("parentResetEventCaller");
                            alertService.showAlert(alertService.alertType.success, "Success", false);
                        },
                            function (error) {
                                console.log(error);
                                if (error.data.Message != null) {
                                    alertService.showAlert(alertService.alertType.danger, error.data.Message, true);
                                }
                                else {
                                    alertService.showAlert(alertService.alertType.danger, "Failed  to Update Data", true);
                                }
                            });
                };


                var remove = function (actionType, id) {
                    $scope.promise = commonHttpService.remove($scope.currentApiUrl, actionType, id)
                        .then(function (data) {
                            //console.log(data);
                            init();
                            $rootScope.$broadcast("afterRemoveEventCaller");
                            $rootScope.$broadcast("parentResetEventCaller");
                            alertService.showAlert(alertService.alertType.success, "Success", false);
                        },
                            function (error) {
                                console.log(error);
                                if (error.data.Message != null) {
                                    alertService.showAlert(alertService.alertType.danger, error.data.Message, true);
                                }
                                else {
                                    alertService.showAlert(alertService.alertType.danger, "Failed  To " + actionType + " Data", true);
                                }
                            });
                };

                $scope.delete = function (size, data, action, configuration) {
                    if ($scope.isNullOrEmpty(configuration))
                        configuration = false;


                    alertService.showConfirmDialog(size, data, action, false)
                        .then(function (response) {
                            console.log(response);
                            if (response.isConfirm) {
                                remove("Delete", response.data.Id);
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                };

                $scope.trash = function (size, data, action, configuration) {

                    if ($scope.isNullOrEmpty(configuration))
                        configuration = false;

                    alertService.showConfirmDialog(size, data, action, configuration)
                        .then(function (response) {
                            console.log(response);
                            if (response.isConfirm) {
                                remove("Trash", response.data.Id);
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                };

                //C
                $scope.loadDropdown = function (modelName, parentId) {
                    
                    var success = function (data) {
                        //console.log(data);
                        $scope.data[modelName + "Dropdown"] = data;
                        $rootScope.$broadcast(modelName + "DropdownSuccssLoadBroadcust");
                    };
                    var error = function (error) {
                        console.log(error);
                    };
                    $scope.promise = commonHttpService.load(urlService.DropdownUrl, modelName, parentId)
                        .then(success, error);
                };



                $scope.baseCaller = function () {
                    $controller("BaseController", { $scope: $scope });
                }

                $scope.cancel = function () {
                    init();
                    $rootScope.$broadcast("parentResetEventCaller");
                };

                $scope.reset = function () {
                    if ($scope.searchRequest.Keyword === undefined || $scope.searchRequest.Keyword === "") {
                        init();
                        $rootScope.$broadcast("parentResetEventCaller");
                    }
                };

                $scope.guid = function () {
                    function s4() {
                        return Math.floor((1 + Math.random()) * 0x10000)
                            .toString(16)
                            .substring(1);
                    }
                    return s4() + s4() + "-" + s4() + "-" + s4() + "-" +
                        s4() + "-" + s4() + s4() + s4();
                }


                $scope.getObject = function (searchValue, searchProperty, list) {
                    var response = {};
                    if (list !== undefined && list !== null) {
                        for (var i = 0; i < list.length; i++) {
                            if (list[i][searchProperty] === searchValue)
                                return response = list[i];
                        }
                    }
                    return response;
                };



                $scope.loadReport = function () {
                    $scope.promise = commonHttpService.call($scope.currentApiUrl +
                        "?request=" +
                        JSON.stringify($scope.searchRequest))
                        .then(function (data) {
                            //console.log(data);
                            $scope.data.report = data;
                        },
                            function (error) {
                                console.log(error);
                                alertService.showAlert(alertService.alertType.danger, "Failed To Load Report", true);
                            });
                };


                //*******PRINT SECTION*********************
                $scope.printModal = function (size, id, tpl, ctrl, url) {
                    if (!url) url = $scope.currentApiUrl;

                    $scope.promise = commonHttpService.find(url, id)
                        .then(function (data) {
                            //console.log(data);

                            var configuration = {
                                template: "app/views/modal/" + tpl + "print.modal.tpl.html",
                                controller: ctrl + "PrintModalInstanceController"
                            }
                            var action = "Print Preview";

                            alertService.printDialog(size, data, action, configuration)
                                .then(function (response) {
                                    console.log(response);
                                    if (response.isConfirm) {
                                        alertService.showAlert(alertService.alertType.success, "Success", false);
                                    }
                                },
                                    function (error) {
                                        console.log(error);
                                    });

                        },
                            function (error) {
                                console.log(error);
                            });
                };


                $scope.print = function (divName) {
                    var printContents = document.getElementById(divName).innerHTML;
                    var originalContents = document.body.innerHTML;
                    var popupWin;
                    if (navigator.userAgent.toLowerCase().indexOf("chrome") > -1) {
                        popupWin = window.open("", "_blank", "width=800,height=auto,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no");
                        popupWin.window.focus();
                        popupWin.document.write("<!DOCTYPE html>" +
                            "<html>" +
                            "<head>" +
                            "<link href='Content/bootstrap.min.css' type='text/css' rel='stylesheet'/>" +
                            "</head>" +
                            "<body onload='window.print()'>" +
                            "<div style='width: 800px; height:auto;'>" +
                            printContents +
                            "</div>" +
                            "<script src='Scripts/angular.min.js'/>" +
                            "</body>" +
                            "</html>");
                        popupWin.onbeforeunload = function (event) {
                            popupWin.close();
                            return ".n";
                        };
                        popupWin.onabort = function (event) {
                            popupWin.document.close();
                            popupWin.close();
                        };
                    } else {
                        popupWin = window.open("", "_blank", "width=1000,height=auto", true);
                        popupWin.document.open();
                        popupWin.document.write("<!DOCTYPE html>" +
                            "<html>" +
                            "<head>" +
                            "<link href='Content/bootstrap.min.css' type='text/css' rel='stylesheet'/>" +
                            "</head>" +
                            "<body onload='window.print()'>" +
                            "<div style='width: 800px; height:auto;'>" +
                            printContents +
                            "</div>" +
                            "<script src='Scripts/angular.min.js'/>" +
                            "</body>" +
                            "</html>");
                        popupWin.document.close();
                    }
                    popupWin.document.close();
                    //$scope.ok();
                    $scope.cancel();
                    return true;
                };


                $scope.reload = function () {
                    init();
                };



                $scope.loadExporterDropdown = function (viewValue) {

                    if (viewValue.length < 4) return null;

                    return $scope.promise = $http.get(urlService.DropdownUrl + "?type=ExporterTypeAHead&id=" + viewValue, {
                        params: {
                            //address: viewValue,
                            sensor: false
                        }
                    }).then(function (response) {
                        return response.data.map(function (item) {
                            return item;
                        });
                    });
                }


                init();

            }
        ]);