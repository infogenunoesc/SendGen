using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SendGen.Domain.SaborColonialDomains.Data;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.SaborColonialRepositories;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers
{
    [Authorize]
    public class ObterClientesController : Controller
    {
        private readonly IClienteRepository clienteRepository;

        public ObterClientesController(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Processar()
        {
            var transacionadoresRepository = new TransacionadoresRepository();
            List<Transacionadores> listaTransacionadores = transacionadoresRepository.ObterClientes();

            List<Cliente> clientesJaCadastrados = clienteRepository.ObterClientes().ToList();

            List<Cliente> clientes = new List<Cliente>();
            foreach (var item in listaTransacionadores)
            {
                Cliente? cliente = clientesJaCadastrados.Where(c => c.TraCod == item.TraCod).SingleOrDefault();


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
                    cliente.Celular = item.TraCelular;
                    cliente.Nome = item.TraNom;
                    cliente.DataNascimento = item.TraDatNasc;
                }
            }

            clienteRepository.Inserir(clientes);




            //List<Cliente> aniverariantes = clienteRepository.ObterClientes().Where(c => c.DataNascimento.Value.Month == DateTime.Today.Month && c.DataNascimento.Value.Day == DateTime.Today.Day).ToList();



            return View("Index");
        }
    }
}
