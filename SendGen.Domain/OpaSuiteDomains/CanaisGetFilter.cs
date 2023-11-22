namespace SendGen.Domain.OpaSuiteDomains;

public class filterCanais
{
    public string? nome { get; set; }
    public string? id_atendente { get; set; }
    public string? status { get; set; }
    public string? canal { get; set; }
    public string? integracao { get; set; }
}

public class optionsCanais
{
    public int? skip { get; set; }
    public int? limit { get; set; }

}

public class canaisGetFilter
{
    public filterCanais? filter { get; set; }
    public optionsCanais? options { get; set; }
}