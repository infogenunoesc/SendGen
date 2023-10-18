using SendGen.Domain.SendGenDomains.Data;

namespace SendGen.Repository.SendGenRepositories
{
    public interface IClienteRepository
    {
        void Inserir(List<Cliente> clientes);
        List<Cliente> ObterClientes();
    }
}