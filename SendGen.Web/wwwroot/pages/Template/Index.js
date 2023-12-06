function Enviar_Click() {
    $.post("/Template/EnviarMensagem", {
        clienteId: clienteId.value,
        templateid: templateid.value
    }, function (ret) {
        alert(ret.mensagem);

        window.location.href = "/Clientes";
    });
}