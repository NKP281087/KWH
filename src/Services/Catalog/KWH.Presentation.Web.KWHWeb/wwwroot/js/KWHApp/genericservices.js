//this script file contains generic functions that cab be used for binding various controls

var bindDropDown = function (elementId, json, firstItemText) {
    //clean dropdown
    $("#" + elementId).empty();

    $("<option />", {
        val: "0",
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })
}

//this function can be used to dropdown to an element
var bindDropDown = function (elementId, json, firstItemText) {
    //clean dropdown
    $("#" + elementId).empty();

    $("<option />", {
        val: "0",
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })
}
//this function can be used to fill a dropdown with values and select a option value
var bindDropDownSelected = function (elementId, json, firstItemText, selectedValue) {
    //clean dropdown
    $("#" + elementId).empty();

    $("<option />", {
        val: "0",
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })

    //select a value
    $("#" + elementId).val(selectedValue);
}

//this function can be used to dropdown to an element & selected value is string type
var bindDropDownString = function (elementId, json, firstItemText) {
    //clean dropdown
    $("#" + elementId).empty();

    $("<option />", {
        val: null,
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })
}

var urlEncodeToJson = function (vale) {
    return JSON.parse('{"' + decodeURI(vale).replace(/"/g, '\\"').replace(/&/g, '","').replace(/=/g, '":"') + '"}')
}

var getSliderHtml = function (isChecked, data) {
    var onOrOff = isChecked ? "Checked" : "";

    return '<label class="switch"><input id="slider" type="checkbox" data-Id=' + data + ' data-CompanyId=' + data + ' ' + onOrOff + '><div class="slider round"></div></label>';
}

var getPencilHtml = function (href) {
    return '<a href="' + href + '"><i class="fa fa-pencil" aria-hidden="true"></i></a>';
}

//this function can be used to dropdown to an element when id is in string form
var bindDropDownForStringValue = function (elementId, json, firstItemText) {
    //clean dropdown
    $("#" + elementId).empty();

    $("<option />", {
        val: "",
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })
}
//this function can be used to dropdown to an element when id is in string form
var bindDropDownForStringValuewithSelected = function (elementId, json, firstItemText, selectedValue) {
    //clean dropdown
    $("#" + elementId).empty();

    $("<option />", {
        val: "",
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })
    //select a value
    $("#" + elementId).val(selectedValue);
}

//this function can be used to dropdown to an element when id is in string form
var bindDropDownForStringTextwithSelected = function (elementId, json, firstItemText, selectedText) {
    //clean dropdown
    $("#" + elementId).empty();

    //$("<option />", {
    //    val: "",
    //    text: firstItemText
    //}).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("#" + elementId);
    })
    //select a value
    $("#" + elementId + " option:contains(" + selectedText + ")").attr('selected', 'selected');
}

//// Validation rule to check space in text
//jQuery.validator.addMethod("noSpace", function (value, element) {
//    return /^\s.*/.test(value) == 0;
//}, "Initial space is not allowed");

function blockSpecialChar(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
}
//pass text message to show success message on top
//function successMessage(text, callback) {
//    $('.loading').hide();
//    $('#successtext').html(text);
//    $('.success-wrap').removeClass('error').fadeIn();
//    $('.success-wrap').fadeOut(7000);

//    if (callback) {
//        callback();
//    }
//}

////pass text message to show error on top
//function errorMessage(text) {
//    $('.loading').hide();
//    $('#successtext').html(text);
//    $('.success-wrap').addClass('error').fadeIn();
//    $('.success-wrap').fadeOut(7000);
//}

//pass text message to show success message on top
function successMessage(text) {
    $('.loading').hide();
    $('#successtext').html(text);
    $('.messagesAlert').addClass('active');
    $('.hidealert').hide();
    window.setTimeout(function () {
        $('.messagesAlert').removeClass('active');

    }, 7000);
}

//pass text message to show error on top
function errorMessage(text) {
    $('.loading').hide();
    $('#errortext').html(text);
    $('.messagesAlert').addClass('active');
    window.setTimeout(function () {
        $('.messagesAlert').removeClass('active');

    }, 3000);
}
//pass direct json and will return parsed output if status code is 200 else log console
function checkStatus(jsonIn) {
    if (JSON.parse(jsonIn).StatusCode == 200) {
        return JSON.parse(jsonIn)
    }
    else errorConsole(jsonIn);
}

function errorConsole(jsonin) {
    try {
        authErrorHandler(jsonin);
        console.log(JSON.parse(jsonin).statusText);
        $(".loading").hide();
        errorMessage("Some error Occured.Please try in sometime.");
    } catch (e) {
        $(".loading").hide();
        console.log(e.statusText);
        errorMessage("Some error Occured.Please try in sometime.");
    }

}

//this function is used to handle error that are caused due to various reasons like authorization or forbidden and other
function authErrorHandler(errorStatus) {
    var response = errorStatus;
    if (response.status = 403) {
        window.location = baseurl + "/Account/Login?reason=singleLogin"
    }
    else if (response.status = 401) {
        window.location = baseurl + "/Account/Login?reason=sessionExpired"
    }
}

$(function () {
    $(document).on("keypress keyup blur", ".allownumericwithdecimal", function (event) {
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if (event.which != 8) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        }
    });

    //$(document).on("paste", ".allownumericwithdecimal", function (e) {
    //    e.preventDefault();

    //});
    $(document).on("drop", ".allownumericwithdecimal", function (e) {
        e.preventDefault();
    });

    $(document).on("keypress keyup blur", ".allownumericwithoutdecimal", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
    //$(document).on("paste", ".allownumericwithoutdecimal", function (e) {
    //    e.preventDefault();
    //});
    $(document).on("drop", ".allownumericwithoutdecimal", function (e) {
        e.preventDefault();
    });

    $(".alphabets").keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (!regex.test(str)) {
            e.preventDefault();
        }
    });
    //copy paste disable
    //$('.alphabets').bind("cut copy paste", function (e) {
    //    e.preventDefault();
    //});

    $('.alphanumeric').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z0-9-., ]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        e.preventDefault();
        return false;
    });

    //copy paste disable
    //$('.alphanumeric').bind("cut copy paste", function (e) {
    //    e.preventDefault();
    //});

    //disable cursor point
    jQuery(document).ready(function ($) {
        //allwo only 2 digits after of decimal

        var max = 6;
        $('.maximum').keypress(function (e) {
            if (e.which < 0x20) {
                // e.which < 0x20, then it's not a printable character
                // e.which === 0 - Not a character
                return;     // Do nothing
            }
            if (this.value.length == max) {
                e.preventDefault();
            } else if (this.value.length > max) {
                // Maximum exceeded
                this.value = this.value.substring(0, max);
            }
        });

        //allow only 2 digits after decimal
        $('.number').keypress(function (event) {
            var $this = $(this);
            if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
                ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }

            var text = $(this).val();
            if ((event.which == 46) && (text.indexOf('.') == -1)) {
                setTimeout(function () {
                    if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                        $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                    }
                }, 1);
            }

            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > 2) &&
                (event.which != 0 && event.which != 8) &&
                ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });

        $('.number').bind("paste", function (e) {
            var text = e.originalEvent.clipboardData.getData('Text');
            if ($.isNumeric(text)) {
                if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
                    e.preventDefault();
                    $(this).val(text.substring(0, text.indexOf('.') + 3));
                }
            }
            else {
                e.preventDefault();
            }
        });

        //Email Validation
        $(".email").keypress(function (e) {
            var regex = new RegExp("^[a-zA-Z0-9_.@-]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(str)) {
                e.preventDefault();
            }
        });
        //copy paste disable

        //$('.email').bind("cut copy paste", function (e) {
        //    e.preventDefault();
        //});

    }); //end if ready(fn
    //allwo only 2 digits after of decimal

    var max = 11;
    $('.maximumNoOfDecimal').keypress(function (e) {
        if (e.which < 0x20) {
            // e.which < 0x20, then it's not a printable character
            // e.which === 0 - Not a character
            return;     // Do nothing
        }
        if (this.value.length == max) {
            e.preventDefault();
        } else if (this.value.length > max) {
            // Maximum exceeded
            this.value = this.value.substring(0, max);
        }
    });
    var max = 8;
    $('.maximumNoOfDecimalOfIncomming').keypress(function (e) {
        if (e.which < 0x20) {
            // e.which < 0x20, then it's not a printable character
            // e.which === 0 - Not a character
            return;     // Do nothing
        }
        if (this.value.length == max) {
            e.preventDefault();
        } else if (this.value.length > max) {
            // Maximum exceeded
            this.value = this.value.substring(0, max);
        }
    });

    
});
//for date formate
function dateformate(fetchdate) {
    var myDate = new Date(fetchdate);
    var d = myDate.getDate();
    var m = myDate.getMonth();
    m += 1;
    var y = myDate.getFullYear();
    var Fdate = ((m < 10 ? '0' + m : m) + '/' + (d < 10 ? '0' + d : d) + '/' + y)
    return Fdate;
}
//this function can be used to fill a dropdown with values and select a option value with class

var bindDropDownwithClass = function (elementId, json, firstItemText) {
    //clean dropdown
    $("." + elementId).empty();

    $("<option />", {
        val: "0",
        text: firstItemText
    }).appendTo("#" + elementId);

    //bind json to dropdown
    $.each(json, function (i, v) {
        $("<option />", {
            val: this.value,
            text: this.text
        }).appendTo("." + elementId);
    })
}

function DBdateformate(fetchdate) {
    var myDate = new Date(fetchdate);
    var d = myDate.getDate();
    var m = myDate.getMonth();
    m += 1;
    var y = myDate.getFullYear();
    var Fdate = (y + '-' + (m < 10 ? '0' + m : m) + '-' + (d < 10 ? '0' + d : d))
    return Fdate;
}
//this will work based on common parent is "row"
$(function () {
    $(".refine").on("keyup", function () {
        _this = this;
        // Show only matching TR, hide rest of them
        $.each($(_this).closest(".row").find("tbody tr"), function () {
            if ($(this).text().toLowerCase().indexOf($(_this).val().toLowerCase()) === -1)
                $(this).hide();
            else
                $(this).show();
        });
    });

    $(".checkall").change(function () {
        $(this).closest("table").find("input:checkbox").prop('checked', $(this).prop("checked"));
    });
});
//// this function is used for get value from  querystring
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

var maskBehavior = function (val) {
    val = val.split(":");
    return (parseInt(val[0]) > 19) ? "HZ:M0" : "H0:M0";
}

spOptions = {
    onKeyPress: function (val, e, field, options) {
        field.mask(maskBehavior.apply({}, arguments), options);
    },
    translation: {
        'H': { pattern: /[0-2]/, optional: false },
        'Z': { pattern: /[0-3]/, optional: false },
        'M': { pattern: /[0-5]/, optional: false }
    }
};

// validate phone no text
function checkintphone(obj) {
    var value = $(obj).val();
    if (value != '' && value != 'undefined') {
        var regex = new RegExp(/^[+]?\d+$/);
        if (!value.match(regex)) {
            value = value.substring(0, (value.length) - 1);
            $(obj).val(value);
        }

    }

}

$('.NumericWithSpace').on("keypress", function (e) {
    var regex = /^(?:[0-9 ]+$)/
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    e.preventDefault();
    return false;
});



function copytoclipboard() {
    var rows = "";
    var cols = "";
    $("table.table tr:visible").each(function () {
        cols = "";
        $(this).find("td,th").each(function () {
            cols += $.trim($(this).text()) + "\t";
            //alert(cols);
        });
        rows += cols + "\n";
    });

    var textBox = $("<textarea>");
    textBox.text(rows);
    $('body').append(textBox);
    textBox.select();
    document.execCommand('copy');
    successMessage("Copied To Clipboard");
};

function printpage() {

    var divContents = $("table")[0].outerHTML;
    var printWindow = window.open('', '', 'height=800,width=1200');
    printWindow.document.write('<html><head><title>Print</title>');
    printWindow.document.write('</head><body style="padding:30px">');
    printWindow.document.write(divContents);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
    printWindow.close();



    //var printContents = $("table.table")[0].html;
    //var originalContents = document.body.innerHTML;
    //document.body.innerHTML = printContents;
    //window.print();
    //document.body.innerHTML = originalContents;
}
function printpageIndex(index) {
    var divContents = $("table")[index].outerHTML;
    var printWindow = window.open('', '', 'height=800,width=1200');
    printWindow.document.write('<html><head><title>Print</title>');
    printWindow.document.write('</head><body style="padding:30px">');
    printWindow.document.write(divContents);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
    printWindow.close();
}


function printdiary() {
    var rows = "";
    var cols = "";
    $("table.table tr").each(function () {
        cols = "";
        $(this).find("td,th").each(function () {
            if ($(this).find(':text').length) { cols += "<td>" + $(this).find("input").val() + "</td>"; }
            else if ($(this).find(':checkbox').length) {
                debugger
                cols += "<td>" + $(this).find(":checkbox")[0].outerHTML + "</td>";
            }
            else if ($(this).text() != "Delete")
                cols += "<td>" + $(this).text() + "</td>";
            //alert(cols);
        });
        rows += "<tr>" + cols + "</tr>\n";
    });

    var divContents = rows;
    var printWindow = window.open('', '', 'height=800,width=1200');
    printWindow.document.write('<html><head><title>Diary Notes</title>');
    printWindow.document.write('</head><body style="padding:30px"><table>');
    printWindow.document.write(divContents);
    printWindow.document.write('</table></body></html>');
    printWindow.document.close();
    printWindow.print();
    printWindow.close();
}

//Funtion to restrict 'only 3 numbers' in textbox
//Works only on textbox 'id'
function AllowThreeNumberOnly(currentId, e) { //e = event

    // Allow: backspace, delete, tab
    if ($.inArray(e.keyCode, [46, 8, 9]) !== -1 ||
        (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
    //Lastly allow only 3 numbers
    alert($(currentId).val().length);
    if ($(currentId).val().length >= 3) {

        $(currentId).val($(currentId).val().slice(0, 3));
        return false;
    }
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '.00';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}