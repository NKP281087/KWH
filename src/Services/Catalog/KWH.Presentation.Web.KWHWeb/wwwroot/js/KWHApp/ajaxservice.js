getSvcUrl = function (method) { return method };



var ajaxGetJson = function (method, jsonIn, sucessCallback, errorCallback) {

    $.ajax({
        url: getSvcUrl(method),
        type: "GET",
        data: jsonIn,
        // dataType: "json",
        //   contentType: "application/json",
        success: function (json) {
            sucessCallback(json);
        },
        error: function (json) {
            if (errorCallback == null || errorCallback == undefined) {
                errorCallback = errorConsole;
            }
            errorCallback(json);
        }
    });
}
var ajaxGetJsonWithToken = function (method, jsonIn, sucessCallback, errorCallback) {

    $.ajax({
        url: getSvcUrl(method),
        type: "GET",
        data: jsonIn,
        beforeSend: function (request) {
            request.setRequestHeader("Authority", sessionStorage.getItem("userToken"));
        },
        // dataType: "json",
        //   contentType: "application/json",
        success: function (json) {
            sucessCallback(json);
        },
        error: function (json) {

            if (errorCallback == null || errorCallback == undefined) {
                errorCallback = errorConsole;
            }
            errorCallback(json);
        }
    });
}
var ajaxPostJson = function (method, jsonIn, callback, errorCallback) {

    $.ajax({
        url: getSvcUrl(method),
        type: "POST",
        data: jsonIn,
        //   dataType: "json",
        // contentType: "application/json",
        success: function (json) {
            callback(json);
        },
        error: function (json) {
            if (errorCallback == null || errorCallback == undefined) {
                errorCallback = errorConsole;
            }
            errorCallback(json);
        }
    });
}

var ajaxPostJsonWithToken = function (method, jsonIn, callback, errorCallback) {

       $.ajax({
        url: getSvcUrl(method),
        type: "POST",
        data: jsonIn,

        beforeSend: function (request) {
            request.setRequestHeader("Authority", sessionStorage.getItem("userToken"));
        },
        success: function (json) {
            callback(json);
        },
        error: function (json) {
            if (errorCallback == null || errorCallback == undefined) {
                errorCallback = errorConsole;
            }
            errorCallback(json);
        }
    })
}