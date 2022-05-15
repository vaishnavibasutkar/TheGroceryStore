$(document).ready(function () {

    $('#AddToCart').click(function (e) {
        // $this = $(e.target);
        if ($('#sessionUser').val() == null) {

            $('#loginLink').click()
        }
        else {

            //$.ajax({
            //    type: 'POST',
            //    dataType: 'json',
            //    url: '/Customer/AddToCart',
            //    data: { id: $('#productID').val() },
            //    success: function (response) {
            //        if (response != null) {
            //            alert("Product added" + response.name);
            //        } else {
            //            alert("Please Try adding Product Again....");
            //        }
            //    }
            //});
        }
    });

    $('#AddNewCart').click(function (e) {
        // $this = $(e.target);
        var as = $('#sessionUser').val();

        if ($('#sessionUser').val().length == 0) {


            $('#loginLink').click()
        }
        else {

            $.ajax({
                type: 'post',
                dataType: 'json',
                url: '/Customer/AddNewCart',
                data: {},
                success: function (response) {
                }

            });
            location.reload();
        }
    });


    $('#customerLogin').change(function () {
        var a = document.getElementById('customerLogin').value
        if (a.length == 10) {
            $('input[type="submit"]').removeAttr('disabled');
        }
        else {
            $('input[type="submit"]').prop('disabled', true
            );
        }
    });

    $('#CustomerLogin').click(function (e) {
        CustomerLoginFunction();
    });


    $('#CustomerLoginEmail').click(function (e) {
        CustomerLoginEmailFunction();

    });
    $('#modelcontent').on('click', '#validateOTP', function () {
        if ($('#otptext').val().length == 6) {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: '/Customer/ValidateOTP',
                data: { otptext: $('#otptext').val() },
                success: function (response) {
                    if (response != null) {

                        if (response.success == true) {
                            $('#closeloginmodel').click();
                            $('#menulogin').html('  <a href="" class="nav-item nav-link">' + response.Name + '</a>' +
                                '<a href = @Url.Action("Logout", "Customer") class= "nav-item nav-link" > Logout</a >');
                            $('#sessionUser').val(response.Name);
                        }
                        else {
                            $("#modelbody").append('<br>' + response.message);
                        }
                        alert("Product added" + response.name);
                    } else {
                        alert("Something went wrong");
                    }
                }
            });
        }
        else {
            $("#modelbody").append('<br>Enter Valid OTP ');
        }
    });


    $('#modelcontent').on('click', '#LoginUsingEmail', function () {

        $("#modelbody").html('<div class="md-form mb-6 text-lg-center">Enter your Email ID to Login / Sign up <br />' +
            //' <i class="fas fa-mail prefix grey-text"></i>' +
            '&#xe0be;' +
            '<input type="text" id="customerLoginEmail" class="form-control-sm "></div>');
        $('#modelfooter').html('<button class="btn btn-deep-orange" id="CustomerLoginEmail" onclick="CustomerLoginEmailFunction()" >Sign up</button>' +
            '<br /><a id="LoginUsingPhone" class="nav-item nav-link">Login Using Phones ID</a>')



    });
    $('#modelcontent').on('click', '#LoginUsingPhone', function () {

        $("#modelbody").html('<div class="md-form mb-6 text-lg-center">Enter your phone number to Login / Sign up <br />' +
            ' <i class="fas fa-mobile-alt prefix grey-text"></i>' +
            '<input type="text" id="customerLogin" class="form-control-sm "></div>');
        $('#modelfooter').html('<button class="btn btn-deep-orange" id="CustomerLogin" onclick="CustomerLoginFunction()" >Sign up</button>' +
            '<br /><a id="LoginUsingEmail" class="nav-item nav-link">Login Using Email ID</a>')



    });


})




function CustomerLoginFunction() {
    var a = document.getElementById('customerLogin').value;
    if (a.length == 10) {
        // $this = $(e.target);
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/Customer/LoginCustomer',
            data: { PhoneNumber: $('#customerLogin').val() },
            success: function (response) {
                if (response != null) {
                    $("#modelbody").html('<h6>Please enter the one time password <br> to verify your account</h6> <div> <span>A code has been sent to</span> ' +
                        '  <small>' + response.phoneno + '</small > </div > <div id="otp" class="inputs d-flex flex-row justify-content-center mt-2">' +
                        ' <input class="m-4 text-center form-control rounded" type="text" id="otptext" maxlength="6" />' +
                        ' </div> ');
                    $('#modelfooter').html('<button class="btn validate" id="validateOTP" >Validate</button>');
                    alert("Product added" + response.name);
                } else {
                    alert("Something went wrong");
                }
            }
        });
    }
}

function CustomerLoginEmailFunction() {
    var a = document.getElementById('customerLoginEmail').value;

    // $this = $(e.target);
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/Customer/LoginCustomerEmail',
        data: { EmailId: $('#customerLoginEmail').val() },
        success: function (response) {
            if (response != null) {
                $("#modelbody").html('<h6>Please enter the one time password <br> to verify your account</h6> <div> <span>A code has been sent to</span> ' +
                    '  <small>' + response.emailId + '</small > </div > <div id="otp" class="inputs d-flex flex-row justify-content-center mt-2">' +
                    ' <input class="m-4 text-center form-control rounded" type="text" id="otptext" maxlength="6" />' +
                    ' </div> ');
                $('#modelfooter').html('<button class="btn validate" id="validateOTP" >Validate</button>');
                alert("Product added" + response.name);
            } else {
                alert("Something went wrong");
            }
        }
    });

}

function AddToCart(item) {
    var cart_id = $(item).attr("id");
    var product_id = $('#productID').val();

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: '/Customer/AddToCartNew',
        data: { IDs: [cart_id, product_id] },
        success: function (response) {
            if (response.Success) {
                $('#cart').children().each(function (i, l) {
                    if ($(this).attr("id") == cart_id) {

                        $(this).text($(item).text() + "(" + response.cartTCount + ")");
                    }
                })
                /* $(item).text("Cart(" + response.Counter + ")");*/
                $('#blankcart').prop("hidden", "hidden")
            }
        }

    });
}
function AddToCartPBPC(item) {
    var cart_id = $(item).attr("id");
    var p_id = $(item).attr("pid");

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: '/Customer/AddToCartNew',
        data: { IDs: [cart_id, p_id] },
        success: function (response) {
            if (response.Success) {
                $('#cart').children().each(function (i, l) {
                    if ($(this).attr("id") == cart_id) {

                        $(this).text($(item).text() + "(" + response.cartTCount + ")");
                    }
                })
                /* $(item).text("Cart(" + response.Counter + ")");*/
                $('#blankcart').prop("hidden", "hidden")
            }
        }

    });
}
function RemoveProductFromCart(item) {
    
    $(item).parent().parent().hide()
    var cart_id = $(item).attr("cartid");
    var p_id = $(item).attr("id");
    alert(cart_id)
    alert(p_id)

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: '/Customer/RemoveFromCart',
        data: { IDs: [cart_id, p_id] },
        success: function (response) {
            if (response.Success) {
                $('#cart').children().each(function (i, l) {
                    if ($(this).attr("id") == cart_id) {

                        $(this).text($(item).attr("cartname") + "(" + response.cartTCount + ")");
                    }
                })
                /* $(item).text("Cart(" + response.Counter + ")");*/
                $('#blankcart').prop("hidden", "hidden")
            }
        }

    });
    
}

