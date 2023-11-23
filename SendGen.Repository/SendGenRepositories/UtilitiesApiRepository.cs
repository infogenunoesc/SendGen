using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace SendGen.Repository.SendGenRepositories
{
    //Classe contendo métodos genericos de uso de uma API Request 
    public class UtilitiesApiRepository : IUtilitiesApiRepository
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";

        // Enviar um get com filtro via JSON para a API do Opa Suite
        public async Task<IActionResult> requestGetJSON(string metodoAPI, string contentJSON)
        {
            var endPoint = baseUrlAPI + metodoAPI;

            // Console.WriteLine("\n\ncontentJSON:" + contentJSON + "\n\n");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Authorization", tokenOpaSuite);
            var content = new StringContent(contentJSON, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var retorno = await response.Content.ReadAsStringAsync();

            Console.WriteLine(retorno);
            return new OkObjectResult(retorno);
        }

        // Enviar um get com filtro via URL para a API do Opa Suite
        public async Task<IActionResult> requestGetURL(string metodoAPI, string url)
        {
            var endPoint = baseUrlAPI + metodoAPI + "/" + url;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Authorization", tokenOpaSuite);

            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var retorno = await response.Content.ReadAsStringAsync();

            // Console.WriteLine(retorno);
            return new OkObjectResult(retorno);
        }

    }
}
