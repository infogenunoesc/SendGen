using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers;

public class TemplateController : Controller
{
    private readonly IUtilitiesRepository utilitiesRepository;

    public TemplateController(IUtilitiesRepository utilitiesRepository)
    {
        this.utilitiesRepository = utilitiesRepository;
    }

    string metodoAPI = "template";

    [HttpPost]
    public async Task<IActionResult> templateGet(templateGetFilter filtroTemplateForm)
    {

        templateGetFilter filtroTemplate = new templateGetFilter
        {
            filter = new filterTemplate
            {
                atalho = filtroTemplateForm.filter.atalho,
                tipo_mensagem = filtroTemplateForm.filter.tipo_mensagem
            },
            options = new optionsTemplate
            {
                skip = filtroTemplateForm.options.skip,
                limit = filtroTemplateForm.options.limit
            }
        };

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };


        string stringJSON = JsonConvert.SerializeObject(filtroTemplate, settings);

        var retorno = await utilitiesRepository.requestGetJSON(metodoAPI, stringJSON);

        return retorno;
    }

    [HttpGet]
    public async Task<IActionResult> templateGetID(string templateID)
    {
        if (templateID == null || templateID.Length == 0)
        {
            return BadRequest("É necessário informar o ID de uma template.");

        }

        var retorno = await utilitiesRepository.requestGetURL(metodoAPI, templateID);

        return retorno;
    }


    public IActionResult Index()
    {
        return View();
    }
}
