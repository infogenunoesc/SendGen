namespace SendGen.Repository.OpaSuiteRepositories
{
    public interface ITemplateRepository
    {
        Task Send(string telefoneCliente);
    }
}