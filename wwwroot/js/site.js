$(document).ready(function () {
    // Prevent non-letter input in 'state' and 'city' fields
    $('#state, #city').on('keypress', function (event) {
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!/^[A-Za-z\s]+$/.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    // Prevent non-digit input in 'zipcode' field
    $('#zipcode').on('keypress', function (event) {
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!/^\d+$/.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $('form').validate({
        rules: {
            state: {
                required: true,
                lettersonly: true
            },
            city: {
                required: true,
                lettersonly: true
            },
            zipcode: {
                required: true,
                digits: true
            },
            date: {
                required: true,
                date: true
            },
        },
    });
});










