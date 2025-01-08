
//Checkout
function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split('&');
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        if (decodeURIComponent(pair[0]) == variable) {
            return decodeURIComponent(pair[1]);
        }
    }
    console.log('Query variable %s not found', variable);
}

// submit
$(document).ready(function () {

    /*<![CDATA[*/
    var merchantId = "";
    var sessionId = "";
    var sessionVersion = "";
    var successIndicator = "";
    var orderId = "";
    var currency = "";
    /*]]>*/
    var resultIndicator = null;

    // This method preserves the current state of successIndicator and orderId, so they're not overwritten when we return to this page after redirect
    function beforeRedirect() {
        console.log("beforeRedirect");
        return {
            successIndicator: successIndicator,
            orderId: orderId
        };
    }

    // This method is specifically for the full payment page option. Because we leave this page and return to it, we need to preserve the
    // state of successIndicator and orderId using the beforeRedirect/afterRedirect option
    function afterRedirect(data) {
        console.log("afterRedirect", data);
        // Compare with the resultIndicator saved in the completeCallback() method
        if (resultIndicator === "CANCELED") {
            return;
        }
        else if (resultIndicator) {
            var result = (resultIndicator === data.successIndicator) ? "SUCCESS" : "ERROR";
            window.location.href = "/hostedCheckout/" + data.orderId + "/" + result;
        }
        else {
            successIndicator = data.successIndicator;
            orderId = data.orderId;
            sessionId = data.sessionId;
            sessionVersion = data.sessionVersion;
            merchantId = data.merchantId;

            window.location.href = "/hostedCheckout/" + data.orderId + "/" + data.successIndicator + "/" + data.sessionId;
        }

    }

    function errorCallback(error) {
        var message = JSON.stringify(error);
        $("#loading-bar-spinner").hide();
        var $errorAlert = $('#error-alert');
        console.log(message);
        $errorAlert.append("<p>" + message + "</p>");
        $errorAlert.show();
    }
    function cancelCallback() {
        console.log('Payment cancelled');
        resultIndicator = "CANCELED";
        // Reload the page to generate a new session ID - the old one is out of date as soon as the lightbox is invoked
        window.location.reload(true);
    }

    // This handles the response from Hosted Checkout and redirects to the appropriate endpoint
    function completeCallback(response) {
        console.log("completeCallback", response);
        // Save the resultIndicator
        resultIndicator = response;
        var result = (resultIndicator === successIndicator) ? "SUCCESS" : "ERROR";

        window.location.href = "/hostedCheckout/" + orderId + "/" + result;
    }

    
    $('#btnConfirmAuthorize').show();
    $('#divAuthorize').show();
    // authorize submit
    $("#btnConfirmAuthorize").click(function (e) {

        var originalButtonText = $('#btnConfirmAuthorize').text();

        $('#btnConfirmAuthorize').html('Loading...');
        $('#btnConfirmAuthorize').prop('disabled', true);

        e.preventDefault();
        //Serialize the form datas.
        var data = {
            Identifier: getQueryVariable('Identifier'),
            ChannelTypeId: 1,
        };

        //to get alert popup
        console.log('Submitting', data);



        // Mastercard --------- Start --------------
        $.ajax({
            url: "/Checkout/Visa",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (response) {
                console.log(response);
                if (response.statusCode == 200) {

                    // Create a form element
                    var form = document.createElement('form');
                    form.setAttribute('method', 'post'); // Assuming you want to submit via POST method
                    form.setAttribute('action', response.data.payUrl); // Set your action URL

                    // Iterate over the dictionary and create hidden input fields
                    var  parameters = response.data.parameters
                    for (var key in parameters) {
                        if (parameters.hasOwnProperty(key)) {
                            var input = document.createElement('input');
                            input.setAttribute('type', 'hidden');
                            input.setAttribute('name', key);
                            input.setAttribute('value', parameters[key]);
                            form.appendChild(input);
                        }
                    }

                    // Append the form to the document body and submit it
                    document.body.appendChild(form);
                    form.submit();


                    //$('#btnConfirmAuthorize').hide();
                    $('#divAuthorize').hide();
                    $('#btnConfirmOtp').show();
                    $('#divOtp').show();
                }
                else {
                    setTimeout(function () {
                        $('#message').toggle();
                    }, 4000);
                    $('#message').toggle();
                    $('#message').html(response.message);
                }
                console.log(response);
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function () {
                // Restore the original button text
                $('#btnConfirmAuthorize').html(originalButtonText);
                // Enable the button after the request is complete
                $('#btnConfirmAuthorize').prop('disabled', false);
                // Perform additional cleanup or final actions here
            }
        });

        return;
        // Mastercard --------- End --------------


        // Mastercard --------- Start --------------
        $.ajax({
            url: "/Checkout/MasterCard",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (response) {
                console.log(response);
                if (response.statusCode == 200) {

                    Checkout.configure({
                        session: {
                            id: response.data.sessionId
                        }
                    });



                    Checkout.showPaymentPage();
      
                    //$('#btnConfirmAuthorize').hide();
                    $('#divAuthorize').hide();
                    $('#btnConfirmOtp').show();
                    $('#divOtp').show();
                    // window.location.href = response.data.redirectUrl;
                }
                else {
                    setTimeout(function () {
                        $('#message').toggle();
                    }, 4000);
                    $('#message').toggle();
                    $('#message').html(response.message);
                }
                console.log(response);
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function () {
                // Restore the original button text
                $('#btnConfirmAuthorize').html(originalButtonText);
                // Enable the button after the request is complete
                $('#btnConfirmAuthorize').prop('disabled', false);
                // Perform additional cleanup or final actions here
            }
        });

        // Mastercard --------- End --------------

    });


    // otp submit
    $("#btnConfirmOtp").click(function (e) {

        var originalButtonText = $('#btnConfirmOtp').text();

        $('#btnConfirmOtp').html('Loading...');
        $('#btnConfirmOtp').prop('disabled', true);

        e.preventDefault();
        //Serialize the form datas.
        var data = {
            Identifier: $("#Identifier").val(),
            Otp: $("#otp").val(),
        };

        //to get alert popup
        console.log('Submitting');
        $.ajax({
            url: "https://localhost:7224/Checkout/SubmitOtp",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (response) {
                console.log(response);
                // Handle successful response.
                if (response.statusCode == 200) {
                    // $('#btnConfirmOtp').hide();
                    // $('#btnConfirmOtp').show();
                    window.location.href = response.data.redirectUrl;
                }
                else {
                    setTimeout(function () {
                        $('#message').toggle();
                    }, 4000);
                    $('#message').toggle();
                    $('#message').html(response.message);
                }
                console.log(response);
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            },
            complete: function () {
                // Restore the original button text
                $('#btnConfirmOtp').html(originalButtonText);
                // Enable the button after the request is complete
                $('#btnConfirmOtp').prop('disabled', false);
                // Perform additional cleanup or final actions here
            }
        });
    });

    // Cancel
    $("#btnCancel").click(function (e) {
        window.location.href = $("#CancelURL").val();
        //window.open('', '_self').close()
    });

});

var inputs = document.getElementsByClassName('password-input');
for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener('input', function () {
        if (this.value.length >= this.maxLength) {
            var nextElement = this.parentElement.nextElementSibling;
            var nextInput = nextElement ? nextElement.querySelector('input') : null;
            if (nextInput) {
                nextInput.focus();
            }
        }
        buttonOnOff();
    });

    inputs[i].addEventListener('keydown', function (event) {
        if (event.keyCode === 8 && this.value.length === 0) {
            var previousElement = this.parentElement.previousElementSibling;
            var previousInput = previousElement ? previousElement.querySelector('input') : null;
            if (previousInput) {
                previousInput.focus();
            }
        }
        buttonOnOff();
    });
}

//otp
var inputs = document.getElementsByClassName('otp-input');
for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener('input', function () {
        if (this.value.length >= this.maxLength) {
            var nextElement = this.parentElement.nextElementSibling;
            var nextInput = nextElement ? nextElement.querySelector('input') : null;
            if (nextInput) {
                nextInput.focus();
            }
        }
        buttonOtpOnOff();
    });

    inputs[i].addEventListener('keydown', function (event) {
        if (event.keyCode === 8 && this.value.length === 0) {
            var previousElement = this.parentElement.previousElementSibling;
            var previousInput = previousElement ? previousElement.querySelector('input') : null;
            if (previousInput) {
                previousInput.focus();
            }
        }
        buttonOtpOnOff();
    });
}

// checkbox - confirmation

var checkbox = document.getElementById('confirmCheckbox');
var button = document.getElementById('btnConfirmAuthorize');
var buttonOtp = document.getElementById('btnConfirmOtp');


//checkbox.addEventListener('change', function () {
//    buttonOnOff();
//});

function buttonOnOff() {
    if (
        $("#password1").val().trim() != ""
        && $("#password2").val().trim() != ""
        && $("#password3").val().trim() != ""
        && $("#password4").val().trim() != ""
    ) {

        button.disabled = false;
    }
    else {
        button.disabled = true;
    }
}

function buttonOtpOnOff() {
    if (
        $("#otp").val().trim() != ""
    ) {

        buttonOtp.disabled = false;
    }
    else {
        buttonOtp.disabled = true;
    }
}
