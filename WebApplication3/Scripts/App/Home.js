$().ready(function () {

});


$('#gerarToken').click(function () {

    $.get("http://localhost:23742/Home/GenerateToken", function (data) {
        $('#tokenValue').text(data.Token);
        $('#time').text('9');
        $('#unit').text('segundos');
        var interval = setInterval(function () {
            $('#time').text(parseInt($('#time').text()) - 1);
            if ($('#time').text() == 0) {
                $('#tokenValue').text("XXXXX-XXXXX-XXXXX-XXXX");
                $('#time').text('1');
                $('#unit').text('minuto');
                clearInterval(interval);
            }
        }, 1000);
    });
});