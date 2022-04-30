$(document).ready(function () {

    GetAllData();
    BindSectionDropdown();
    $(document).on("click", "#btnSubmit", function () {
        SubmitClassData();
    });

    $(document).on("click", "#btnAdd", function () {
        $("#txtClassName").val("");
        $('#classModal').modal('show');

    });
    $(document).on("click", "#btnClose", function () {
        $("#txtClassName").val("");
        $('#classModal').modal('hide');
    });
   
})
 
function SubmitClassData() {
    if ($("#txtClassName").val() == "") {
        $("#txtClassName").focus()
        return false;
    }
    if ($("#ddlSection").val() == "0") {
        $("#ddlSection").focus()
        alert("Please Select Section");
        return false;
    }

    var data = {
        ClassName: $("#txtClassName").val(),
        SectionId: $("#ddlSection").val()
    }

    $.ajax({

        type: "POST",
        url: baseurl +  "/Admin/SaveClassData",
        data: data,
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.message == "Success") {
                alert("Data Added Successfully!");
                $('#classModal').modal('hide');
                GetAllData();
            }
            else {
                alert(response.message);
            }
        }, error: function (result) {
            alert("Something went wrong");
        }

    })
}

function GetAllData()
{
    $("#tblClass").empty();
    $.ajax({
        type: "GET",
        url: baseurl + "/Admin/GetAllClassMasterData",
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.result.length > 0) {
                var totalCount = response.result.length;
                var classData = response.result;
                var row = "";
                for (var i = 0; i < totalCount; i++) {
                    row += "<tr>";
                    row += "<td>" + classData[i].className + "</td>"
                    row += "<td>" + classData[i].sectionName + "</td>"
                    row += "<td>";
                    row += "<a style='padding-right:12px; cursor:pointer;'>";
                    row += "<img id='EditClassData' data-id='" + classData[i].classId + "' src='../Content/images/icons/admin-edit.svg'>";
                    row += "</a>";
                    row += "<a style='cursor:pointer;'>";
                    row += "<img data-id='" + classData[i].classId + "' id='btnSectionDelete' src='../Content/images/icons/delete.svg'>";
                    row += "</a>";
                    row += "</td>";
                    row += "</tr>";
                }
                $("#tblClass").append(row);
            }
            else {
                alert("No Record Found");
            }
        }, error: function (result) {
            alert("Something went wrong");
        }
    });
}


function BindSectionDropdown() {
    $.ajax({
        type: "GET",
        url: baseurl + "/Admin/GetSectionDropdownData",
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.result.length > 0) {
                bindDropDown("ddlSection", response.result, "--Select--")
            }
            
        }, error: function (result) {
            alert("Something went wrong");
        }
    });
}