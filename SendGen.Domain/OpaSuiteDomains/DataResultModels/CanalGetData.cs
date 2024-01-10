namespace SendGen.Domain.OpaSuiteDomains.DataResultModels
{
    // Objeto canal da API do Opa Suite
    public class CanalGetData
    {
        public string _id { get; set; }
        public string nome { get; set; }
        public string id_atendente { get; set; }
        public string status { get; set; }
        public string canal { get; set; }
        public string integracao { get; set; }
        public int prioridadeListagemAtendimentos { get; set; }

    }

}