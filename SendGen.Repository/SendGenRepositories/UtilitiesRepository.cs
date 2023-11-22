using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace SendGen.Repository.SendGenRepositories
{
    public class UtilitiesRepository : IUtilitiesRepository
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";

        public async Task<IActionResult> requestGetJSON(string metodoAPI, string contentJSON)
        {
            var endPoint = baseUrlAPI + metodoAPI;

            // Console.WriteLine("\n\ncontentJSON:"+contentJSON+"\n\n");

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

        public async Task<IActionResult> requestGetURL(string metodoAPI, string id)
        {
            var endPoint = baseUrlAPI + metodoAPI + "/" + id;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Authorization", tokenOpaSuite);

            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var retorno = await response.Content.ReadAsStringAsync();

            Console.WriteLine(retorno);
            return new OkObjectResult(retorno);
        }

    }
}
