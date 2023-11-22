namespace SendGen.Domain.OpaSuiteDomains;

public class filterTemplate
{
    public string? atalho { get; set; }
    public string? tipo_mensagem { get; set; }
}

public class optionsTemplate
{
    public int? skip { get; set; }
    public int? limit { get; set; }

}

public class templateGetFilter
{
    public filterTemplate? filter { get; set; }
    public optionsTemplate? options { get; set; }
}