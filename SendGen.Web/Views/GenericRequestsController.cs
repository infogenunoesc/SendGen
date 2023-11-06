using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SendGen.Web.Views
{
    public class GenericRequestsController : Controller
    {
        private string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";

        public async Task<IActionResult> Request(string token, string metodoAPI, string contentJSON)
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
        public async Task<IActionResult> Get([FromBody] JObject jsonData, string enderecoAPI)
        {
            string jsonString = JsonConvert.SerializeObject(jsonData);

            return await Request(tokenOpaSuite, enderecoAPI, jsonString);
        }


    }
}
