using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers.API
{
    // Controle dos Canais de Comunicação do Opa Suite

    public class CanalController : Controller
    {
        private readonly IUtilitiesApiRepository _utilitiesApiRepository;

        public CanalController(IUtilitiesApiRepository utilitiesApiRepository)
        {
            _utilitiesApiRepository = utilitiesApiRepository;
        }

        private readonly string metodoAPI = "canal-comunicacao";

        // Método referente ao "Canais de Comunicação - Listar canais de comunicação" da API do Opa Suite

        // Retorna lista de canais 
        public async Task<List<CanalGetData>> canalGet(canalGetFilter? filtroCanalForm)
        {
            if (filtroCanalForm == null)
            {
                filtroCanalForm = new canalGetFilter
                {
                    filter = new filterCanais { },
                    options = new options { }
                };
            }

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string stringJSON = JsonConvert.SerializeObject(filtroCanalForm, settings);

            var retorno = await _utilitiesApiRepository.requestGetJSON(metodoAPI, stringJSON);

            List<CanalGetData> resposta = _utilitiesApiRepository.IActionResultToList<CanalGetData>(retorno);

            return resposta;
        }

        // Método usado para chamar os outros gets da api dos canais
        public async Task<List<CanalGetData>> canalGetID(string canalID, string metodo)
        {
            // Métodos válidos: CanalID, CanalTemplate e CanalTemplateLimite

            if (canalID == null || canalID.Length == 0)
            {
                return null;
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

            var retorno = await _utilitiesApiRepository.requestGetURL(metodoAPI, urlAPI);

            List<CanalGetData> resposta = _utilitiesApiRepository.IActionResultToList<CanalGetData>(retorno);

            return resposta;
        }

    }
}
