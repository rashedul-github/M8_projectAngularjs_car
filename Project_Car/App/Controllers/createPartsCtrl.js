angular.module("partsApp")
    .controller("createPartsCtrl", ($scope, apiUrl, ApiService) => {
        $scope.currentPartsSingle = null;
        $scope.newPartsMsg = "";
        $scope.pertsCreateDone = (frm) => {
            ApiService.post(apiUrl + "CarParts", $scope.currentPartsSingle, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('partsInserted', r.data);
                    $scope.currentPartsSingle = {};
                    // $location.path("/companies");
                    $scope.newPartsMsg = "Data inserted";
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }
    });