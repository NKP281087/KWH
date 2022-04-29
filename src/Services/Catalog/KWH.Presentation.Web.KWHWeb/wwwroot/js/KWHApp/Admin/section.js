$(document).ready(function () {
 

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
                successMessage('Data Added Successfully!');
               // window.setTimeout(function () { window.location.href = baseurl + '/Admin/GetAllSectionData' }, 2000);
            }
        });

    });

    //$(document).on("click", "#", function () {

    //})


    $(document).on("click", "#btnAdd",function () {
         
        $('#sectionModal').modal('show');
    });
    $(document).on("click", "#btnClose", function () {
        $('#sectionModal').modal('hide');
    }); 
   
});
