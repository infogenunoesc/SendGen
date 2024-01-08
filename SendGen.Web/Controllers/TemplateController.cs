using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.OpaSuiteRepositories;

namespace SendGen.Web.Controllers
{
	public class TemplateController : Controller
	{
		// Declaração de membros privados para o contexto do banco de dados e o repositório de templates
		private readonly SendGenContexto contexto;
		private readonly ITemplateRepository templateRepository;
		
		// Construtor que realiza a injeção de dependências para o contexto e o repositório
		public TemplateController(

			SendGenContexto contexto,

			ITemplateRepository templateRepository

			)
		{
			this.contexto = contexto;
			this.templateRepository = templateRepository;
		}

		// Ação que retorna a view para selecionar um template
		public async Task<IActionResult> Selecionar(string clienteIds)
		{
			// Obtém a lista de templates usando o método assíncrono do repositório
			var listaDeTemplates = await templateRepository.Get();

			// Configura os dados que serão usados na view (Templates e clienteIds)
			ViewData["Templates"] = new SelectList(listaDeTemplates.data, "_id", "texto");
			ViewData["clienteIds"] = clienteIds;

			return View();
		}

		// Ação que processa o envio de mensagens
		[HttpPost]
		public async Task<IActionResult> EnviarMensagem(string clienteIds, string templateid)
		{
			// Converte a string de clienteIds para uma lista de inteiros
			List<int> arrIds = clienteIds.Split(",").Select(c => Convert.ToInt32(c)).ToList();

			// Obtém os clientes do banco de dados com base nos IDs fornecidos
			var clientes = await contexto.Cliente.Where(c => arrIds.Contains(c.ClienteId)).ToListAsync();

			// Verifica se há clientes
			if (clientes == null)
			{
				return NotFound();
			}

			// loop sobre os clientes e envia mensagens usando o método Send do repositório de templates
			foreach (var cliente in clientes)
			{
				// Verifica se o cliente possui celular cadastrado
				if (cliente.Celular == null || cliente.Celular == "")
				{
					throw new Exception("O cliente informado não possuí celular cadastrado!");
				}

				// Chama o método Send do repositório de templates para enviar a mensagem
				await templateRepository.Send(cliente.Celular.Trim(), cliente.Nome!.Trim(), templateid);
			}

			// Retorna uma resposta JSON indicando o sucesso do envio
			return Json(new
			{
				Situacao = "OK",
				Mensagem = "Mensagem enviada!"
			});
		}

	}
}
