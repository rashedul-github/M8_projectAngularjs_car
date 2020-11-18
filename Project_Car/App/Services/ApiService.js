angular.module("partsApp")
    .factory("ApiService", ($http) => {
        return {
            get: (url, headers) => {
                return $http({
                    method: "GET",
                    url: url,
                    headers: headers
                });
            },
            put: (url, data, headers) => {
                return $http({
                    method: "PUT",
                    url: url,
                    data: data,
                    headers: headers
                });
            },
            post: (url, data, headers) => {
                return $http({
                    method: "POST",
                    url: url,
                    data: data,
                    headers: headers
                });
            },
            delete: (url, headers) => {
                return $http({
                    method: "DELETE",
                    url: url,
                    headers: headers
                });
            }
        };
    });