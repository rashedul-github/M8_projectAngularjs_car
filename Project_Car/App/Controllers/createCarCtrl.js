angular.module("partsApp")
    .controller("createCarCtrl", ($scope, apiUrl, ApiService) => {
        $scope.activeTab = 0;
        $scope.carInsertMsg = "";
        $scope.currentCar = {};
        $scope.currentParts = {};
        $scope.CarParts = [];
        //date
        $scope.popup1 = {
            opened: false,
        };
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        //
        $scope.CarDone = () => {
            $scope.activeTab = 1;
        };
        $scope.pertsDone = function (frm) {
            $scope.CarParts.push($scope.currentParts);

            $scope.currentParts = {};
            frm.$setPristine();

            //console.log($scope.CarParts);
        };
        $scope.perDel = (index) => {
            $scope.CarParts.splice(index, 1);
        };
        $scope.perPre = () => {
            $scope.activeTab = 0;
        };
        $scope.saveAll = (frms) => {
            $scope.currentCar.carParts = $scope.CarParts;
            ApiService.post(apiUrl + "Cars", $scope.currentCar, null)
                .then(r => {
                    $scope.carInsertMsg = "Data inserted successfully."
                    $scope.$emit('carInserted', r.data);
                    $scope.currentCar = {};
                    $scope.currentParts = {};
                    $scope.CarParts = [];
                    $scope.activeTab = 0;
                    frms[0].$setPristine();
                    frms[1].$setPristine();
                    //console.log(frms);
                    //console.log(r.data);
                }, err => {
                    console.log(err);
                });
        };
        
        
    });