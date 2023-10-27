$(document).ready(function () {


    $("#createItemModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                
            },
            error: function () {
                
            }
        })
    });
});