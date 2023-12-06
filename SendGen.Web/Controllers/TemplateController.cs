using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.OpaSuiteRepositories;

namespace SendGen.Web.Controllers
{
	public class TemplateController : Controller
	{
		private readonly SendGenContexto contexto;
		private readonly ITemplateRepository templateRepository;

		public TemplateController(

			SendGenContexto contexto,

			ITemplateRepository templateRepository

			)
		{
			this.contexto = contexto;
			this.templateRepository = templateRepository;
		}



		public async Task<IActionResult> Selecionar(int clienteid)
		{
			var listaDeTemplates = await templateRepository.Get();
			ViewData["Templates"] = new SelectList(listaDeTemplates.data, "_id", "texto");
			ViewData["ClienteId"] = clienteid;

			return View();
		}






		[HttpPost]
		public async Task<IActionResult> EnviarMensagem(int clienteId, string templateid)
		{
			Cliente? cliente = await contexto.Cliente.FirstOrDefaultAsync(c => c.ClienteId == clienteId);

			if (cliente == null)
			{
				return NotFound();
			}

			if (cliente.Celular == null || cliente.Celular == "")
			{
				throw new Exception("O cliente informado não possuí celular cadastrado!");
			}

			await templateRepository.Send(cliente.Celular.Trim(), cliente.Nome!.Trim(), templateid);

			return Json(new
			{
				Situacao = "OK",
				Mensagem = "Mensagem enviada!"
			});
		}

	}
}
