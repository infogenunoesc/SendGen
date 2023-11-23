// Script para renderizar os dados recebidos de form após enviado
// Uso recomendado: <div id=resultadoForm></div>
    $(document).ready(function () {
        $('form').submit(function (e) {
            e.preventDefault();

            $.ajax({
                url: $(this).attr('action'),
                method: $(this).attr('method'),
                data: $(this).serialize(),
                success: function (response) {
                    $('#resultadoForm').html(response);
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                    $('#resultadoForm').html("Ocorreu um erro ao processar a solicitação.");
                }
            });
        });
        });
