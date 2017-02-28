/*jshint -W069 */
/*global angular:false */
angular.module('Model', [])
    .factory('SampleApi', ['$q', '$http', '$rootScope', function ($q, $http, $rootScope) {
        'use strict';

        /**
         *
         * @class SampleApi
         * @param {(string|object)} [domainOrOptions] - The project domain or options object. If object, see the object's optional properties.
         * @param {string} [domainOrOptions.domain] - The project domain
         * @param {string} [domainOrOptions.cache] - An angularjs cache implementation
         * @param {object} [domainOrOptions.token] - auth token - object with value property and optional headerOrQueryName and isQuery properties
         * @param {string} [cache] - An angularjs cache implementation
         */
        var SampleApi = (function () {
            function SampleApi(options, cache) {
                var domain = (typeof options === 'object') ? options.domain : options;
                this.domain = typeof (domain) === 'string' ? domain : '';
                if (this.domain.length === 0) {
                    throw new Error('Domain parameter must be specified as a string.');
                }
                cache = cache || ((typeof options === 'object') ? options.cache : cache);
                this.cache = cache;
            }

            SampleApi.prototype.$on = function ($scope, path, handler) {
                var url = domain + path;
                $scope.$on(url, function () {
                    handler();
                });
                return this;
            };

            SampleApi.prototype.$broadcast = function (path) {
                var url = domain + path;
                //cache.remove(url);
                $rootScope.$broadcast(url);
                return this;
            };

            SampleApi.transformRequest = function (obj) {
                var str = [];
                for (var p in obj) {
                    var val = obj[p];
                    if (angular.isArray(val)) {
                        val.forEach(function (val) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(val));
                        });
                    } else {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(val));
                    }
                }
                return str.join("&");
            };

            /**
             *
             * @method
             * @name SampleApi#Contacts_Get
             *
             */
            SampleApi.prototype.Contacts_Get = function (parameters) {
                if (parameters === undefined) {
                    parameters = {};
                }
                var deferred = $q.defer();

                var domain = this.domain;
                var path = '/api/Contacts';

                var body;
                var queryParameters = {};
                var headers = {};
                var form = {};

                if (parameters.$queryParameters) {
                    Object.keys(parameters.$queryParameters)
                        .forEach(function (parameterName) {
                            var parameter = parameters.$queryParameters[parameterName];
                            queryParameters[parameterName] = parameter;
                        });
                }

                //var url = domain + path;
                var url = path;
                var cached = parameters.$cache && parameters.$cache.get(url);
                if (cached !== undefined && parameters.$refresh !== true) {
                    deferred.resolve(cached);
                    return deferred.promise;
                }
                var options = {
                    timeout: parameters.$timeout,
                    method: 'GET',
                    url: url,
                    params: queryParameters,
                    data: body,
                    headers: headers
                };
                if (Object.keys(form).length > 0) {
                    options.data = form;
                    options.headers['Content-Type'] = 'application/x-www-form-urlencoded';
                    options.transformRequest = SampleApi.transformRequest;
                }
                $http(options)
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                        if (parameters.$cache !== undefined) {
                            parameters.$cache.put(url, data, parameters.$cacheItemOpts ? parameters.$cacheItemOpts : {});
                        }
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({
                            status: status,
                            headers: headers,
                            config: config,
                            body: data
                        });
                    });

                return deferred.promise;
            };
            /**
             *
             * @method
             * @name SampleApi#Contacts_Add
             * @param {} user - 
             *
             */
            SampleApi.prototype.Contacts_Add = function (parameters) {
                if (parameters === undefined) {
                    parameters = {};
                }
                var deferred = $q.defer();

                var domain = this.domain;
                var path = '/api/Contacts';

                var body;
                var queryParameters = {};
                var headers = {};
                var form = {};

                if (parameters['user'] !== undefined) {
                    body = parameters['user'];
                }

                if (parameters['user'] === undefined) {
                    deferred.reject(new Error('Missing required  parameter: user'));
                    return deferred.promise;
                }

                if (parameters.$queryParameters) {
                    Object.keys(parameters.$queryParameters)
                        .forEach(function (parameterName) {
                            var parameter = parameters.$queryParameters[parameterName];
                            queryParameters[parameterName] = parameter;
                        });
                }

                //var url = domain + path;
                var url = path;
                var options = {
                    timeout: parameters.$timeout,
                    method: 'POST',
                    url: url,
                    params: queryParameters,
                    //data: body,
                    headers: headers
                };
                if (Object.keys(form).length > 0) {
                    options.data = form;
                    options.headers['Content-Type'] = 'application/x-www-form-urlencoded';
                    options.transformRequest = SampleApi.transformRequest;
                }
                $http(options)
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                        if (parameters.$cache !== undefined) {
                            parameters.$cache.put(url, data, parameters.$cacheItemOpts ? parameters.$cacheItemOpts : {});
                        }
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({
                            status: status,
                            headers: headers,
                            config: config,
                            body: data
                        });
                    });

                return deferred.promise;
            };
            /**
             *
             * @method
             * @name SampleApi#Contacts_UpdateUSer
             * @param {string} id - 
             * @param {} user - 
             *
             */
            SampleApi.prototype.Contacts_UpdateUSer = function (parameters) {
                if (parameters === undefined) {
                    parameters = {};
                }
                var deferred = $q.defer();

                var domain = this.domain;
                var path = '/api/Contacts/{id}';

                var body;
                var queryParameters = {};
                var headers = {};
                var form = {};

                path = path.replace('{id}', parameters['id']);

                if (parameters['id'] === undefined) {
                    deferred.reject(new Error('Missing required  parameter: id'));
                    return deferred.promise;
                }

                if (parameters['user'] !== undefined) {
                    body = parameters['user'];
                }

                if (parameters['user'] === undefined) {
                    deferred.reject(new Error('Missing required  parameter: user'));
                    return deferred.promise;
                }

                if (parameters.$queryParameters) {
                    Object.keys(parameters.$queryParameters)
                        .forEach(function (parameterName) {
                            var parameter = parameters.$queryParameters[parameterName];
                            queryParameters[parameterName] = parameter;
                        });
                }

                //var url = domain + path;
                var url = path;
                var options = {
                    timeout: parameters.$timeout,
                    method: 'PUT',
                    url: url,
                    params: queryParameters,
                    //data: body,
                    headers: headers
                };
                if (Object.keys(form).length > 0) {
                    options.data = form;
                    options.headers['Content-Type'] = 'application/x-www-form-urlencoded';
                    options.transformRequest = SampleApi.transformRequest;
                }
                $http(options)
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                        if (parameters.$cache !== undefined) {
                            parameters.$cache.put(url, data, parameters.$cacheItemOpts ? parameters.$cacheItemOpts : {});
                        }
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({
                            status: status,
                            headers: headers,
                            config: config,
                            body: data
                        });
                    });

                return deferred.promise;
            };
            /**
             *
             * @method
             * @name SampleApi#Contacts_DeleteUser
             * @param {string} id - 
             *
             */
            SampleApi.prototype.Contacts_DeleteUser = function (parameters) {
                if (parameters === undefined) {
                    parameters = {};
                }
                var deferred = $q.defer();

                var domain = this.domain;
                var path = '/api/Contacts/{id}';

                var body;
                var queryParameters = {};
                var headers = {};
                var form = {};

                path = path.replace('{id}', parameters['id']);

                if (parameters['id'] === undefined) {
                    deferred.reject(new Error('Missing required  parameter: id'));
                    return deferred.promise;
                }

                if (parameters.$queryParameters) {
                    Object.keys(parameters.$queryParameters)
                        .forEach(function (parameterName) {
                            var parameter = parameters.$queryParameters[parameterName];
                            queryParameters[parameterName] = parameter;
                        });
                }

                //var url = domain + path;
                var url = path;
                var options = {
                    timeout: parameters.$timeout,
                    method: 'DELETE',
                    url: url,
                    params: queryParameters,
                    //data: body,
                    headers: headers
                };
                if (Object.keys(form).length > 0) {
                    options.data = form;
                    options.headers['Content-Type'] = 'application/x-www-form-urlencoded';
                    options.transformRequest = SampleApi.transformRequest;
                }
                $http(options)
                    .success(function (data, status, headers, config) {
                        deferred.resolve(data);
                        if (parameters.$cache !== undefined) {
                            parameters.$cache.put(url, data, parameters.$cacheItemOpts ? parameters.$cacheItemOpts : {});
                        }
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject({
                            status: status,
                            headers: headers,
                            config: config,
                            body: data
                        });
                    });

                return deferred.promise;
            };

            return SampleApi;
        })();

        return SampleApi;
    }])

    .controller('ModelController', function (SampleApi, $scope, $http) {
        $scope.Get_Contacts = function () {
            var result = SampleApi.prototype.Contacts_Get();
            result.then(function (data) {
                $scope.contacts = data;
            });
        };

        $scope.Add_Contacts = function (user) {
            var result = SampleApi.prototype.Contacts_Add(user);
            result.then(function (data) {
                $scope.user = data;
            });
        };
        var httpData = {
            user: {
                'Id': 1,
                'Name': 'lakshman',
                'Email': 'lpeethan@xya.com',
                'Age': 0,
                'DateOfBirth': '0001-01-01T00:00:00',
                'Addresses': null,
                'Intrests': null,
                'IsAlive': false,
                'MaritalStatus': 0
            }
        };
        $scope.Update_Contacts = function (user) {
            var result = SampleApi.prototype.Contacts_UpdateUSer(user);
            result.then(function (data) {
                $scope.updateStatus = data;
            });
        };
        $scope.Delete_Contacts = function (user) {
            var result = SampleApi.prototype.Contacts_DeleteUser(user);
            result.then(function (data) {
                $scope.deleteStatus = data;
            });
        };
    });
