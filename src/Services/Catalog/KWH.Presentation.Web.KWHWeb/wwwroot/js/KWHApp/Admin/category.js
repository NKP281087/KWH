
$(document).ready(function () {

    BindCategoryData();

    $(document).on("click", "#btnSubmit", function () {

        if ($("#txtCategoryName").val() == "") {
            $("#txtCategoryName").focus();
            return false
        }
        var data =
        {
            CategoryName: $("#txtCategoryName").val()
        }

        $.ajax({
            type: "POST",
            url: baseurl + "/Admin/SubmitCategoryData",
            data: data,
            dataType: "json",
            success: function (result) {
                var response = JSON.parse(result);
                if (response.statusCode == 200 && response.message == "Success") {
                    alert("Data Added Successfully!");
                    $('#categoryModal').modal('hide');
                    BindCategoryData();
                }
                else {
                    alert(response.message);
                }
            }
        });

    });

    $(document).on("click", "#btnUpdate", function () {

        if ($("#txtEditCategoryName").val() == "") {
            $("#txtEditCategoryName").focus();
            return false
        }
        var data =
        {
            CategoryName: $("#txtEditCategoryName").val(),
            CategoryId: String($("#hdnCategoryId").val()),
        }

        $.ajax({
            type: "POST",
            url: baseurl + "/Admin/UpdateCategoryData",
            data: data,
            dataType: "json",
            success: function (result) {
                var response = JSON.parse(result);
                if (response.statusCode == 200 && response.message == "Success") {
                    alert("Data Updated Successfully!");
                    $('#EditCategoryModal').modal('hide');
                    BindCategoryData();
                }
                else {
                    alert(response.message);
                }
            }
        });

    });

    $(document).on("click", "#btnCategoryDelete", function () {
        var Id = $(this).data('id');
        DeleteCategory($(this).data('id'))
    })

    $(document).on('click', '#EditCategoryData', function () {
        var Id = $(this).data('id');
        GetCategoryDataById(Id);
    })

    $(document).on("click", "#btnAdd", function () {
        $("#txtCategoryName").val("");
        $('#categoryModal').modal('show');
    });
    $(document).on("click", "#btnClose", function () {
        $("#txtCategoryName").val("");
        $('#categoryModal').modal('hide');
    });
    $(document).on("click", "#btnEditClose", function () {
        $("#txtEditCategoryName").val("");
        $('#EditCategoryModal').modal('hide');
    });

});


function BindCategoryData() {
    $("#tblCategory").empty();
    $.ajax({

        type: "GET",
        url: baseurl + "/Admin/GetAllCategoryData",
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.result.length > 0) {
                var totalCount = response.result.length;
                var categoryData = response.result;
                var row = "";
                for (var i = 0; i < totalCount; i++) {
                    row += "<tr>";
                    row += "<td>" + categoryData[i].categoryName + "</td>"
                    row += "<td>";
                    row += "<a style='padding-right:12px; cursor:pointer;'>";
                    row += "<img id='EditCategoryData' data-id='" + categoryData[i].categoryId + "' src='../Content/images/icons/admin-edit.svg'>";
                    row += "</a>";
                    row += "<a style='cursor:pointer;'>";
                    row += "<img data-id='" + categoryData[i].categoryId + "' id='btnCategoryDelete' src='../Content/images/icons/delete.svg'>";
                    row += "</a>";
                    row += "</td>";
                    row += "</tr>";
                }
                $("#tblCategory").append(row);
            }
            else {
                alert("No Record Found");
            }
        }, error: function (result) {
            alert("Something went wrong");
        }
    })
}
function DeleteCategory(Id) {
    var data = {
        CategoryId: String(Id)
    }

    $.ajax({
        type: "POST",
        url: baseurl + "/Admin/DeleteCategoryData",
        data: data,
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200) {
                alert("Data Deleted Successfull");
                BindCategoryData();
            }
        },
        error: function (result) {
            alert("Something went wrong");
        }
    })
}

function GetCategoryDataById(Id) {

    $.ajax({
        type: "GET",
        url: baseurl + "/Admin/GetCategoryById",
        data: { Id: String(Id) },
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200) {
                var categoryData = response.result;
                $("#hdnCategoryId").val(Id);
                $("#txtEditCategoryName").val(categoryData.categoryName);
                $('#EditCategoryModal').modal('show');
            }
        }
    })
}