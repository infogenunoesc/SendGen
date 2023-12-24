using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.OpaSuiteRepositories;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using X.PagedList;
using SendGen.Domain.OpaSuiteDomains;

namespace SendGen.Web.Controllers;

public class AutomacaoController : Controller
{
    string conexaoServer = "Server=DESKTOP-H95BSF0\\SQLEXPRESS; Database=SendGen; Integrated Security=True;TrustServerCertificate=True;";

    private readonly SendGenContexto _context;

    public AutomacaoController(SendGenContexto context)
    {
        _context = context;
    }


    private IEnumerable<Cliente> ConectarServer(string condicao)
    {
		IEnumerable<Cliente> lista;
        // Inicializa a conexão
        using (var con = new SqlConnection(conexaoServer))
        {
            // Abre a conexão
            con.Open();

            lista = con
                .Query<Cliente>(@condicao)
            ;

            Console.WriteLine("Condição conecta server: " + condicao);
        }

        return lista;

    }

	public async Task<IActionResult> Create(string SearchString, bool enviado = false)
    {
		IEnumerable<Cliente> listaClientes = _context.Cliente;

		if (enviado == true)
		{
            listaClientes = ConectarServer(SearchString);            
        }

		if (listaClientes == null)
		{
			return NotFound();
		}

		Console.WriteLine("Enviado: " + enviado);

		ViewData["CurrentFilter"] = SearchString;

		Console.WriteLine("CurrentFilter: " + ViewData["CurrentFilter"]);
		
        enviado = false;

        bool? salvo;

        if (TempData["Salvo"] != null)
        {
            salvo = TempData["Salvo"] as bool?;
        } else
        {
            salvo = false;
        }

		ViewData["Salvo"] = salvo;

		Console.WriteLine("Clientes: " + listaClientes);
		Console.WriteLine("Salvo?: " + salvo);

		return View(listaClientes);
    }

    [HttpPost]
    public async Task<IActionResult> Salvar(string condicao)
    {
        FiltroDB filtro = new FiltroDB
        {
            Condicao = condicao
        };

        TempData["Salvo"] = true;

		Console.WriteLine("Salvo: " + TempData["Salvo"]);

		//_context.FiltroDB.Add(filtro);
		//    await _context.SaveChangesAsync();

		Console.WriteLine("Salvar: " + condicao);
		
		return RedirectToAction("Create");
	}

}
