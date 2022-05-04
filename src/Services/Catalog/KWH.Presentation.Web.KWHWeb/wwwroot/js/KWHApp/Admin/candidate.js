$(document).ready(function () {

     $(document).on("change", "#ddlClassId", function () {
     
        if ($("#ddlClassId").val() != "0") {
            BindSectionDropdown($("#ddlClassId").val());
        }
    })

})

function BindSectionDropdown(Id) {
    $.ajax({
        type: "GET",
        url: baseurl + "/Admin/GetSectionDropdownDataByClassId",
        data: { Id: Id },
        dataType: "json",
        success: function (result) {
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.result.length > 0) {
                bindDropDown("ddlSectionId", response.result, "--Select--");
            }

        }, error: function (result) {
            alert("Something went wrong");
        }
    });
}