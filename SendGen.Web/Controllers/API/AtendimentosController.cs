using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers.API
{
    // Controller dos Atendimentos e Mensagens do Atendimento do Opa Suite 

    public class AtendimentosController : Controller
    {
        private readonly IUtilitiesApiRepository utilitiesApiRepository;

        public AtendimentosController(IUtilitiesApiRepository utilitiesApiRepository)
        {
            this.utilitiesApiRepository = utilitiesApiRepository;
        }

        string metodoAPI = "atendimento";


        // Método referente ao "Atendimentos - Listar atendimentos" da API do Opa Suite

        [HttpPost] //Usado como Post por causa da form de envio via filtro JSON de um "get" do Opa Suite
        public async Task<IActionResult> atendimentosGet(atendimentosGetFilter filtroAtendimentosForm)
        {
            atendimentosGetFilter filtroAtendimentos = new atendimentosGetFilter
            {
                filter = new filterAtendimentos
                {
                    protocolo = filtroAtendimentosForm.filter.protocolo,
                    dataInicialAbertura = filtroAtendimentosForm.filter.dataInicialAbertura,
                    dataFinalAbertura = filtroAtendimentosForm.filter.dataFinalAbertura,
                    dataInicialEncerramento = filtroAtendimentosForm.filter.dataInicialEncerramento,
                    dataFinalEncerramento = filtroAtendimentosForm.filter.dataFinalEncerramento
                },
                options = new options
                {
                    skip = filtroAtendimentosForm.options.skip,
                    limit = filtroAtendimentosForm.options.limit
                }
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string stringJSON = JsonConvert.SerializeObject(filtroAtendimentos, settings);

            var retorno = await utilitiesApiRepository.requestGetJSON(metodoAPI, stringJSON);

            return retorno;
        }

        // Método usado para chamar um atendimento por ID
        public async Task<IActionResult> atendimentosGetID(string atendimentoID)
        {

            if (atendimentoID == null || atendimentoID.Length == 0)
            {
                return BadRequest("É necessário informar o ID de um canal de comunicação.");
            }

            var retorno = await utilitiesApiRepository.requestGetURL(metodoAPI, atendimentoID);

            return retorno;
        }


        // "Controller" das Mensagens de Atendimento da API Opa Suite


        // Método referente ao "Mensagens - Listar mensagens atendimentos" da API do Opa Suite

        [HttpPost] //Usado como Post por causa da form de envio via filtro JSON de um "get" do Opa Suite
        public async Task<IActionResult> mensagensAtendimentoGet(mensagensAtendimentoGetFilter mensagensAtendimentoForm)
        {
            mensagensAtendimentoGetFilter filtroMensagensAtendimento = new mensagensAtendimentoGetFilter
            {
                filter = new filterMensagensAtendimento
                {
                    id_rota = mensagensAtendimentoForm.filter.id_rota
                },
                options = new options
                {
                    skip = mensagensAtendimentoForm.options.skip,
                    limit = mensagensAtendimentoForm.options.limit
                }
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string stringJSON = JsonConvert.SerializeObject(filtroMensagensAtendimento, settings);

            var retorno = await utilitiesApiRepository.requestGetJSON(metodoAPI + "/mensagem", stringJSON);

            return retorno;
        }

        // Método usado para chamar as mensagens de um atendimento por ID ("Mensagens - Buscar mensagem atendimento pupulada" (sim está escrito errado lá)
        public async Task<IActionResult> mensagensAtendimentosGetID(string mensagensAtendimentoID)
        {

            if (mensagensAtendimentoID == null || mensagensAtendimentoID.Length == 0)
            {
                return BadRequest("É necessário informar o ID de um canal de comunicação.");
            }

            var retorno = await utilitiesApiRepository.requestGetURL(metodoAPI + "/mensagem", mensagensAtendimentoID);

            return retorno;
        }


    }
}
