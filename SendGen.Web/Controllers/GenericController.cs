using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace SendGen.Web.Controllers
{
    public class GenericController : Controller
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";

        public async Task<IActionResult> APIrequest(string token, string metodoAPI, string contentJSON)
        {
            var endPoint = baseUrlAPI + metodoAPI;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Authorization", token);
            var content = new StringContent(contentJSON, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ContatoControllerGet([FromBody] JObject jsonData)
        {
            string enderecoAPI = "contato";

            string contatoJSON = JsonConvert.SerializeObject(jsonData);

            return await APIrequest(tokenOpaSuite, enderecoAPI, contatoJSON);

        }

        [HttpPost]
        public async Task<IActionResult> ContatoControllerPost([FromBody] JObject jsonData)
        {
            string enderecoAPI = "contato";

            //var contato = new { 
            //    id_contato = 1,
            //    id_cliente = 1,
            //    id_fornecedor = 1,
            //    nome = "",
            //    classificacao = 1,
            //    foneresidencialcompleto = 1,
            //    fonecomercialcompleto = 1,
            //    celularcompleto = 1,
            //    whatsappcompleto = 1,
            //    requerautenticacaosempre = 1,
            //    habilitaralerta = 1,
            //    lead = 1, 
            //    historico_email = 1,
            //    email_principal = 1,
            //    mensagemalerta = 1,
            //    senha = 1,
            //    repetirsenha = 1
            //};

            var contato = new { 
                nome = "Arnold Schwarzenegger",
                celularCompleto = "+5549988776655",
                requerAutenticacaoSempre = true,
                habilitarAlerta = true,
                lead = false,
                historico_email = true,
                senha = "senha",
                repetirSenha = "senha"
            };

            string contatoJSON = JsonConvert.SerializeObject(jsonData);

            return await APIrequest(tokenOpaSuite, enderecoAPI, contatoJSON);

        }


    }
}
