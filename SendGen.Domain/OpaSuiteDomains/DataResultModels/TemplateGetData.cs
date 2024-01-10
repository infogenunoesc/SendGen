namespace SendGen.Domain.OpaSuiteDomains.DataResultModels
{
    // Objeto template da API do Opa Suite
    public class TemplateGetData
    {
        public string _id { get; set; }
        public string texto { get; set; }
        public string atalho { get; set; }
        public string tipo_mensagem { get; set; }
        public List<string> departamentos { get; set; }
    }
}