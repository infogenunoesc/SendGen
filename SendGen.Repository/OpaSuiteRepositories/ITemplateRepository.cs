using SendGen.Domain.OpaSuiteDomains.OpaSuiteTemplate;

namespace SendGen.Repository.OpaSuiteRepositories
{
	public interface ITemplateRepository
	{
		Task<OpaSuiteTemplateListResponse> Get();
		Task Send(string telefoneCliente, string nome, string templateid);
	}
}