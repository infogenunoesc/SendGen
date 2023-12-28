using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SendGen.Domain.SendGenDomains.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using X.PagedList;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Web.Models;
using SendGen.Repository.SendGenRepositories;
using SendGen.Web.Controllers.API;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using Azure;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace SendGen.Web.Controllers;

public class FiltroController : Controller
{
	string conexaoServer = "Server=DESKTOP-H95BSF0\\SQLEXPRESS; Database=SendGen; Integrated Security=True;TrustServerCertificate=True;";

	private readonly SendGenContexto _context;

    private readonly IUtilitiesApiRepository _utilitiesApiRepository;

	public FiltroController(SendGenContexto context, IUtilitiesApiRepository utilitiesApiRepository)
	{
		_context = context;
        _utilitiesApiRepository = utilitiesApiRepository;
    }

	private IEnumerable<Cliente> BuscaClientesDB(string condicao)
	{
		IEnumerable<Cliente> lista;
		// Inicializa a conexão
		using (var con = new SqlConnection(conexaoServer))
		{
			// Abre a conexão
			con.Open();

			lista = con
				.Query<Cliente>(@condicao);

			Console.WriteLine("Condição conecta server: " + condicao);
		}

		return lista;

	}

	private IEnumerable<FiltroDB> BuscaFiltrosDB()
	{
		string condicao = "SELECT * FROM FILTRODB";

		IEnumerable<FiltroDB> lista;
		// Inicializa a conexão
		using (var con = new SqlConnection(conexaoServer))
		{
			// Abre a conexão
			con.Open();

			lista = con
				.Query<FiltroDB>(@condicao);

			Console.WriteLine("Condição conecta server: " + condicao);
		}

		return lista;
	}

	public async Task<IActionResult> Create(string SearchString, bool enviado = false)
	{
		IEnumerable<Cliente> listaClientes = _context.Cliente;

		if (enviado == true)
		{
			listaClientes = BuscaClientesDB(SearchString);
		}

		if (listaClientes == null)
		{
			return NotFound();
		}

		Console.WriteLine("Enviado: " + enviado);

		ViewData["CurrentFilter"] = SearchString;

		Console.WriteLine("CurrentFilter: " + ViewData["CurrentFilter"]);

		enviado = false;

		Console.WriteLine("Clientes: " + listaClientes);

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
		Console.WriteLine("Filtros: " + _context.FiltroDB.ToJson());

		IEnumerable<FiltroDB> filtros = _context.FiltroDB;

		return View(filtros);
	}

	public async Task<ActionResult> Agendamento(int filtroID)
	{
		FiltroDB filtro = await _context.FiltroDB.FirstOrDefaultAsync(e => e.ID == filtroID);

        var templateController = new TemplateController(_utilitiesApiRepository);
        var canaisController = new CanaisController(_utilitiesApiRepository);

        Console.WriteLine("Filtro ID: " + filtroID);
		Console.WriteLine("Filtro: " + filtro.ToJson());

		IEnumerable<Cliente> listaClientes = BuscaClientesDB(filtro.Condicao);

		List<TemplateGetData> templates = await templateController.templateGet(null, null, 0, 100);

        canaisGetFilter filtroCanais = new canaisGetFilter
        {
            filter = new filterCanais
            {
                
            },
            options = new options
            {
               
            }
        };

        List<CanaisGetData> canais = await canaisController.canaisGet(filtroCanais);

        var viewModel = new FiltroAgendamentoView
		{
			Filtro = filtro,
			Clientes = listaClientes,
			Templates = templates,
			Canais = canais
        };
		
		Console.WriteLine("ViewModel Filtro: " + viewModel.Filtro.ToString());
       
        Console.WriteLine("ViewModel Templates: " + viewModel.Templates.ToJson());

        Console.WriteLine("ViewModel Canais: " + viewModel.Canais.ToJson());


        return View(viewModel);
	}


}
