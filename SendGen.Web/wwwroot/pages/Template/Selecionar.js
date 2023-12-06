function Enviar_Click() {
    $.post("/Template/EnviarMensagem", {
        clienteIds: clienteIds.value,
        templateid: templateid.value
    }, function (ret) {
        alert(ret.mensagem);

        window.location.href = "/Clientes";
    });
}