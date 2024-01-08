// Função chamada ao clicar no botão "Enviar"
function Enviar_Click() {
    // Utiliza jQuery para fazer uma requisição POST para a URL "/Template/EnviarMensagem"
    // Envia os parâmetros clienteIds e templateid no corpo da requisição
    $.post("/Template/EnviarMensagem", {
        clienteIds: clienteIds.value,
        templateid: templateid.value
    }, function (ret) {
        // Função de callback que é executada quando a requisição é concluída

        // Exibe um alerta com a mensagem retornada pela requisição
        alert(ret.mensagem);

        // Redireciona o usuário para a URL "/Clientes"
        window.location.href = "/Clientes";
    });
}
