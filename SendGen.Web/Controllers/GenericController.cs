using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;

namespace SendGen.Web.Controllers
{
    public class GenericController : ControllerBase
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";

        public async Task<IActionResult> APIrequest(string token, string enderecoAPI, string contentJSON)
        {
            var endPoint = "https://demo2.opasuite.com.br/api/v1/" + enderecoAPI;

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

        public async Task<IActionResult> ContatoController()
        {
            string enderecoAPI = "/contato/";

            var contato = new { 
                id_contato = 1,
                id_cliente = 1,
                id_fornecedor = 1,
                nome = "",
                classificacao = 1,
                foneresidencialcompleto = 1,
                fonecomercialcompleto = 1,
                celularcompleto = 1,
                whatsappcompleto = 1,
                requerautenticacaosempre = 1,
                habilitaralerta = 1,
                lead = 1, 
                historico_email = 1,
                email_principal = 1,
                mensagemalerta = 1,
                senha = 1,
                repetirsenha = 1
            };

            string contatoJSON = JsonConvert.SerializeObject(contato);

            return await APIrequest(tokenOpaSuite, enderecoAPI, contatoJSON);
        }


    }
}
