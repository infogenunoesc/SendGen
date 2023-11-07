using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SendGen.Domain.OpaSuiteDomains;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace SendGen.Web.Controllers
{
    public class GenericController : Controller
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";

        public async Task<IActionResult> requestPost(string token, string metodoAPI, string contentJSON)
        {
            var endPoint = baseUrlAPI + metodoAPI;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, endPoint);
            request.Headers.Add("Authorization", token);
            var content = new StringContent(contentJSON, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

            return Ok(result);
        }
        public async Task<IActionResult> requestGet(string token, string metodoAPI, string contentJSON)
        {
			var endPoint = baseUrlAPI + metodoAPI;

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
			request.Headers.Add("Authorization", token);
			var content = new StringContent(contentJSON, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			Console.WriteLine(await response.Content.ReadAsStringAsync());

            return Ok(response);
}

		[Route("Contato/Post")]
		[HttpPost]
		public async Task<IActionResult> ContatoPost()
		{
			//string metodoAPI = "contato";

			//var jsonData1 = new
			//{
			//	nome = "TestA1",
			//	celularCompleto = "+5549988776655",
			//	requerAutenticacaoSempre = true,
			//	habilitarAlerta = true,
			//	lead = false,
			//	historico_email = true,
			//	senha = "senha",
			//	repetirSenha = "senha"

			//};


			//string stringJSON = JsonConvert.SerializeObject(jsonData1);

			//Console.WriteLine(stringJSON);

			//return await requestPost(tokenOpaSuite, metodoAPI, "{\n\t\"nome\": \"Arnold Schwarzenegger\",\n    \"celularCompleto\": \"+5549988776655\",\n    \"requerAutenticacaoSempre\": true,\n    \"habilitarAlerta\": true,\n    \"lead\": false,\n    \"historico_email\": true,\n    \"senha\": \"senha\",\n    \"repetirSenha\": \"senha\"\n}");

			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, baseUrlAPI+"contato");
			request.Headers.Add("Authorization", tokenOpaSuite);
			var content = new StringContent("{\n\t\"nome\": \"Arnold Schwarzenegger\",\n    \"celularCompleto\": \"+5549988776655\",\n    \"requerAutenticacaoSempre\": true,\n    \"habilitarAlerta\": true,\n    \"lead\": false,\n    \"historico_email\": true,\n    \"senha\": \"senha\",\n    \"repetirSenha\": \"senha\"\n}", null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			Console.WriteLine(await response.Content.ReadAsStringAsync());

			return Ok(response);

		}

		public async Task<IActionResult> ContatoGet(JObject jsonData)
        {
            string metodoAPI = "contato";






            string stringJSON = JsonConvert.SerializeObject(jsonData);

			Console.WriteLine(stringJSON);

			return await requestGet(tokenOpaSuite, metodoAPI, stringJSON);
		}

        

        public IActionResult Index()
        {
            return View();
        }

    }
}
