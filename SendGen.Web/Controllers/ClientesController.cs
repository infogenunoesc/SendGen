using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.OpaSuiteRepositories;

namespace SendGen.Web.Controllers
{
	// O atributo [Authorize] indica que somente usuários autenticados podem acessar este controlador
	[Authorize]
    public class ClientesController : Controller
	{
		private readonly SendGenContexto _context;
		private readonly ITemplateRepository templateRepository;

		// Construtor que realiza a injeção de dependências do contexto do banco de dados e do repositório de templates

		public ClientesController(

			SendGenContexto context
			, ITemplateRepository templateRepository
			)
		{
			_context = context;
			this.templateRepository = templateRepository;
		}

		// GET: Clientes
		// Ação que retorna a lista de clientes, com a opção de filtrar apenas aqueles com telefone

		public async Task<IActionResult> Index(bool somenteComTelefone = false)
		{
			// Inicializa a lista de clientes com todos os clientes do contexto

			IQueryable<Cliente> listaClientes = _context.Cliente;

			// Filtra os clientes com base na opção somenteComTelefone

			if (somenteComTelefone == true)
			{
				listaClientes = listaClientes.Where(c => c.Celular != null && c.Celular != "");
			}

			// Define a variável ViewData para ser usada na view

			ViewData["SomenteComTelefone"] = somenteComTelefone;

<<<<<<< HEAD
			// Retorna a view com a lista de clientes

=======
>>>>>>> main
			return View(await listaClientes.ToListAsync());
		}

		
		// Ação que exibe detalhes de um cliente específico
		public async Task<IActionResult> Details(int? id)
		{
			// Verifica se o ID é nulo ou se não há clientes no contexto

			if (id == null || _context.Cliente == null)
			{
				return NotFound();
			}

			// Obtém o cliente com o ID fornecido

			var cliente = await _context.Cliente
				.FirstOrDefaultAsync(m => m.ClienteId == id);

			// Se o cliente não for encontrado, retorna NotFound

			if (cliente == null)
			{
				return NotFound();
			}

			// Retorna a view com os detalhes do cliente

			return View(cliente);
		}

		// GET: Clientes/Create
		// Ação que exibe o formulário de criação de um novo cliente
		public IActionResult Create()
		{
			return View();
		}

		// POST: Clientes/Create
		// Ação que processa a criação de um novo cliente a partir do formulário

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ClienteId,Nome,Celular,DataNascimento,TraCod")] Cliente cliente)
		{

			// Verifica se o modelo recebido é válido

			if (ModelState.IsValid)
			{

				// Adiciona o novo cliente ao contexto e salva as alterações no banco de dados

				_context.Add(cliente);
				await _context.SaveChangesAsync();

				// Redireciona para a ação Index após a criação bem-sucedida

				return RedirectToAction(nameof(Index));
			}

			// Se o modelo não for válido, retorna a view com o cliente para correção

			return View(cliente);
		}

		// GET: Clientes/Edit/5
		// Ação que exibe o formulário de edição de um cliente específico

		public async Task<IActionResult> Edit(int? id)
		{
			// Verifica se o ID é nulo ou se não há clientes no contexto

			if (id == null || _context.Cliente == null)
			{
				return NotFound();
			}

			// Obtém o cliente com o ID fornecido

			var cliente = await _context.Cliente.FindAsync(id);
			if (cliente == null)
			{
				// Se o cliente não for encontrado, retorna NotFound

				return NotFound();
			}

			// Retorna a view com o formulário de edição preenchido com os dados do cliente

			return View(cliente);
		}

		// POST: Clientes/Edit/5
		// Ação que processa a edição de um cliente a partir do formulário

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nome,Celular,DataNascimento,TraCod")] Cliente cliente)
		{
			// Verifica se o ID fornecido corresponde ao ID do cliente no modelo

			if (id != cliente.ClienteId)
			{
				return NotFound();
			}

			// Verifica se o ID fornecido corresponde ao ID do cliente no modelo

			if (ModelState.IsValid)
			{
				try
				{
					// Atualiza o cliente no contexto e salva as alterações no banco de dados

					_context.Update(cliente);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					// Trata exceções de concorrência durante a atualização do banco de dados

					if (!ClienteExists(cliente.ClienteId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				// Redireciona para a ação Index após a edição bem-sucedida

				return RedirectToAction(nameof(Index));
			}
			return View(cliente);
		}

		// GET: Clientes/Delete/5
		// Ação que exibe a confirmação de exclusão de um cliente específico

		public async Task<IActionResult> Delete(int? id)
		{
			// Verifica se o ID é nulo ou se não há clientes no contexto

			if (id == null || _context.Cliente == null)
			{
				return NotFound();
			}

			// Obtém o cliente com o ID fornecido

			var cliente = await _context.Cliente
				.FirstOrDefaultAsync(m => m.ClienteId == id);

			// Se o cliente não for encontrado, retorna NotFound

			if (cliente == null)
			{
				return NotFound();
			}

			// Retorna a view com a confirmação de exclusão do cliente

			return View(cliente);
		}

		// POST: Clientes/Delete/5
		// Ação que processa a exclusão de um cliente a partir da confirmação

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			// Verifica se o contexto de clientes é nulo

			if (_context.Cliente == null)
			{
				return Problem("Entity set 'SendGenContexto.Cliente'  is null.");
			}

			// Obtém o cliente com o ID fornecido

			var cliente = await _context.Cliente.FindAsync(id);

			// Se o cliente existe, remove do contexto e salva as alterações no banco de dados

			if (cliente != null)
			{
				_context.Cliente.Remove(cliente);
			}

			await _context.SaveChangesAsync();

			// Redireciona para a ação Index após a exclusão bem-sucedida

			return RedirectToAction(nameof(Index));
		}

		private bool ClienteExists(int id)
		{
			return (_context.Cliente?.Any(e => e.ClienteId == id)).GetValueOrDefault();
		}

	}
}
