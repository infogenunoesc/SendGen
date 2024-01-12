using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.SendGenRepositories;

namespace SendGen.Web.Controllers;

public class FiltroController : Controller
{
	private readonly SendGenContexto _context;
	private readonly IUtilitiesApiRepository _utilitiesApiRepository;

	public FiltroController(SendGenContexto context, IUtilitiesApiRepository utilitiesApiRepository)
	{
		_context = context;
		_utilitiesApiRepository = utilitiesApiRepository;
	}

	public async Task<IActionResult> Create(string SearchString, bool enviado = false)
	{
		List<Cliente> listaClientes = _context.Cliente.ToList();

		if (enviado == true)
		{
			listaClientes = _utilitiesApiRepository.BuscaEntidadeDB<Cliente>(SearchString);
			await Salvar(SearchString);
			ViewData["Salvo"] = true;
		}

		if (listaClientes == null)
		{
			return NotFound();
		}

		Console.WriteLine("Enviado: " + enviado);

		ViewData["CurrentFilter"] = SearchString;

		Console.WriteLine("CurrentFilter: " + ViewData["CurrentFilter"]);

		enviado = false;

		Console.WriteLine("Clientes: " + listaClientes.ToJson());

		return View(listaClientes);
	}

	[HttpPost]
	public async Task Salvar(string condicao)
	{
		FiltroDB filtro = new FiltroDB
		{
			Condicao = condicao
		};

		_context.FiltroDB.Add(filtro);
		await _context.SaveChangesAsync();

		Console.WriteLine("Salvar: " + condicao);
	}

	public async Task<ActionResult> Index()
	{
		List<FiltroDB> filtros = _context.FiltroDB.ToList();

		Console.WriteLine("Filtros: " + _context.FiltroDB.ToJson());

		return View(filtros);
	}
}
