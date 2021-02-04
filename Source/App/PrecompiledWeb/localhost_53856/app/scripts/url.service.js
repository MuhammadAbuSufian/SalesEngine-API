
angular.module("app")
    .service("UrlService", [
        function () {
            "use strict";

            var self = this;

            self.url = "http://localhost:54652/";
            
            self.urls = [];
            self.urls.baseUrl = self.url;
            self.urls.baseApi = self.url + "api/";

            self.urls.TokenUrl = self.urls.baseUrl + "token";
            self.urls.AccountUrl = self.urls.baseApi + "Account";
            self.urls.DropdownUrl = self.urls.baseApi + "Dropdown";
            self.urls.AutoGenUrl = self.urls.baseApi + "AutoGen";

            self.urls.RoleUrls = roleUrls(self.urls.baseApi);
            self.urls.ResourceUrls = resourceUrls(self.urls.baseApi);
            self.urls.PermissionUrls = permissionUrls(self.urls.baseApi);
            self.urls.ProfileUrls = profileUrls(self.urls.baseApi);
            self.urls.UserUrls = userUrls(self.urls.baseApi);

            self.urls.HomeUrls = homeUrls(self.urls.baseApi);
            self.urls.DashboardUrls = dashboardUrls(self.urls.baseApi);
            self.urls.ServiceTypeUrls = serviceTypeUrls(self.urls.baseApi);
            self.urls.IssueNameUrls = issueNameUrls(self.urls.baseApi);
            self.urls.ServiceIssueTypeUrls = serviceIssueTypeUrls(self.urls.baseApi);
            self.urls.ExporterUrls = exporterUrls(self.urls.baseApi);
            self.urls.NonExporterUrls = nonExporterUrls(self.urls.baseApi);
            self.urls.AccountUrls = accountUrls(self.urls.baseApi);
            self.urls.EpbServiceUrls = epbServiceUrls(self.urls.baseApi);            
            self.urls.ExporterEpbServiceUrls = exporterEpbServiceUrls(self.urls.baseApi);
            self.urls.ImportUrls = importUrls(self.urls.baseApi);
            self.urls.ImportLogUrls = importLogUrls(self.urls.baseApi);

            self.urls.SettingUrls = settingUrls(self.urls.baseApi);

            self.urls.EpbServiceLogUrl = self.urls.baseApi + "EpbServiceLog";
            self.urls.ExporterEpbServicePrintLogUrl = self.urls.baseApi + "ExporterEpbServicePrintLog";

            self.urls.ReportUrls = reportUrls(self.urls.baseApi);

            return self.urls;
        }
    ]);

var reportUrls = function (baseApiUrl) {
    var urls = [];
    urls.Report = baseApiUrl + "Report";
    urls.AccountReport = baseApiUrl + "AccountReport";
    urls.PayslipReport = baseApiUrl + "PayslipReport";
    urls.ServiceReport = baseApiUrl + "ServiceReport";
    return urls;
};

var settingUrls = function (baseApiUrl) {
    var urls = [];
    urls.Setting = baseApiUrl + "Setting";
    return urls;
}
var importLogUrls = function (baseApiUrl) {
    var urls = [];
   
    urls.ImportLog = baseApiUrl + "ImportLog";
    urls.GetByTransaction = urls.ImportLog + "/GetByTransaction";  
    return urls;
}

var importUrls = function (baseApiUrl) {
    var urls = [];
    urls.Import = baseApiUrl + "Import";
    urls.UploadFile = urls.Import + "/UploadFile";
    urls.GetByTransaction = urls.Import + "/GetByTransaction";
    return urls;
};

var exporterEpbServiceUrls = function (baseApiUrl) {
    var urls = [];
    urls.ExporterEpbService = baseApiUrl + "ExporterEpbService";
    return urls;
};

var epbServiceUrls = function (baseApiUrl) {
    var urls = [];
    urls.EpbService = baseApiUrl + "EpbService";
    return urls;
};

var accountUrls = function (baseApiUrl) {
    var urls = [];
    urls.EpbAccount = baseApiUrl + "EpbAccount";
    urls.CheckAvailability = urls.EpbAccount + "/CheckAvailability";
    return urls;
};

var exporterUrls = function (baseApiUrl) {
    var urls = [];
    urls.Exporter = baseApiUrl + "Exporter";
    return urls;
};

var nonExporterUrls = function (baseApiUrl) {
    var urls = [];
    urls.NonExporter = baseApiUrl + "TempExporter";
    urls.NonExporterSave = urls.NonExporter + "/NonExporterSave";
    return urls;
};

var serviceIssueTypeUrls = function (baseApiUrl) {
    var urls = [];
    urls.ServiceIssueType = baseApiUrl + "ServiceIssueType";
    return urls;
};

var serviceTypeUrls = function (baseApiUrl) {
    var urls = [];
    urls.ServiceType = baseApiUrl + "ServiceType";
    return urls;
};

var issueNameUrls = function (baseApiUrl) {
    var urls = [];
    urls.IssueName = baseApiUrl + "IssueName";
    return urls;
};

var dashboardUrls = function (baseApiUrl) {
    var urls = [];
    urls.Dashboard = baseApiUrl + "Dashboard";
    return urls;
};

var homeUrls = function (baseApiUrl) {
    var urls = [];
    urls.Home = baseApiUrl + "Home";
    return urls;
};

var userUrls = function (baseApiUrl) {
    var urls = [];
    urls.User = baseApiUrl + "User";
    urls.GetUser = urls.User + "/GetUser";
    urls.GetUsers = urls.User + "/GetUsers";
    urls.CreateUser = urls.User + "/CreateUser";
    urls.UpdateUser = urls.User + "/UpdateUser";
    urls.DeleteUser = urls.User + "/DeleteUser";
    return urls;
};

var profileUrls = function (baseApiUrl) {
    var urls = [];
    urls.Profile = baseApiUrl + "Profile";
    urls.UserProfile = urls.Profile + "/UserProfile";
    urls.UpdateProfile = urls.Profile + "/UpdateProfile";
    urls.UpdatePassword = urls.Profile + "/UpdatePassword";
    return urls;
};

var permissionUrls = function (baseApiUrl) {
    var urls = [];
    urls.Permission = baseApiUrl + "Permission";
    urls.GetListById = urls.Permission + "/GetListById";
    urls.CreatePermissions = urls.Permission + "/AddList";
    urls.CheckPermission = urls.Permission + "/CheckPermission";
    return urls;
};

var resourceUrls = function (baseApiUrl) {
    var urls = [];
    urls.Resource = baseApiUrl + "Resource";
    urls.GetPrivateResources = urls.Resource + "/GetPrivateResources";
    return urls;
};

var roleUrls = function (baseApiUrl) {
    var urls = [];
    urls.Role = baseApiUrl + "Role";
    return urls;
};