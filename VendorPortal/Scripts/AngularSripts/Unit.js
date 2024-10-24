var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.Unit = {};
            $scope.Unit.UnitCode = $scope.UnitCode;
            $scope.Unit.UnitDescription = $scope.UnitDescription;
            $scope.Unit.Active = $scope.Active;
            $http({
                method: "post",
                url: '/Home/Insert_Unit', 
                datatype: "json",
                data: JSON.stringify($scope.Unit)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.UnitCode = "";
                $scope.UnitDescription = "";
                $scope.Active = "";
            })
        } else {
            $scope.Unit = {};
            $scope.Unit.UnitCode = $scope.UnitCode;
            $scope.Unit.UnitDescription = $scope.UnitDescription;
            $scope.Unit.Active = $scope.Active;
            $scope.Unit.UnitId = document.getElementById("UnitID_").value;
            $http({
                method: "post",
                url: '/Home/Update_Unit',
                datatype: "json",
                data: JSON.stringify($scope.Unit)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.UnitCode = "";
                $scope.UnitDescription = "";
                $scope.Active = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Unit";
            })
        }
    }
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: '/Home/Get_AllUnit'
        }).then(function (response) {
            $scope.Units = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    $scope.DeleteUnit = function (Unit) {
        $http({
            method: "post",
            url: '/Home/Delete_Unit',
            datatype: "json",
            data: JSON.stringify(Unit)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.UpdateUnit = function (Unit) {
        document.getElementById("UnitID_").value = Unit.UnitId;
        $scope.UnitCode = Unit.UnitCode;
        $scope.UnitDescription = Unit.UnitDescription;
        $scope.Active = Unit.Active;
        document.getElementById("btnSave").setAttribute("value", "Update");         
        document.getElementById("spn").innerHTML = "Update Unit Information";
    }
})  