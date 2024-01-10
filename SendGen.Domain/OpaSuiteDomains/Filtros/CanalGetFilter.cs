namespace SendGen.Domain.OpaSuiteDomains.Filtros;

//Classe com os parametros do filtro de busca dos Canais de Comunicação da API do Opa Suite
public class filterCanais
{
    public string? nome { get; set; }
    public string? id_atendente { get; set; }
    public string? status { get; set; }
    public string? canal { get; set; }
    public string? integracao { get; set; }
}

public class canalGetFilter
{
    public filterCanais? filter { get; set; }
    public options? options { get; set; }
}