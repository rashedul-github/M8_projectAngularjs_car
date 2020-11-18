angular.module("partsApp")
    .controller("editPartsCtrl", ($scope, $routeParams, $location, apiUrl, ApiService) => {

        var id = $routeParams.id;
        $scope.currentParts = null;

        $scope.updatePartsMsg = "";
        if (id == null) {
            $scope.errorMsg = "Parts id not available.";
            $location.path("/car");
        }
        ApiService.get(apiUrl + "CarParts/" + id, null)
            .then(r => {
                $scope.currentParts = r.data;
                console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.partsEditDone = (frm) => {
            ApiService.put(apiUrl + "CarParts/" + id, $scope.currentParts, null)
                .then(r => {
                    //console.log(r);
                    $scope.$emit('partsUpdated',$scope.currentParts);
                    $scope.updatePartsMsg = "Data updated."
                    $scope.currentParts = {};
                    frm.$setPristine();
                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
    });