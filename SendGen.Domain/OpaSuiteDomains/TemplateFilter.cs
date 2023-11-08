namespace SendGen.Domain.OpaSuiteDomains;

public class filter
{
    public string? atalho { get; set; }
    public string? tipo_mensagem { get; set; }

}

public class options
{
    public int? skip { get; set; }
    public int? limit { get; set; }

}

public class templateFilter
{
    public filter filter { get; set; }
    public options options { get; set; }
}