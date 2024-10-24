var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.Material = {};
            $scope.Material.MaterialCode = $scope.MaterialCode;
            $scope.Material.MaterialDescription = $scope.MaterialDescription;
            $scope.Material.Unit = $scope.Unit;
            $scope.Material.MaterialGroup = $scope.MaterialGroup;
            $scope.Material.SNP = $scope.SNP;
            $scope.Material.Active = $scope.Active;
            $http({
                method: "post",
                url: '/Home/Insert_Material',
                datatype: "json",
                data: JSON.stringify($scope.Material)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.MaterialCode = "";
                $scope.MaterialDescription = "";
                $scope.MaterialGroup = "";
                $scope.SNP = "";    
                $scope.Active = "";
                $scope.Unit = "--SELECT UNIT--";
                         
              
            })
        } else {
            $scope.Material = {};
            $scope.Material.MaterialCode = $scope.MaterialCode;
            $scope.Material.MaterialDescription = $scope.MaterialDescription;
            $scope.Material.Unit = $scope.Unit;
            $scope.Material.MaterialGroup = $scope.MaterialGroup;
            $scope.Material.SNP = $scope.SNP;
            $scope.Material.Active = $scope.Active;
            $scope.Material.MaterialID = document.getElementById("MaterialID_").value;
            $http({
                method: "post",
                url: '/Home/Update_Material',
                datatype: "json",
                data: JSON.stringify($scope.Material)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.MaterialCode = "";
                $scope.MaterialDescription = "";
                $scope.MaterialGroup = "";
                $scope.SNP = "";
                $scope.Active = "";
                $scope.Unit = "--SELECT UNIT--";  
               
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Material";
            })
        }
    }
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: '/Home/Get_AllMaterial'
        }).then(function (response) {
            $scope.Materials = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    $scope.DeleteMaterial = function (Material) {
        $http({
            method: "post",
            url: '/Home/Delete_Material',
            datatype: "json",
            data: JSON.stringify(Material)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.UpdateMaterial = function (Material) {
        document.getElementById("MaterialID_").value = Material.MaterialID;
        debugger ;
        $scope.MaterialCode = Material.MaterialCode;
        $scope.MaterialDescription = Material.MaterialDescription;
        $scope.Unit = Material.Unit;
        $scope.MaterialGroup = Material.MaterialGroup;
        $scope.SNP = Material.SNP
        $scope.Active = Material.Active;       
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("spn").innerHTML = "Update Material Information";
    }
})  