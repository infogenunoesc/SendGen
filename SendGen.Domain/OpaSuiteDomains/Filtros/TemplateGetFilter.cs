namespace SendGen.Domain.OpaSuiteDomains.Filtros;

//Classe com os parametros do filtro da busca de uma Template da API do Opa Suite
public class filterTemplate
{
    public string? atalho { get; set; }
    public string? tipo_mensagem { get; set; }
}

public class templateGetFilter
{
    public filterTemplate? filter { get; set; }
    public options? options { get; set; }
}

