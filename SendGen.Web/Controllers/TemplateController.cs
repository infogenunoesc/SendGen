using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SendGen.Repository.SendGenRepositories;
using SendGen.Domain.OpaSuiteDomains;

namespace SendGen.Web.Controllers;

public class TemplateController : Controller
{
    private readonly IUtilitiesRepository utilitiesRepository;

    public TemplateController(IUtilitiesRepository utilitiesRepository)
    {
        this.utilitiesRepository = utilitiesRepository;
    }

    public async Task<IActionResult> templateGet() //JObject jsonData
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

        string stringJSON = JsonSerializer.Serialize(filtros);

        Console.WriteLine(stringJSON);

        return await utilitiesRepository.RequestGet(metodoAPI, stringJSON);
    }

    public IActionResult Index()
    {  
        return View();
    }
}
