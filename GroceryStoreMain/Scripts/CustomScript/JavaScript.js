$(document).ready(function () {

    $('#AddToCart').click(function (e) {
        // $this = $(e.target);
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/Customer/AddToCart',
            data: { id: $('#productID').val() },
            success: function (response) {
                if (response != null) {
                    alert("Product added" + response.name);
                } else {
                    alert("Something went wrong");
                }
            }
        });
    });


    $('#customerLogin').change(function () {
        if ($(this).val().length == 10) {
            $('input[type="submit"]').removeAttr('disabled');
        }
    });

    $('#CustomerLogin').click(function (e) {
        // $this = $(e.target);
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/Customer/LoginCustomer',
            data: { PhoneNumber: $('#customerLogin').val() },
            success: function (response) {
                if (response != null) {
                    alert("Product added" + response.name);
                } else {
                    alert("Something went wrong");
                }
            }
        });
    });
})

