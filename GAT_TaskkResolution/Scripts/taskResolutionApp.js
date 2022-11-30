var taskRes = angular.module("taskRes", []);

taskRes.controller("parent", function ($scope, $http, $filter) {

    $scope.student = {};
    $scope.students = [];

    $scope.subject = {};
    $scope.subjects = [];

    $scope.init = function () {
        $scope.GetAllStudents();
        $scope.GetAllSubjects();
    };

    $scope.GetAllSubjects = function () {
        $http({
            method: "GET",
            url: "http://localhost:64305/api/subjects"
        }).then(function (response) {
            $scope.subjects = response.data;
            console.log($scope.subjects);
        }, function (e) {
            console.log(e);
            $scope.Alert('error', e);
        });
    }

    $scope.GetAllStudents = function () {
        $http({
            method: "GET",
            url: "http://localhost:64305/api/students"
        }).then(function (response) {
            $scope.students = response.data;
            console.log($scope.students);
        }, function (e) {
            console.log(e);
            $scope.Alert('error', e);
        });
    };

    $scope.InsertStudent = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $http({
                method: "POST",
                url: "http://localhost:64305/api/students",
                datatype: "json",
                data: JSON.stringify($scope.student)
            }).then(function (response) {
                $scope.Alert('success', 'Student added successfully');
                $scope.GetAllStudents();
                $scope.ResetForm();
            }, function (e) {
                console.log(e);
                $scope.Alert('error', e);
            });
            //console.log($scope.student);
        } else {
            $http({
                method: "PUT",
                url: "http://localhost:64305/api/students",
                datatype: "json",
                data: JSON.stringify($scope.student)
            }).then(function (response) {
                $scope.Alert('success', 'Student updated successfully');
                $scope.GetAllStudents();
                $scope.ResetForm();
            }, function (e) {
                console.log(e);
                $scope.Alert('error', e);
            })
        }
    };

    $scope.UpdateStudent = function (student) {
        student.BirthDate = $filter('date')(student.BirthDate, "yyyy-MM-dd")
        $scope.student = student;        
        $('#btnSave').prop('value', 'Update Student').removeClass('btn-success').addClass('btn-warning');
        $("#spn").html("Update Student");
        $('#inputNumber').prop('readonly', true);
    };

    $scope.DeleteStudent = function (student) {
        $http({
            method: "delete",
            url: "http://localhost:64305/api/students?Number=" + student.Number,
        }).then(function (response) {
            $scope.Alert('success', 'Student deleted successfully');
            $scope.GetAllStudents();            
        }, function (e) {
            console.log(e);
            $scope.Alert('error', e);
        })
    };

    $scope.ResetForm = function () {
        $scope.student = {};
        $('#btnSave').prop('value', 'Submit').removeClass('btn-warning').addClass('btn-success');
        $("#spn").html("New Student");
        $('#inputNumber').prop('readonly', false);
        $('#subjectModal').modal('hide');
    };

    $scope.DeleteSubject = function (studentNumber, subjectCode) {
        $http({
            method: "delete",
            url: "http://localhost:64305/api/students/" + studentNumber + "/DeleteSubject/" + subjectCode,
        }).then(function (response) {
            $scope.Alert('success', 'Subject deleted successfully');
            $scope.GetAllStudents();
        }, function (e) {
            console.log(e);
            $scope.Alert('error', e);
        })
    };

    $scope.AddSubject = function (student) {
        $scope.student = student;
        $('#subjectModal').modal('show');
    }

    $scope.InsertSubject = function (studentNumber, subjectCode) {
        $http({
            method: "POST",
            url: "http://localhost:64305/api/students/" + studentNumber + "/AddSubject/" + subjectCode,
            datatype: "json",
        }).then(function (response) {
            $scope.Alert('success', 'Subject added successfully');
            $scope.GetAllStudents();
            $scope.ResetForm();
        }, function (e) {
            console.log(e);
            $scope.Alert('error', e);
        });
    }

    $scope.Alert = function (type, message) {
        if (type === "success") {
            $('#alert .alertText').text(message);
            $("#alert").removeClass("alert-danger").addClass("alert-success");
        }
        else if (type == "error") {
            if (message.data.length > 0)
                $('#alert .alertText').text(message.data);
            else
                $('#alert .alertText').text(message.statusText);
            $("#alert").removeClass("alert-success").addClass("alert-danger");
        }        
        $(".alert").removeClass('hidden');
        $(".alert").alert()
    }
});

taskRes.directive("formatDate", function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attr, modelDate) {
                modelDate.$formatters.push(function (modelValue) {
                    return new Date(modelValue);
                })
            }
        }
})

taskRes.filter('ifEmpty', function () {
    return function (input, defaultValue) {
        if (angular.isUndefined(input) || input === null || input === '') {
            return defaultValue;
        }

        return input;
    }
});

taskRes.filter('studentFullName', function () {
    return function (student) {
        var fullName = student.FirstName + ' ';
        if (student.MidName !== null && student.MidName !== '')
            fullName += student.MidName + ' ';
        fullName += student.LastName;
        return fullName;
        };
});

