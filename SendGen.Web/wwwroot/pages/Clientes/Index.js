function enviarParaTodosSelecionados_Click() {
    window.location.href = "/Template/Selecionar?clienteIds=" + Array.from($("input[name=clienteid]:checked").map((c, d) => d.value)).join(",");
}