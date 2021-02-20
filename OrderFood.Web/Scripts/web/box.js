
$(document).ready(function myfunction() {
    $(".aa-add-card-btn").click(function myfunction() {
        var uid = $(this).attr("name");
        $.ajax({
            type: "GET",
            url: "/Product/AddtoCard/",
            dataType: 'JSON',
            data: { id: uid },
            success: function () {
                alertify.error('El producto no se pudo agregar al carrito.');
            },
            error: function () {
                alertify.success('Producto añadido al carrito');
            },
        });
    });
});