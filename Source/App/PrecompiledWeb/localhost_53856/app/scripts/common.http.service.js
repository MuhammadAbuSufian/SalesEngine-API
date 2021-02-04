angular.module("app")
    .service("CommonHttpService", [
        "$q", "$http",
        function ($q, $http) {
            "use strict";

            var call = function (url) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url).then(self.success, self.error);

                return self.deferred.promise;
            };

            var get = function (url, type) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url, { params: { type: type } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            var find = function (url, id) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url, { params: { id: id } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            var post = function (url, data) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.post(url, JSON.stringify(data)).then(self.success, self.error);

                return self.deferred.promise;
            };


            var update = function (url, data) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.put(url, JSON.stringify(data)).then(self.success, self.error);

                return self.deferred.promise;
            };

            var remove = function (url, actionType, id) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.delete(url, { params: { type: actionType, id: id } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            var load = function (url, type, parentId) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                if (parentId === undefined || parentId === null || parentId === "")
                    $http.get(url, { params: { type: type } }).then(self.success, self.error);
                else
                    $http.get(url, { params: { type: type, id: parentId } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            var loadDataFromUrls = function (urls) {
                var deferred = $q.defer();
                var urlCalls = [];
                angular.forEach(urls,
                    function (url) {
                        urlCalls.push($http.get(url.url));
                    });

                $q.all(urlCalls)
                    .then(function (response) {
                        deferred.resolve(response);
                    },
                        function (errors) {
                            deferred.reject(errors);
                        },
                        function (updates) {
                            deferred.update(updates);
                        }
                    );

                return deferred.promise;
            }

            var search = function (url, type, requestObject) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url, { params: { type: type, request: JSON.stringify(requestObject) } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            var getPagingData = function (url, status, requestObject) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url, { params: { status: status, request: JSON.stringify(requestObject) } })
                    .then(self.success, self.error);

                return self.deferred.promise;
            };


            var loadPagingDataFromUrls = function (urls) {
                var deferred = $q.defer();
                var urlCalls = [];
                angular.forEach(urls,
                    function (url) {
                        urlCalls.push($http.get(url.url));
                    });

                $q.all(urlCalls)
                    .then(function (response) {
                        deferred.resolve(response);
                    },
                        function (errors) {
                            deferred.reject(errors);
                        },
                        function (updates) {
                            deferred.update(updates);
                        }
                    );

                return deferred.promise;
            }

            var pagingSearch = function (url, type, status, requestObject) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function (response) {
                    //console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function (error) {
                    //console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url, { params: { type: type, status: status, request: JSON.stringify(requestObject) } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            return {
                call: call,
                get: get,
                find: find,
                post: post,
                update: update,
                remove: remove,
                load: load,
                loadDataFromUrls: loadDataFromUrls,
                search: search,

                getPagingData: getPagingData,
                loadPagingDataFromUrls: loadPagingDataFromUrls,
                pagingSearch: pagingSearch
            };
        }
    ]);
