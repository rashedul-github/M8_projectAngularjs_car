angular.module("partsApp")
    .filter("pageCount", () => {
        return (data, perPage) => {
            if (angular.isArray(data) && angular.isNumber(perPage)) {
                var r = [];
                var count = Math.ceil(data.length / perPage);
                for (var i = 0; i < count; i++) {
                    r.push(i);
                }
                return r;
            }
            else {
                return data;
            }
        }
    })
    .filter("unique", () => {
        return (input, prop) => {
            if (angular.isArray(input) && angular.isString(prop)) {
                var keys = [];
                var data = [];
                angular.forEach(input, function (item) {
                    var key = item[prop];
                    if (keys.indexOf(key) == -1) {
                        keys.push(key);
                        data.push(item);
                    }
                })

                return data;
            }
            else
                return input;

        }
    })
    .filter("range", function ($filter) {
        return function (data, page, size) {

            if (angular.isArray(data) && angular.isNumber(page) && angular.isNumber(size)) {
                var start_index = (page - 1) * size;
                if (data.length < start_index) {
                    return [];
                } else {
                    var x = $filter("limitTo")(data.splice(start_index), size);

                    return x;
                }
            } else {
                return data;
            }
        }
    });