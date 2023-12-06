using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Domain.OpaSuiteDomains.OpaSuiteTemplate;
using SendGen.Domain.OpaSuiteDomains;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace SendGen.Repository.OpaSuiteRepositories
{
	public class TemplateRepository : ITemplateRepository
	{

		// Método para obter a lista de templates da API OpaSuite
		public async Task<OpaSuiteTemplateListResponse> Get()
		{
			// Cria uma instância do cliente HTTP
			var client = new HttpClient();

			// Cria uma requisição HTTP GET para a API OpaSuite
			var request = new HttpRequestMessage(HttpMethod.Get, "https://demo2.opasuite.com.br/api/v1/template");

			// Adiciona o cabeçalho de autorização com o token JWT
			request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4");
			
			// Envia a requisição e aguarda a resposta
			var response = await client.SendAsync(request);

			// Certifica-se de que a resposta foi bem-sucedida (código 200)
			response.EnsureSuccessStatusCode();

			// Lê o conteúdo da resposta como uma string
			string retornoStr = await response.Content.ReadAsStringAsync();

			// Desserializa a string JSON em um objeto OpaSuiteTemplateListResponse
			OpaSuiteTemplateListResponse retornoObjeto = JsonConvert.DeserializeObject<OpaSuiteTemplateListResponse>(retornoStr);

			// Retorna o objeto desserializado
			return retornoObjeto;
		}

		// Método para enviar uma mensagem usando um template específico para um cliente
		public async Task Send(string telefoneCliente, string nome, string templateid)
		{
			// Validação dos parâmetros de entrada
			if (string.IsNullOrEmpty(telefoneCliente)) throw new ArgumentNullException();
			if (!telefoneCliente.StartsWith("+")) throw new Exception("O telefone deve começar com \"+\".");
			if (string.IsNullOrEmpty(nome)) throw new ArgumentNullException();

			// Cria um objeto OpaSuiteSend
			OpaSuiteSend templateSend = new OpaSuiteSend
			{
				contato = new OpaSuiteContato
				{
					canalCliente = telefoneCliente
				},
				template = new OpaSuiteTemplateSend
				{
					_id = templateid,
					variaveis = new List<string>
					{
						nome
					}
				},
				canal = "64f09e6332843f9dada2d0d6"
			};

			// Cria uma instância do cliente HTTP
			var client = new HttpClient();

			// Cria uma requisição HTTP POST para enviar a mensagem para a API OpaSuite
			var request = new HttpRequestMessage(HttpMethod.Post, "https://demo2.opasuite.com.br/api/v1/template/send");

			// Adiciona o cabeçalho de autorização com o token JWT
			request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4");

			// Converte o objeto OpaSuiteSend para uma string JSON e configura o conteúdo da requisição
			StringContent content = new StringContent(JsonConvert.SerializeObject(templateSend), null, "application/json");
			//Console.WriteLine(content);
			request.Content = content;

			// Envia a requisição e aguarda a resposta
			var response = await client.SendAsync(request);

			// Certifica-se de que a resposta foi bem-sucedida
			response.EnsureSuccessStatusCode();

			Console.WriteLine(await response.Content.ReadAsStringAsync());
		}


    }
}
