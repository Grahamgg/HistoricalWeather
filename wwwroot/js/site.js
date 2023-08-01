
$(document).ready(function () {
    // Prevent non-letter input in 'city' field
    $('#city').on('keypress', function (event) {
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!/^[A-Za-z\s]+$/.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $("#date").on("change", function () {
        var selectedDate = new Date($(this).val());
        var now = new Date();

        selectedDate.setHours(0, 0, 0, 0);
        now.setHours(0, 0, 0, 0);

        if (selectedDate > now) {
            alert("Please select a date in the past");
            $(this).val('');
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
                required: true
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











