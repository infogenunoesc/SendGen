using SendGen.Domain.SendGenDomains.Data;

namespace SendGen.Repository.SendGenRepositories
{
    public class ClienteRepository : IClienteRepository
    {
		// Membro privado para armazenar a instância do contexto do banco de dados
		private readonly SendGenContexto sendGenContexto;

		// Construtor que recebe o contexto como dependência
		public ClienteRepository(SendGenContexto sendGenContexto)
        {
            this.sendGenContexto = sendGenContexto;
        }

		// Método para obter todos os clientes do contexto
		public List<Cliente> ObterClientes()
        {
			// Utiliza LINQ para obter todos os clientes e converte para uma lista
			return sendGenContexto.Cliente.ToList();
        }

		// Método para inserir uma lista de clientes no contexto e salvar as alterações
		public void Inserir(List<Cliente> clientes)
        {
            sendGenContexto.AddRange(clientes);
            sendGenContexto.SaveChanges();
        }
    }
}
