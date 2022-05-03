$(document).ready(function () {

    $('#AddToCart').click(function (e) {
        $this = $(e.target);

        $.ajax({
            type: "POST",
            dataType: 'json',
            url:'Customer/AddToCart', 
            data: { id= $('#productID').val()},
            success: function (response) {
                if (response != null) {
                    alert("Product added" + response.name );
                } else {
                    alert("Something went wrong");
                }
            }
        });
    });

})