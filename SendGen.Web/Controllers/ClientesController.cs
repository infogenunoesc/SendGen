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
    [Authorize]
    public class ClientesController : Controller
	{
		private readonly SendGenContexto _context;
		private readonly ITemplateRepository templateRepository;

		public ClientesController(

			SendGenContexto context

			, ITemplateRepository templateRepository


			)
		{
			_context = context;
			this.templateRepository = templateRepository;
		}

		// GET: Clientes
		public async Task<IActionResult> Index(bool somenteComTelefone = false)
		{
			IQueryable<Cliente> listaClientes = _context.Cliente;

			if (somenteComTelefone == true)
			{
				listaClientes = listaClientes.Where(c => c.Celular != null && c.Celular != "");
			}

			ViewData["SomenteComTelefone"] = somenteComTelefone;



			return View(await listaClientes.ToListAsync());
		}

		// GET: Clientes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Cliente == null)
			{
				return NotFound();
			}

			var cliente = await _context.Cliente
				.FirstOrDefaultAsync(m => m.ClienteId == id);
			if (cliente == null)
			{
				return NotFound();
			}

			return View(cliente);
		}

		// GET: Clientes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Clientes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ClienteId,Nome,Celular,DataNascimento,TraCod")] Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				_context.Add(cliente);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(cliente);
		}

		// GET: Clientes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Cliente == null)
			{
				return NotFound();
			}

			var cliente = await _context.Cliente.FindAsync(id);
			if (cliente == null)
			{
				return NotFound();
			}
			return View(cliente);
		}

		// POST: Clientes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nome,Celular,DataNascimento,TraCod")] Cliente cliente)
		{
			if (id != cliente.ClienteId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(cliente);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ClienteExists(cliente.ClienteId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(cliente);
		}

		// GET: Clientes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Cliente == null)
			{
				return NotFound();
			}

			var cliente = await _context.Cliente
				.FirstOrDefaultAsync(m => m.ClienteId == id);
			if (cliente == null)
			{
				return NotFound();
			}

			return View(cliente);
		}

		// POST: Clientes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Cliente == null)
			{
				return Problem("Entity set 'SendGenContexto.Cliente'  is null.");
			}
			var cliente = await _context.Cliente.FindAsync(id);
			if (cliente != null)
			{
				_context.Cliente.Remove(cliente);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ClienteExists(int id)
		{
			return (_context.Cliente?.Any(e => e.ClienteId == id)).GetValueOrDefault();
		}







	}
}
