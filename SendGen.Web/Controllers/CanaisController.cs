using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers
{
    // Controle dos Canais de Comunicação

    public class CanaisController : Controller
    {
        private readonly IUtilitiesRepository utilitiesRepository;

        public CanaisController(IUtilitiesRepository utilitiesRepository)
        {
            this.utilitiesRepository = utilitiesRepository;
        }

        string metodoAPI = "canal-comunicacao";

        [HttpPost]
        public async Task<IActionResult> canaisGet(canaisGetFilter filtroCanaisForm)
        {
            canaisGetFilter filtroCanais = new canaisGetFilter
            {
                filter = new filterCanais
                {
                    nome = filtroCanaisForm.filter.nome,
                    id_atendente = filtroCanaisForm.filter.id_atendente,
                    status = filtroCanaisForm.filter.status,
                    canal = filtroCanaisForm.filter.canal,
                    integracao = filtroCanaisForm.filter.integracao
                },
                options = new optionsCanais
                {
                    skip = filtroCanaisForm.options.skip,
                    limit = filtroCanaisForm.options.limit
                }
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string stringJSON = JsonConvert.SerializeObject(filtroCanais, settings);

            var retorno = await utilitiesRepository.requestGetJSON(metodoAPI, stringJSON);

            return retorno;
        }

        // Método usado para chamar os outros gets da api dos canais
        public async Task<IActionResult> canaisGetID(string canalID, string metodo)
        {
            // Métodos válidos: CanalID, CanalTemplate e CanalTemplateLimite

            if (canalID == null || canalID.Length == 0)
            {
                return BadRequest("É necessário informar o ID de um canal de comunicação.");
            }

            var urlAPI = canalID;

            if (metodo == "CanalTemplate") {
                urlAPI += "/template";
            } else if (metodo == "CanalTemplateLimite")
            {
                urlAPI += "/template/limites-diarios-envio";
            } else if (metodo != "CanalID")
            {
                Console.WriteLine("\n\nMétodo inválido!\n\n");
            } 

            var retorno = await utilitiesRepository.requestGetURL(metodoAPI, urlAPI);

            return retorno;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
