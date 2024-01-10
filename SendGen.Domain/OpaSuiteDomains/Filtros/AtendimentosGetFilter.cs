namespace SendGen.Domain.OpaSuiteDomains.Filtros;

//Classe com os parametros do filtro da busca de Atendimentos da API do Opa Suite

public class filterAtendimentos
{
    public string? protocolo { get; set; }
    public string? dataInicialAbertura { get; set; }
    public string? dataFinalAbertura { get; set; }
    public string? dataInicialEncerramento { get; set; }
    public string? dataFinalEncerramento { get; set; }
}

public class atendimentosGetFilter
{
    public filterAtendimentos? filter { get; set; }
    public options? options { get; set; }
}

public class filterMensagensAtendimento
{
    public string? id_rota { get; set; }
}
public class mensagensAtendimentoGetFilter
{
    public filterMensagensAtendimento? filter { get; set; }
    public options? options { get; set; }
}