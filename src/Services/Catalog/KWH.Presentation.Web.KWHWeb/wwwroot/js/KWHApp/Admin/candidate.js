$(document).ready(function () {

    $(document).on("change", "#ddlClassId", function () {

        if ($("#ddlClassId").val() != "0") {
            BindSectionDropdown($("#ddlClassId").val());
        }
    });

    $(document).on("click", "#btnSubmit", function () {
        SubmitData();
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

function SubmitData() {

    if ($("#txtClassRollNo").val() == "")
    {
        $("#txtClassRollNo").focus();
        alert("Please Enter Class Roll Number");
        return false;
    }
    if ($("#txtCandidateName").val() == "") {
        $("#txtCandidateName").focus();
        alert("Please Enter Candidate Name");
        return false;
    }
    if ($("#txtMobileNo").val() == "") {
        $("#txtMobileNo").focus();
        alert("Please Enter Mobile Number");
        return false;
    }
    if ($("#txtEmailId").val() == "") {
        $("#txtEmailId").focus();
        alert("Please Enter Email Id");
        return false;
    }
    if (!validateEmail($("#txtEmailId").val()))
    {
        $("#txtEmailId").focus();
        alert("Please Enter Valid Email Id");
        return false;
    }
    if ($("#ddlCategoryId").val() == "0") {
        $("#ddlCategoryId").focus();
        alert("Please Select Category");
        return false;
    }
    if ($("#txtICardNumber").val() == "") {
        $("#txtICardNumber").focus();
        alert("Please Enter ICardNumber");
        return false;
    }
    if ($("#txtGRNumber").val() == "") {
        $("#txtGRNumber").focus();
        alert("Please Enter GR Number");
        return false;
    }
    if ($("#txtRFId").val() == "") {
        $("#txtRFId").focus();
        alert("Please Enter RFId");
        return false;
    }
    if ($("#ddlClassId").val() == "0") {
        $("#ddlClassId").focus();
        alert("Please Select Class");
        return false;
    }
    if ($("#ddlSectionId").val() == "0") {
        $("#ddlSectionId").focus();
        alert("Please Select Section");
        return false;
    }

    var data =
    {
        ClassRollNo : $('#txtClassRollNo').val(),
        CandidateName: $('#txtCandidateName').val(),
        MobileNo     : $('#txtMobileNo').val(),
        AlternateNo  : $('#txtAlternateNo').val(),
        EmailId      : $('#txtEmailId').val(),
        CategoryId   : $('#ddlCategoryId').val(),
        ICardNumber  : $('#txtICardNumber').val(),
        GRNumber     : $('#txtGRNumber').val(),
        RFId         : $('#txtRFId').val(),
        ClassId      : $('#ddlClassId').val(),
        SectionId    : $('#ddlSectionId').val() 
    }

    $.ajax({

        type: "POST",
        url: baseurl + "/Admin/AddEditCandidateData",
        data: data,
        dataType: "json",
        success: function (result)
        {     
            var response = JSON.parse(result);
            if (response.statusCode == 200 && response.message == "Success") {
                alert("Data Added Successfully!");
                Empty();
            }
            else {
                alert(response.message);
            }

        }, error: function (result) {
            alert("Something went wrong");
        }

    })
}

function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}

function Empty()
{
    $('#txtClassRollNo').val("");
    $('#txtCandidateName').val("");
    $('#txtMobileNo').val("");
    $('#txtAlternateNo').val("");
    $('#txtEmailId').val("");
    $('#ddlCategoryId').val("");
    $('#txtICardNumber').val("");
    $('#txtGRNumber').val("");
    $('#txtRFId').val("");
    $('#ddlClassId').val("");
    $('#ddlSectionId').val("");

}