using SendGen.Domain.OpaSuiteDomains;
using System.Text.Json;

namespace SendGen.Repository.OpaSuiteRepositories
{
    public class TemplateRepository : ITemplateRepository
    {

        public async Task Send(string telefoneCliente)
        {
            TemplateSend templateSend = new TemplateSend
            {
                contato = new Contato
                {
                    canalCliente = telefoneCliente
                },
                template = new Template
                {
                    _id = "6500b9df32843f9dada2e6be",
                    variaveis = new List<string>
                    {

                    }
                },
                canal = "64f09e6332843f9dada2d0d6"
            };


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://demo2.opasuite.com.br/api/v1/template");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY0ZGU1M2U2ZTZjYTQxZWE0YzFiOTk2YiIsImlhdCI6MTY5MjI5MjQ2Mn0.qeaAujfZ1gXlODjU3JAjoNVYbA_5QiMPQPmph01xujI");

            StringContent content = new StringContent(JsonSerializer.Serialize(templateSend), null, "application/json");

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
