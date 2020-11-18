angular.module("partsApp")
    .controller("editCarAtrl", ($scope, $routeParams, apiUrl, customUrl, ApiService) => {
        var id = $routeParams.id;
        $scope.carToEdit = {};
        $scope.newCars = {};
        $scope.newParts = {};
        $scope.newPartsModal = {};
        //date
        $scope.popup2 = {
            opened: false,
        };
        $scope.open2 = function () {
            $scope.popup2.opened = true;
        };
        //
        ApiService.get(customUrl + "Cars/" + id)
            .then(r => {
                $scope.carToEdit = r.data;
                r.data.LunchDate = new Date(r.data.LunchDate);
            }, err => {
                console.log(err);
            });
        $scope.delCarParts = (index) => {
            $scope.carToEdit.CarParts.splice(index, 1);

        }
 
        $scope.editDone = (frm) => {
            ApiService.put(apiUrl + "Cars/" + $scope.carToEdit.CarId, $scope.carToEdit, null)
                .then(r => {
                    //console.log(r);
                    $scope.$emit("carUpdated", $scope.carToEdit);
                    $scope.carEditMsg = "Data updated successfuly.";
                    console.log($scope.carToEdit);
                    $scope.carToEdit = {};
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                })
        }
        //modal perts
        $scope.openAddPartsDialog = function () {
            //console.log($scope.companyToEdit.CompanyId)
            $("#addPartsModal").modal('show');
        }
        $scope.AddPartsModal = (frm) => {
            console.log(frm);
            $scope.newPartsModal.CarId = $scope.carToEdit.CarId;
            $scope.carToEdit.CarParts.push($scope.newPartsModal);
            frm.$setPristine();
            $scope.newPartsModal = {};
            $("#addPartsModal").modal('hide');
        }
        

    });