namespace SendGen.Domain.SendGenDomains.Data
{
    //classe fiel ao banco de dados SendGen
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }
        public string? Celular { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int TraCod { get; set; }
	}
}
