var testobject;
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

    $('#name').change(function () {
        alert('Changed!');
        var n = $('#name').val()
        $('#recipeName').text(n);
    });

    $("#upload").on('click', function () {
        $("input[type='file']").click();
    });

    $('#ProceedToCheckout').click(function () {
        //var obj = JSON.parse('@Html.Raw(objJson)');
        //var jsonData = JSON.parse(JSON.stringify({ f: {} }));
        ////add object to json
        //jsonData.f = obj;
        var t = $('#testmodel').val();
        $.ajax({
            url: '@Url.Action("ProceedToCheckout")',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify(t),
            success: function (data) {
                        ...
            alert(data);
        },
            error: function (response, xhr, data) {
            }
                }
    )
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
                        if (response.cartTCount > 0) {
                            $(this).text($(item).attr("cartname") + "(" + response.cartTCount + ")");
                        }
                        else {
                            $(this).remove();
                            $('#cartdisplay').addClass('collapse');
                            $('#nocartdisplay').removeClass('collapse');
                        }
                    }
                })
                /* $(item).text("Cart(" + response.Counter + ")");*/
                $('#blankcart').prop("hidden", "hidden")
            }
        }

    });

}
function Ordernow(item) {
    alert($(item).attr('p_id'));
    if ($('#sessionUser').val().length == 0) {


        $('#loginLink').click()
    }
    else {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: '/Customer/ProceedToCheckout',
            data: { id: $(item).attr('p_id') },
            success: function (response) {
                //if (response.Success) {
                //    //$('#cart').children().each(function (i, l) {
                //    //    if ($(this).attr("id") == cart_id) {
                //    //        if (response.cartTCount > 0) {
                //    //            $(this).text($(item).attr("cartname") + "(" + response.cartTCount + ")");
                //    //        }
                //    //        else {
                //    //            $(this).remove();
                //    //            $('#cartdisplay').addClass('collapse');
                //    //            $('#nocartdisplay').removeClass('collapse');
                //    //        }
                //    //    }
                //    //})
                //    ///* $(item).text("Cart(" + response.Counter + ")");*/
                //    //$('#blankcart').prop("hidden", "hidden")
                //}
            }

        });
    }
}
function ProceedToCheckoutFromCart(ids) {
    //alert($(ids).attr('id'));
    alert($('#cpa').val());
    var len = $('#cpa').length
    var arr = []
    var test = $('#cpa').val()
    for (var i = 0; i < len; i++) {
        arr.push($('#cpa')[i])

    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: '/Customer/ProceedToCheckoutFromCart',
        data: { IDs: arr },
        success: function (response) {
            if (response.Success) {
                $('#cart').children().each(function (i, l) {
                    if ($(this).attr("id") == cart_id) {
                        if (response.cartTCount > 0) {
                            $(this).text($(item).attr("cartname") + "(" + response.cartTCount + ")");
                        }
                        else {
                            $(this).remove();
                            $('#cartdisplay').addClass('collapse');
                            $('#nocartdisplay').removeClass('collapse');
                        }
                    }
                })
                /* $(item).text("Cart(" + response.Counter + ")");*/
                $('#blankcart').prop("hidden", "hidden")
            }
        }

    });



}

function DeliveryDateChange(item) {
    var DeliveryDate = $(item).val()
    $.ajax({
        type: 'post',
        dataType: 'json',
        url: '/Customer/GetDeliveryDate',
        data: { SelectedDeliveryDate: DeliveryDate },
        success: function (dts) {
            var deliveryTimeSlots = dts.DeliveryTimeSlots
            //var a = response.DeliveryTimeSlots;

            var ddlCustomers = $("<select />");

            //Add the Options to the DropDownList.
            $(deliveryTimeSlots).each(function () {
                var option = $("<option />");

                //Set Customer Name in Text part.
                option.html(this.dts_dtm);

                //Set CustomerId in Value part.
                option.val(this.dts_id);

                //Add the Option element to DropDownList.
                ddlCustomers.append(option);
            });

            //Reference the container DIV.
            var dvContainer = $("#dvContainer")

            //Add the DropDownList to DIV.
            var div = $("<div />");
            div.append(ddlCustomers);

            //Add the DIV to the container DIV.
            dvContainer.append(div);

            $('#deliverydtm').val(temp)
            var test = "hi"
            alert(test);
        },
        failure: function (dts) {

            var temp = JSON.parse(dts)
            for (var i = 0; i < a.length; i++) {
                alert("jh");
            }
            var test = "hi"
            alert(test);
        }


    });

}

function AddRecipeStep() {
    $.ajax({
        type: 'post',
        dataType: 'json',
        url: '/Recipe/AddNewRecipeDPartial',
        success: function (partialView) {
            $('#RecipeSteps').append(partialView);
        }
    });
    //var url = '@Url.Action("addnewrecipedetail", "recipe")';
    //var ele = $('#RecipeSteps').load(url)
    //document.getElementById("RecipeSteps").innerHTML += " <div class=card mt-2 mb-2>"+
    //    "<div id = id_card_step_0 class=card - body pr - 2 pl - 2 pr - md - 5 pl - md - 5> "+
    //     "   <div class=row > <div class=col-11>                <h4 id=id_step_0 class=handle>@*<i class=fas fa-paragraph></i>*@Step 1</h4>            </div>        </div > "+
    //     "       <div class=row > <div class=col-md-12>                <label for=id_step_18514name>Instruction</label>                <input id=id_step_18514name class=form-control>       +     </div>        </div> "+
    //      "          <div class=row pt-2><div class=col-md-12> <div class=jumbotron style=padding: 16px;>                    <div class=row><div class=col-md-12><h4>Ingredients</h4></div>                    </div>                    <div class=row>                        <div class=col-md-12 pr-0 pl-0 pr-md-2 pl-md-2 mt-2>                            <div>                                <div draggable=false class= style=> <hr class=d-md-none><!---->                                    <div class=d-flex>"+
    //       "             <div class=flex-grow-0 handle align-self-start><button type=button class=btn btn-lg shadow-none pr-0 pl-1 pr-md-2 pl-md-2><i class=fas fa-arrows-alt-v></i></button>  </div>"+
    //        "            <div class=flex-fill row style=margin-left: 4px; margin-right: 4px;>                                            <div class=col-lg-3 col-md-6 small-padding><input type=number step=any id=amount_0_0 class=form-control></div>                                            <div class=col-lg-3 col-md-6 small-padding>   @Html.DropDownList(pc_id, new SelectList(ViewBag.ProductCategory, pc_Id, Name), htmlAttributes: new { @class = form-control })</div>                                            <div class=col-lg-3 col-md-6 small-padding>  <input type=text class=form-control placeholder=product_unit /> </div>                                            <div class=col-lg-3 col-md-6 small-padding>                                                <input type=text class=form-control placeholder=amount_req /> </div>                                            </div>                                            <div class=flex-grow-0 small-padding>                                                <button type=button class=dropdown-item><i class=fa fa-trash fa-fw></i>  </button>                                            </div>"+
    //         "       </div>                                </div>                            </div>                        </div>                    </div>                </div>            </div>        </div >    </div ></div >"
}

function AllIngredientCart(item) {
    var test = $('#sessionUser').val();
    if ($('#sessionUser').val() == null || $('#sessionUser').val() == "") {

        $('#loginLink').click()
    }
    else {

        var RecipeID = $('#SeletedIngrdient').attr('RecipeID')
        var NoOfServing = $('#NOServing').text();

        $.ajax({
            type: 'post',
            dataType: 'json',
            url: '/Recipe/AllIngredientCart',
            data: { RecipeID: RecipeID, NoOfServing: NoOfServing },
            success: function (response) {
                if (response.Success) {
                   
                    $('#tempMessage').append(response.tempMessage)
                }
            }
        });
    }

}

function SelectedIngredientCart() {
    
    var NoOfServing = $('#NOServing').text();
    
    var selectedIngredient = []
    
    $('input[name="rs"]:checked').each(function () {
        selectedIngredient.push(this.id);
    });

    if ($('#sessionUser').val() == null || $('#sessionUser').val() == "") {

        $('#loginLink').click()
    }
    else {

        var RecipeID = $('#SeletedIngrdient').attr('RecipeID')
        var NoOfServing = $('#NOServing').text();

        $.ajax({
            type: 'post',
            dataType: 'json',
            url: '/Recipe/SelectedIngrdientCart',
            data: { SelectedIngredient: selectedIngredient, NoOfServing: NoOfServing },
            success: function (response) {
                if (response.Success) {

                    $('#tempMessage').append(response.tempMessage)
                }
            }
        });
    }
}

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#recipeimage').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#url").change(function () {
    readURL(this);
});