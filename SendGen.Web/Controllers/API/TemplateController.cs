using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers.API;

// Controle dos Templates do Opa Suite
public class TemplateController : Controller
{
    private readonly IUtilitiesApiRepository utilitiesApiRepository;

    public TemplateController(IUtilitiesApiRepository utilitiesApiRepository)
    {
        this.utilitiesApiRepository = utilitiesApiRepository;
    }

    string metodoAPI = "template";

    // Método referente ao "Template - Listar templates" da API do Opa Suite

    [HttpPost] //Usado como Post por causa da form de envio via filtro JSON de um "get" do Opa Suite
    public async Task<IActionResult> templateGet(templateGetFilter filtroTemplateForm) //Recebe os dados via um objeto com os dados do filtro
    {
        //Especificando o filtro
        templateGetFilter filtroTemplate = new templateGetFilter
        {
            filter = new filterTemplate
            {
                atalho = filtroTemplateForm.filter.atalho,
                tipo_mensagem = filtroTemplateForm.filter.tipo_mensagem
            },
            options = new options
            {
                skip = filtroTemplateForm.options.skip,
                limit = filtroTemplateForm.options.limit
            }
        };

        var settings = new JsonSerializerSettings //Tirar espaços e ignorar os nulls do json (filtro) enviado
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        string stringJSON = JsonConvert.SerializeObject(filtroTemplate, settings);

        var retorno = await utilitiesApiRepository.requestGetJSON(metodoAPI, stringJSON);

        return retorno;
    }

    // Método referente ao "Template - Buscar template populado" da API do Opa Suite

    [HttpGet]
    public async Task<IActionResult> templateGetID(string templateID)
    {
        if (templateID == null || templateID.Length == 0)
        {
            return BadRequest("É necessário informar o ID de uma template.");
        }

        var retorno = await utilitiesApiRepository.requestGetURL(metodoAPI, templateID);

        return retorno;
    }


}
