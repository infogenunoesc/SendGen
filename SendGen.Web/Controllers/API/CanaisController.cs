using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers.API
{
    // Controle dos Canais de Comunicação do Opa Suite

    public class CanaisController : Controller
    {
        private readonly IUtilitiesApiRepository utilitiesApiRepository;

        public CanaisController(IUtilitiesApiRepository utilitiesApiRepository)
        {
            this.utilitiesApiRepository = utilitiesApiRepository;
        }

        string metodoAPI = "canal-comunicacao";

        // Método referente ao "Canais de Comunicação - Listar canais de comunicação" da API do Opa Suite

        [HttpPost] //Usado como Post por causa da form de envio via filtro JSON de um "get" do Opa Suite
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
                options = new options
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

            var retorno = await utilitiesApiRepository.requestGetJSON(metodoAPI, stringJSON);

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

            if (metodo == "CanalTemplate")
            {
                urlAPI += "/template";
            }
            else if (metodo == "CanalTemplateLimite")
            {
                urlAPI += "/template/limites-diarios-envio";
            }
            else if (metodo != "CanalID")
            {
                Console.WriteLine("\n\nMétodo inválido!\n\n");
            }

            var retorno = await utilitiesApiRepository.requestGetURL(metodoAPI, urlAPI);

            return retorno;
        }

    }
}
