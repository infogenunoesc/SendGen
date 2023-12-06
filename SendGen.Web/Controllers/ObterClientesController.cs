using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SendGen.Domain.SaborColonialDomains.Data;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.SaborColonialRepositories;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers
{
	// O atributo [Authorize] indica que somente usuários autenticados podem acessar este controlador

	[Authorize]
    public class ObterClientesController : Controller
    {
        private readonly IClienteRepository clienteRepository;

		// Construtor que realiza a injeção de dependência do repositório de clientes

		public ObterClientesController(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

		// Ação que retorna a view principal
		public IActionResult Index()
        {
            return View();
        }

		// Ação que processa a obtenção e processamento de clientes
        [HttpPost]
        public IActionResult Processar()
        {
			// Cria uma instância do repositório de transacionadores
			var transacionadoresRepository = new TransacionadoresRepository();

			// Obtém a lista de transacionadores
			List<Transacionadores> listaTransacionadores = transacionadoresRepository.ObterClientes();

			// Obtém a lista de clientes já cadastrados
			List<Cliente> clientesJaCadastrados = clienteRepository.ObterClientes().ToList();

			// Lista para armazenar novos clientes ou atualizações
			List<Cliente> clientes = new List<Cliente>();

			// Loop sobre os transacionadores
			foreach (var item in listaTransacionadores)
            {
				// Busca um cliente com base no código do transacionador
				Cliente? cliente = clientesJaCadastrados.Where(c => c.TraCod == item.TraCod).SingleOrDefault();

				// Se o cliente não existe, adiciona um novo cliente à lista
				if (cliente == null)
                {
                    clientes.Add(new Cliente
                    {
                        TraCod = item.TraCod,
                        Celular = item.TraCelular,
                        DataNascimento = item.TraDatNasc,
                        Nome = item.TraNom
                    });
                }
                else
                {
					// Se o cliente já existe, atualiza as informações
					cliente.Celular = item.TraCelular;
                    cliente.Nome = item.TraNom;
                    cliente.DataNascimento = item.TraDatNasc;
                }
            }
			// Insere os novos clientes ou atualizações no repositório de clientes
			clienteRepository.Inserir(clientes);

			// Retorna a view principal
			return View("Index");
        }
    }
}
