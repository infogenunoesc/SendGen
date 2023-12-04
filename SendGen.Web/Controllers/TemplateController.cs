using Microsoft.AspNetCore.Mvc;
using SendGen.Repository.SendGenRepositories;
using SendGen.Domain.OpaSuiteDomains;
using Newtonsoft.Json;

namespace SendGen.Web.Controllers;

public class TemplateController : Controller
{
    private readonly IUtilitiesRepository utilitiesRepository;

    public TemplateController(IUtilitiesRepository utilitiesRepository)
    {
        this.utilitiesRepository = utilitiesRepository;
    }

    public async Task<IActionResult> templateGet(string atalho, string tipo_mensagem, int skip, int limit) //JObject jsonData
    {
        string metodoAPI = "template";

        atalho = "Informal";

        templateFilter filtros = new templateFilter
        {
            filter = new filter
            {
                atalho = atalho,
                tipo_mensagem = tipo_mensagem
            },
            options = new options
            {
                skip = skip,
                limit = limit
            }
        };

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };


        string stringJSON = JsonConvert.SerializeObject(filtros, settings);

        return await utilitiesRepository.RequestGet(metodoAPI, stringJSON);
    }

    public IActionResult Index()
    {  
        return View();
    }
}
