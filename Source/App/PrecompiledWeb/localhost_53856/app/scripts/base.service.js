
angular.module("app")
    .service("BaseService",
    ["UrlService",
        function (urlService) {
            "use strict";

            var self = this;
            self.currentUrl = urlService.baseUrl;
            self.isLoadDataFromUrls = false;
            self.urls = [];
            self.dataProperties = [];
            self.searchType = "General";
            self.isLoadPagingData = true;
            self.dataStatus = "Active";
            self.callerStatus = true;
            self.pageCount = 10;
            self.page = 1;
            //self.model = new Import;
            self.TransectionID = "";
            self.TransectionDate = new Date();

            var setCurrentApi = function (url) {
                self.currentUrl = urlService.baseUrl;
                self.currentUrl = url;
            };

            var getCurrentApi = function () {
                return self.currentUrl;
            };

            var setIsLoadDataFromUrls = function (bolleanValue) {
                self.isLoadDataFromUrls = false;
                self.isLoadDataFromUrls = bolleanValue;
            };

            var getIsLoadDataFromUrls = function () {
                return self.isLoadDataFromUrls;
            };

            var setUrls = function (urls) {
                self.urls = [];
                self.urls = urls;
            };

            var getUrls = function () {
                return self.urls;
            };

            var setDataProperties = function (dataProperties) {
                self.dataProperties = [];
                self.dataProperties = dataProperties;
            };

            var getDataProperties = function () {
                return self.dataProperties;
            };

            var setSearchType = function (searchType) {
                self.searchType = "General";
                self.searchType = searchType;
            };

            var getSearchType = function () {
                return self.searchType;
            };

            var setIsLoadPagingData = function (isLoadPagingData) {
                self.isLoadPagingData = true;
                self.isLoadPagingData = isLoadPagingData;
            };

            var getIsLoadPagingData = function () {
                return self.isLoadPagingData;
            };

            var setDataStatus = function (status) {
                self.dataStatus = "Active";
                self.dataStatus = status;
            };

            var getDataStatus = function () {
                return self.dataStatus;
            };

            var setCallerStatus = function (callerStatus) {
                self.callerStatus = true;
                self.callerStatus = callerStatus;
            };

            var getCallerStatus = function () {
                return self.callerStatus;
            };

            var setPageCount = function (count) {
                self.pageCount = 10;
                self.pageCount = count;
            }

            var getPageCount = function () {
                return self.pageCount;
            }

            var setPage = function (page) {
                self.page = 1;
                self.page = page;
            }

            var getPage = function () {
                self.page = 1;
                return self.page;
            }

            var setTransectionID = function(TransectionID) {
                self.TransectionID = TransectionID;
            }

            var getTransectionID = function() {
                return self.TransectionID;
            }
            var setTransectionDate = function(TransectionDate) {
                self.TransectionDate = TransectionDate;
            }

            var getTransectionDate = function () {

                return self.TransectionDate;
            }

            return {
                setCurrentApi: setCurrentApi,
                getCurrentApi: getCurrentApi,
                setIsLoadDataFromUrls: setIsLoadDataFromUrls,
                getIsLoadDataFromUrls: getIsLoadDataFromUrls,
                setUrls: setUrls,
                getUrls: getUrls,
                setDataProperties: setDataProperties,
                getDataProperties: getDataProperties,
                setSearchType: setSearchType,
                getSearchType: getSearchType,
                setIsLoadPagingData: setIsLoadPagingData,
                getIsLoadPagingData: getIsLoadPagingData,
                setDataStatus: setDataStatus,
                getDataStatus: getDataStatus,
                setCallerStatus: setCallerStatus,
                getCallerStatus: getCallerStatus,
                setPageCount: setPageCount,
                getPageCount: getPageCount,
                setPage: setPage,
                getPage: getPage,
                setTransectionID: setTransectionID,
                getTransectionID: getTransectionID,
                setTransectionDate: setTransectionDate,
                getTransectionDate : getTransectionDate
            };

        }
    ]);