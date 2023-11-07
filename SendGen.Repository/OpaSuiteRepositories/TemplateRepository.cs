using SendGen.Domain.OpaSuiteDomains;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace SendGen.Repository.OpaSuiteRepositories
{
	public class TemplateRepository : ITemplateRepository
	{

		public async Task Send(string telefoneCliente, string nome)
		{
			if (string.IsNullOrEmpty(telefoneCliente)) throw new ArgumentNullException();
			if (!telefoneCliente.StartsWith("+")) throw new Exception("O telefone deve começar com \"+\".");
			if (string.IsNullOrEmpty(nome)) throw new ArgumentNullException();


#if DEBUG
			telefoneCliente = "+5549999153242";
#endif

			TemplateSend templateSend = new TemplateSend
			{
				contato = new Contato
				{
                    celularCompleto = telefoneCliente
				},
				template = new Template
				{
					_id = "6500b9df32843f9dada2e6be",
					variaveis = new List<string>
					{
						nome
					}
				},
				canal = "64f09e6332843f9dada2d0d6"
			};

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://demo2.opasuite.com.br/api/v1/template/send");

			//request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
			request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4");
			//request.Headers.Add("Content-Type", "application/json");

			StringContent content = new StringContent(JsonSerializer.Serialize(templateSend), null, "application/json");
			//Console.WriteLine(content);

			request.Content = content;
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();

			Console.WriteLine(await response.Content.ReadAsStringAsync());
		}
	}
}
