using Newtonsoft.Json;
using SendGen.Domain.OpaSuiteDomains;
using SendGen.Domain.OpaSuiteDomains.OpaSuiteTemplate;

namespace SendGen.Repository.OpaSuiteRepositories
{
	public class TemplateRepository : ITemplateRepository
	{


		public async Task<OpaSuiteTemplateListResponse> Get()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://demo2.opasuite.com.br/api/v1/template");

			request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4");

			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();


			string retornoStr = await response.Content.ReadAsStringAsync();
			
			OpaSuiteTemplateListResponse retornoObjeto = JsonConvert.DeserializeObject<OpaSuiteTemplateListResponse>(retornoStr);


			return retornoObjeto;
		}


		public async Task Send(string telefoneCliente, string nome, string templateid)
		{
			if (string.IsNullOrEmpty(telefoneCliente)) throw new ArgumentNullException();
			if (!telefoneCliente.StartsWith("+")) throw new Exception("O telefone deve começar com \"+\".");
			if (string.IsNullOrEmpty(nome)) throw new ArgumentNullException();


#if DEBUG
			telefoneCliente = "+5549999153242";
#endif

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


			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://demo2.opasuite.com.br/api/v1/template/send");

			//request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
			request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4");
			//request.Headers.Add("Content-Type", "application/json");

			StringContent content = new StringContent(JsonConvert.SerializeObject(templateSend), null, "application/json");
			//Console.WriteLine(content);

			request.Content = content;
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();

			Console.WriteLine(await response.Content.ReadAsStringAsync());
		}
	}
}
