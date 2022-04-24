$(document).ready(function () {

    var listItems = $(".nav-menu li");
    var valid = 0;
    $("ul.nav-menu >li.subli").each(function () {
        var product = $(this);

        if (product.find('ul').length > 0) {

            product.find("ul.custom_class > li a").each(function () {
                var url = $(this).attr("href");
                if (url == window.location.pathname) {
                    $(product).find('a').attr("class", "active");
                    valid = 1;
                    return true;
                }
                else {
                                      
                }
            });
        }
        else {
            if (product.find('a').attr("href") == window.location.pathname) {
                $(product).find('a').attr("class", "active");
                valid = 1;
            }
        }
    });
    
})


var PTSCommon = (function () {

    function createDecimalOnlyTextbox(element) {
        $(document).on('keypress', element, function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 46 || e.which > 57 || e.which == 47) || (e.which == 46 && $(this).val().indexOf('.') > -1)) {
                return false;
            }
        });

        $(document).on("paste", element, function (e) {
            e.preventDefault();
        });

        $(document).on("drop", element, function (e) {
            e.preventDefault();
        });
    }

    function createNumericOnlyTextbox(element) {
        $(document).on('keypress', element, function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        $(document).on("paste", element, function (e) {
            e.preventDefault();
        });

        $(document).on("drop", element, function (e) {
            e.preventDefault();
        });
    }

    function characterOnlyTextBox(element) {
        $(document).on('keypress', element, function (e) {
            return validateCharacterOnly(e.key);
        });

        $(document).on("paste", element, function (e) {
            e.preventDefault();
        });

        $(document).on("drop", element, function (e) {
            e.preventDefault();
        });
    }

    function validateCharacterOnly(value) {
        if (/^[a-zA-Z]*$/.test(value))
            return true;
        else
            return false;
    }

    function initializeMap(address, elementId) {
        var geocoder = new google.maps.Geocoder();
        var latlng = new google.maps.LatLng(-34.397, 150.644);

        var myOptions = {
            zoom: 15,
            center: latlng,
            mapTypeControl: true,
            mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
            navigationControl: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        var map = new google.maps.Map(document.getElementById(elementId), myOptions);
        if (geocoder) {
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {
                        map.setCenter(results[0].geometry.location);

                        var infowindow = new google.maps.InfoWindow(
                            {
                                content: '<b>' + address + '</b>',
                                size: new google.maps.Size(150, 50)
                            });

                        var marker = new google.maps.Marker({
                            position: results[0].geometry.location,
                            map: map,
                            title: address
                        });

                        google.maps.event.addListener(marker, 'click', function () {
                            infowindow.open(map, marker);
                        });
                    }
                }
            });
        }
    }

    function initializeMultipleMap(locations, elementId) {        
        var map = new google.maps.Map(document.getElementById(elementId), {
            zoom: 10,
            center: new google.maps.LatLng(-34.397, 150.644),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        var infowindow = new google.maps.InfoWindow();
        var geocoder = new google.maps.Geocoder();

        var marker, i;

        for (i = 0; i < locations.length; i++) {
            var address = locations[i];
            geocodeAddress(address);
        }

        function geocodeAddress(address) {
            geocoder.geocode({ 'address': address }, function (results, status) {               
                if (status == google.maps.GeocoderStatus.OK) {

                    map.setCenter(results[0].geometry.location);

                    var marker = new google.maps.Marker({
                        position: results[0].geometry.location,
                        map: map,
                        title: address
                    });

                    google.maps.event.addListener(marker, 'mouseover', function () {
                        infowindow.setContent(address);
                        infowindow.open(map, marker);
                    });

                    google.maps.event.addListener(marker, 'mouseout', function () {
                        infowindow.close();
                    });
                }
                else {
                  //  alert("some problem in geocode" + status);
                }
            });
        }
    }



    function b64StrtoBlob(b64Data, contentType, sliceSize) {
        contentType = contentType || '';
        sliceSize = sliceSize || 512;
        var byteCharacters = atob(b64Data);
        var byteArrays = [];
        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            var slice = byteCharacters.slice(offset, offset + sliceSize);
            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }
            var byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }
        var blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }

    //function initializeMap(address, elementId) {
    //    var geocoder = new google.maps.Geocoder();
    //    var latlng = new google.maps.LatLng(-34.397, 150.644);

    //    var myOptions = {
    //        zoom: 15,
    //        center: latlng,
    //        mapTypeControl: true,
    //        mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
    //        navigationControl: true,
    //        mapTypeId: google.maps.MapTypeId.ROADMAP
    //    };

    //    var map = new google.maps.Map(document.getElementById(elementId), myOptions);
    //    if (geocoder) {
    //        geocoder.geocode({ 'address': address }, function (results, status) {
    //            if (status == google.maps.GeocoderStatus.OK) {
    //                if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {
    //                    map.setCenter(results[0].geometry.location);

    //                    var infowindow = new google.maps.InfoWindow(
    //                        {
    //                            content: '<b>' + address + '</b>',
    //                            size: new google.maps.Size(150, 50)
    //                        });

    //                    var marker = new google.maps.Marker({
    //                        position: results[0].geometry.location,
    //                        map: map,
    //                        title: address
    //                    });

    //                    google.maps.event.addListener(marker, 'click', function () {
    //                        infowindow.open(map, marker);
    //                    });
    //                }
    //            }
    //        });
    //    }
    //}

    function getParameterByName(name, url) {
        if (!url) url = window.location.href.toLowerCase();
        name = name.replace(/[\[\]]/g, "\\$&").toLowerCase();
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " ").toLowerCase());
    }

    function hidePopup(elementId) {
        $("#" + elementId).find('.lb-container').removeClass('active');
        $("#" + elementId).find('.overlay').hide();
    }

    function showPopup(elementId) {
        $("#" + elementId).find('.lb-container').addClass('active');
        $("#" + elementId).find('.overlay').show();
    }

    function multicomErrorHandler(multicomResponse, callback) {
        var errorNode = multicomResponse.Error;
        if (errorNode === undefined || errorNode === null) {
            callback(false);
        }
        else {
            callback(true, errorNode.ErrorCode, errorNode.ErrorText);
        }
    }

    return {
        CreateDecimalOnlyTextbox: createDecimalOnlyTextbox,
        CreateNumericOnlyTextbox: createNumericOnlyTextbox,
        GetParameterByName: getParameterByName,
        HidePopup: hidePopup,
        InitializeMap: initializeMap,
        InitializeMultipleMap: initializeMultipleMap,
        ShowPopup: showPopup,
        MulticomErrorHandler: multicomErrorHandler,
        CharacterOnlyTextBox: characterOnlyTextBox,
        B64StrtoBlob: b64StrtoBlob
        //InitializeMapWithLATLAN: initializeMapWithLATLAN
    }

  
})();


$(document).ready(function () {
    $('#closeAlertBox').on('click', function () {
        $('.messagesAlert').toggleClass('active');
    })
    $('.btn-slider').on('click', function () {
        $('.btn-view').slideToggle();
    })
})
