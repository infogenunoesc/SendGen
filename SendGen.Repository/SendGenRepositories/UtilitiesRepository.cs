using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SendGen.Repository.SendGenRepositories
{
    public class UtilitiesRepository : IUtilitiesRepository
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";

        public async Task<IActionResult> RequestGet(string metodoAPI, string contentJSON)
        {
            var endPoint = baseUrlAPI + metodoAPI;

            Console.WriteLine("\n\ncontentJSON:"+contentJSON+"\n\n");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Authorization", tokenOpaSuite);
            var content = new StringContent(contentJSON, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var retorno = await response.Content.ReadAsStringAsync();

            Console.WriteLine(retorno);

            return new OkObjectResult(retorno);
        }
    }
}
