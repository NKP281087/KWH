$(document).ready(function () {


});

$(document).on('click',"#btnUpdateRF", function () {

    if ($("#TimeIn").val() == "") {
        $("#TimeIn").focus();
        alert("Please Enter Time In");
        return false;
    }
    if ($("#TimeOut").val() == "") {
        $("#TimeOut").focus();
        alert("Please Enter Time Out");
        return false;
    }

    var data =
    {
        TimeIn: $("#TimeIn").val(),
        TimeOut: $("#TimeOut").val()
    }

    $.ajax({
        type: "POST",
        url: baseurl + "/Admin/SubmitRFData",
        data: data,
        dataType: "json",
        success: function (result) {
            successMessage('Data Added Successfully!');

            window.setTimeout(function () { window.location.href = baseurl + '/Admin/RFData' }, 1000);
        }
    });


});