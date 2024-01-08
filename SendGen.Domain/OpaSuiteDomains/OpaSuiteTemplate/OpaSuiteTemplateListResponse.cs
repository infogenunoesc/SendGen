namespace SendGen.Domain.OpaSuiteDomains.OpaSuiteTemplate
{

	public class OpaSuiteTemplateListResponse
	{
		public string status { get; set; }
		public int code { get; set; }
		public List<OpaSuiteTemplate> data { get; set; }
	}
	public class OpaSuiteTemplate
	{
		public string _id { get; set; }
		public string texto { get; set; }
		public string atalho { get; set; }
		public string tipo_mensagem { get; set; }
		public List<string> departamentos { get; set; }
	}

}
