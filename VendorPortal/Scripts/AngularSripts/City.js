var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {  
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.City = {};
            $scope.City.CityName = $scope.CityName;
            $scope.City.StateName = $scope.StateName;
            $scope.City.Active = $scope.Active;
            $http({
                method: "post",
                url: '/Home/Insert_City',
                datatype: "json",
                data: JSON.stringify($scope.City)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();                
                $scope.CityName = ""; 
                $scope.StateName = "---Select state---";               
                $scope.Active = "";
            })
        } else {
            $scope.City = {};
            $scope.City.CityName = $scope.CityName;
            $scope.City.StateName = $scope.StateName;
            $scope.City.Active = $scope.Active;
            $scope.City.CityId = document.getElementById("CityID_").value;
            $http({
                method: "post",
                url: '/Home/Update_City',
                datatype: "json",
                data: JSON.stringify($scope.City)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.CityName = "";
                $scope.StateName = "---Select state---";
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
            url: '/Home/Get_AllCity'
        }).then(function (response) {
            $scope.Cities = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    $scope.DeleteCity = function (City) {
        $http({
            method: "post",
            url: '/Home/Delete_City',
            datatype: "json",
            data: JSON.stringify(City)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.UpdateCity = function (City) {
        document.getElementById("CityID_").value = City.CityId;
        $scope.CityName = City.CityName;
        $scope.StateName = City.StateName;
        $scope.Active = City.Active;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("spn").innerHTML = "Update City Information";
    }
})  