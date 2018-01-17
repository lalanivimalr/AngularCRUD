var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    $scope.Employee = {};
    $scope.Employee.Emp_Name = "";
    $scope.Employee.Emp_Age = "";
    $scope.Employee.Emp_City = "";
    $scope.GetEmployeeData = function () {
        $http({
            method: "get",
            url: "http://localhost:52155/Employee/GetAllEmployee"
        }).then(function (response) {
            $scope.employees = response.data;
        }, function (response) {
            alert("Error Occur", response.data);
        });
    }
    $scope.delete = function (emp) {
        $http({
            method: "post",
            url: "http://localhost:52155/Employee/DeleteEmployee",
            datatype: "json",
            data: JSON.stringify(emp)
        }).then(function (response) {
            $http({
                method: "get",
                url: "http://localhost:52155/Employee/GetAllEmployee"
            }).then(function (response) {
                $scope.employees = response.data;
            }, function (response) {
                alert("Error Occur", response.data);
            });
        
            alert(response.data);
        }, function (response) {
            alert("Error Occur", response.data);
        });        
    }
    $scope.Add = function () {
        $http({
            method: "post",
            url: "http://localhost:52155/Employee/AddEmployee",
            datatype: "json",
            data: JSON.stringify($scope.Employee)
        }).then(function (response) {
            $scope.GetEmployeeData();
            alert(response.data);
        }, function (response) {
            alert("Error Occur", response.data);
        });
    }
});