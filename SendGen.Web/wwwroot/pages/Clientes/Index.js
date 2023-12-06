// Função chamada ao clicar no botão "enviarParaTodosSelecionados"
function enviarParaTodosSelecionados_Click() {
    // Redireciona o usuário para a URL "/Template/Selecionar" com parâmetro "clienteIds"

    // Utiliza o objeto window.location.href para redirecionar o navegador
    window.location.href = "/Template/Selecionar?clienteIds=" +
        // Obtém uma lista de IDs dos clientes selecionados
        // Utiliza jQuery para selecionar os inputs do tipo checkbox com o nome "clienteid" que estão marcados (checked)
        Array.from($("input[name=clienteid]:checked")
            // Mapeia os elementos checkbox (IDs de clientes)
            .map((c, d) => d.value))
            // Junta os IDs em uma string, separados por vírgula
            .join(",");
}
