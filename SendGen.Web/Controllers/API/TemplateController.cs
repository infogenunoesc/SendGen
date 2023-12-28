using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers.API;

// Controle dos Templates do Opa Suite
public class TemplateController : Controller
{
    private readonly IUtilitiesApiRepository _utilitiesApiRepository;

    public TemplateController(IUtilitiesApiRepository utilitiesApiRepository)
    {
        _utilitiesApiRepository = utilitiesApiRepository;
    }

    string metodoAPI = "template";

        // Método referente ao "Template - Listar templates" da API do Opa Suite

        [HttpPost] //Usado como Post por causa da form de envio via filtro JSON de um "get" do Opa Suite
    public async Task<List<TemplateGetData>> templateGet(string? atalho, string? tipoMensagem, int? skip, int? limit) //Recebe o filtro e retorna uma lista, caso existir algum elemento
    {
         templateGetFilter filtroTemplate = new templateGetFilter
        {
            filter = new filterTemplate
            {
                atalho = atalho,
                tipo_mensagem = tipoMensagem
            },
            options = new options
            {
                skip = skip,
                limit = limit
            }
        };

        var settings = new JsonSerializerSettings //Tirar espaços e ignorar os nulls do json (filtro) enviado
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        string stringJSON = JsonConvert.SerializeObject(filtroTemplate, settings);

        Console.WriteLine(stringJSON);

        var retorno = await _utilitiesApiRepository.requestGetJSON(metodoAPI, stringJSON);

        List<TemplateGetData> resposta = _utilitiesApiRepository.IActionResultToList<TemplateGetData>(retorno);

        return resposta;
    }

    // Método referente ao "Template - Buscar template populado" da API do Opa Suite

    [HttpGet]
    public async Task<IActionResult> templateGetID(int templateID)
    {
        if (templateID == null)
        {
            return BadRequest("É necessário informar o ID de uma template.");
        }

        var retorno = await _utilitiesApiRepository.requestGetURL(metodoAPI, templateID.ToString());

        return retorno;
    }


}
