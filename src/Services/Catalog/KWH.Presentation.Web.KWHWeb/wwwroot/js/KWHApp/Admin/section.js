
$(document).ready(function () {

    BindSectionData();

    $(document).on("click", "#btnSubmit", function () {

        if ($("#txtSectionName").val() == "") {
            $("#txtSectionName").focus();
            return false
        }
        var data =
        {
            SectionName: $("#txtSectionName").val()
        }

        $.ajax({
            type: "POST",
            url: baseurl + "/Admin/SaveSectionData",
            data: data,
            dataType: "json",
            success: function (result) {
                var response = JSON.parse(result);
                if (response.statusCode == 200 && response.message == "Success") 
                {
                    alert("Data Added Successfully!");
                    $('#sectionModal').modal('hide');
                    BindSectionData();
                }
                else
                {
                    alert(response.message);
                }
            }
        });

    });

    $(document).on("click", "#btnUpdate", function () {

        if ($("#txtEditSectionName").val() == "") {
            $("#txtEditSectionName").focus();
            return false
        }
        var data =
        {
            SectionName: $("#txtEditSectionName").val(),
            SectionId: String($("#hdnSectionId").val()),
        }

        $.ajax({
            type: "POST",
            url: baseurl + "/Admin/UpdateSectionData",
            data: data,
            dataType: "json",
            success: function (result) {
                var response = JSON.parse(result);
                if (response.statusCode == 200 && response.message == "Success") {
                    alert("Data Updated Successfully!");
                    $('#EditSectionModal').modal('hide');
                    BindSectionData();
                }
                else
                {
                    alert(response.message);
                }
            }
        });

    });

    $(document).on("click", "#btnSectionDelete", function () {
        var Id = $(this).data('id');
        DeleteSection($(this).data('id'))
    })

    $(document).on('click', '#EditSectionData', function () {
        var Id = $(this).data('id');
        GetSectionDataById(Id);
    })

    $(document).on("click", "#btnAdd", function () {
        $("#txtSectionName").val("");
        $('#sectionModal').modal('show');
    });
    $(document).on("click", "#btnClose", function () {
        $("#txtSectionName").val("");
        $('#sectionModal').modal('hide');
    });
    $(document).on("click", "#btnEditClose", function () {
        $("#txtEditSectionName").val("");
        $('#EditSectionModal').modal('hide');
    });

});


function BindSectionData() {
    $("#tblSection").empty();
    $.ajax({

        type: "GET",
        url: baseurl + "/Admin/GetAllSectionData",
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.result.length > 0) {
                var totalCount = response.result.length;
                var sectionData = response.result;
                var row = "";
                for (var i = 0; i < totalCount; i++) {
                    row += "<tr>";
                    row += "<td>" + sectionData[i].sectionName + "</td>"
                    row += "<td>";
                    row += "<a style='padding-right:12px; cursor:pointer;'>";
                    row += "<img id='EditSectionData' data-id='" + sectionData[i].sectionId + "' src='../Content/images/icons/admin-edit.svg'>";
                    row += "</a>";
                    row += "<a style='cursor:pointer;'>";
                    row += "<img data-id='" + sectionData[i].sectionId + "' id='btnSectionDelete' src='../Content/images/icons/delete.svg'>";
                    row += "</a>";
                    row += "</td>";
                    row += "</tr>";
                }
                $("#tblSection").append(row);
            }
            else {
                alert("No Record Found");
            }
        }, error: function (result) {
            alert("Something went wrong");
        }
    })
}
function DeleteSection(Id) {
    var data = {
        SectionId: String(Id)
    }
    //var sectionId = 
    //var sectionId = "abc";

    $.ajax({
        type: "POST",
        url: baseurl + "/Admin/DeleteSectionData",
        data: data,
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200) {
                alert("Data Deleted Successfull");
                BindSectionData();
            }
        },
        error: function (result) {
            alert("Something went wrong");
        }
    })
}

function GetSectionDataById(Id) {
    
    $.ajax({
        type: "GET",
        url: baseurl + "/Admin/GetSectionById",
        data: { Id: String(Id) },
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200)
            {
                var sectionData = response.result;
                $("#hdnSectionId").val(Id);
                $("#txtEditSectionName").val(sectionData.sectionName);
                $('#EditSectionModal').modal('show');
            }
        }
    })
}