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


    public IActionResult templateGet() //JObject jsonData
    {
        string metodoAPI = "contato";


        templateFilter filtros = new templateFilter
        {
            filter = new filter
            {
                atalho = null,
                tipo_mensagem = null
            },
            options = new options
            {
                limit = 1,
                skip = 0
            }
        };


        string stringJSON = JsonConvert.SerializeObject(filtros);

        Console.WriteLine(stringJSON);

        return new OkObjectResult(utilitiesRepository.RequestGet(metodoAPI, stringJSON));
    }

    public IActionResult Index()
    {
        return View();
    }
}
