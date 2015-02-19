
var app = angular.module('shurly', ['ngCookies', 'toastr']);

app.controller('shurlyController', [
    '$scope', '$rootScope', '$http', '$cookieStore', 'toastr', 'base64', function ($scope, $rootScope, $http, $cookieStore, toastr, base64) {
        if (!location.origin)
            location.origin = location.protocol + "//" + location.host;

        $scope.createAccount = function (accountId) {
            if ($scope.accountForm.$valid) {
                var data = {
                    accountId: accountId
                };

                $http.post('/account', data).
                    success(function(data, status, headers, config) {
                        if (data.success) {
                            toastr.success(data.description + " Your password is: " + data.password, 'Success', { timeOut: 999999, tapToDismiss: false });
                            $scope.username = accountId;
                            $scope.password = data.password;
                            $scope.statisticsLink = location.origin + "/statistic/" + accountId;
                        } else {
                            toastr.error(data.description);
                        }
                    }).
                    error(function(data, status, headers, config) {
                        toastr.error(data.message);
                    });
            } else {
                toastr.error("Form invalid. Enter required fields.");
            };
        }

        $scope.register = function (username, password, url, redirectType) {
            setCredentials(username, password);
            if ($scope.registerForm.$valid) {
                if (url.substring(0, 3) === 'www') {
                    url = 'http://' + url;
                }

                var data = {
                    url: url
                };

                if (redirectType) {
                    data.redirectType = redirectType;
                }

                $http.post('/register', data).
                    success(function (data, status, headers, config) {

                        $scope.shurly = location.origin + "/" + data.shortUrl;
                        toastr.success("Url successfully registered, your shurly is " + $scope.shurly, 'Success', { timeOut: 999999, tapToDismiss: false });
                    }).
                    error(function (data, status, headers, config) {
                        if (status === 401) {
                            toastr.error("Unauthorized access, enter your credentials");
                        } else if (data) {
                            toastr.error(data);
                        } else {
                            toastr.error("There has been an error");
                        }
                    });
            } else {
                toastr.error("Form invalid. Enter required fields.");
            }
        };

        $scope.openStats = function(event, url) {
            if (event) {
                event.preventDefault();
            }

            setCredentials($scope.username, $scope.password);
            $http.get('/statistic/' + $scope.username).
                    success(function (data, status, headers, config) {
                    $scope.statistics = data;
                    }).
                    error(function (data, status, headers, config) {
                        toastr.error(data.message);
                    });
        }

        var setCredentials = function (username, password) {
            var authdata = base64.encode(username + ':' + password);

            $rootScope.globals = {
                currentUser: {
                    username: username,
                    authdata: authdata
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata;
            $cookieStore.put('globals', $rootScope.globals);
        };

    }
]);

app.factory('base64', function () {

    var keyStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

    return {
        encode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            do {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output +
                    keyStr.charAt(enc1) +
                    keyStr.charAt(enc2) +
                    keyStr.charAt(enc3) +
                    keyStr.charAt(enc4);
                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";
            } while (i < input.length);

            return output;
        },

        decode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            var base64test = /[^A-Za-z0-9\+\/\=]/g;
            if (base64test.exec(input)) {
                window.alert("There were invalid base64 characters in the input text.\n" +
                    "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                    "Expect errors in decoding.");
            }
            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

            do {
                enc1 = keyStr.indexOf(input.charAt(i++));
                enc2 = keyStr.indexOf(input.charAt(i++));
                enc3 = keyStr.indexOf(input.charAt(i++));
                enc4 = keyStr.indexOf(input.charAt(i++));

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + String.fromCharCode(chr1);

                if (enc3 != 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 != 64) {
                    output = output + String.fromCharCode(chr3);
                }

                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";

            } while (i < input.length);

            return output;
        }
    };

});
