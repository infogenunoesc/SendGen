using SendGen.Domain.SendGenDomains.Data;

namespace SendGen.Repository.SendGenRepositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SendGenContexto sendGenContexto;

        public ClienteRepository(SendGenContexto sendGenContexto)
        {
            this.sendGenContexto = sendGenContexto;
        }

        public List<Cliente> ObterClientes()
        {
            return sendGenContexto.Cliente.ToList();
        }

        public void Inserir(List<Cliente> clientes)
        {
            sendGenContexto.AddRange(clientes);
            sendGenContexto.SaveChanges();
        }
    }
}
