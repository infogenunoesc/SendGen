using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SendGen.Domain.SendGenDomains.Data;
using System.Net.Http.Headers;

namespace SendGen.Repository.SendGenRepositories
{
    //Classe contendo métodos genericos de uso de uma API Request 
    public class UtilitiesApiRepository : IUtilitiesApiRepository
    {
        private readonly string tokenOpaSuite = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1MmMxNzRmNzkwNjkyZmQ4YWYyY2E0ZSIsImlhdCI6MTY5NzM4ODM2N30.g81oPrxP-bP672kt8IGwRPmvFvWzvmMno1Csb8XMNi4";
        private readonly string baseUrlAPI = "https://demo2.opasuite.com.br/api/v1/";
        private readonly string conexaoServer = "Server=localhost; Database=SendGen; Integrated Security=True;TrustServerCertificate=True;";

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

            // Console.WriteLine("Retorno requestGetJSON: " + retorno);
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

        // Converter os dados recebidos da API para List
        public List<T> IActionResultToList<T>(IActionResult objeto) where T : class
        {
            List<T> resposta = null;

            try
            {
                if (objeto is OkObjectResult okObjectResult)
                {
                    var value = okObjectResult.Value;

                    if (value != null)
                    {
                        var valueString = value.ToString();

                        var responseObject = JsonConvert.DeserializeObject<RespostaAPI<T>>(valueString);

                        if (responseObject != null)
                        {
                            resposta = responseObject.data;
                        }
                    }
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Erro ao desserializar o JSON: {e.Message}");
            }

            return resposta;
        }

        // Buscar dados de alguma tabela do banco de dados
        public List<T> BuscaEntidadeDB<T>(string condicao)
        {
            List<T> lista;
            // Inicializa a conexão
            using (var con = new SqlConnection(conexaoServer))
            {
                // Abre a conexão
                con.Open();

                lista = con
                    .Query<T>(@condicao).ToList();
            }

            return lista;

        }

    }
}
