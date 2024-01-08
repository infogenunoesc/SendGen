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
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Web.Models;
using SendGen.Repository.SendGenRepositories;
using SendGen.Web.Controllers.API;
using SendGen.Domain.OpaSuiteDomains.Filtros;
using SendGen.Domain.OpaSuiteDomains.DataResultModels;
using Azure;
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

	public async Task<IActionResult> Create(string SearchString, bool enviado = false)
	{
		List<Cliente> listaClientes = _context.Cliente.ToList();

		if (enviado == true)
		{
			listaClientes = _utilitiesApiRepository.BuscaEntidadeDB<Cliente>(SearchString);
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

		List<FiltroDB> filtros = _context.FiltroDB.ToList();

		return View(filtros);
	}



}
