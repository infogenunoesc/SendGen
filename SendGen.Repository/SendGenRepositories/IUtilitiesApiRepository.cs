using Microsoft.AspNetCore.Mvc;

namespace SendGen.Repository.SendGenRepositories
{
    public interface IUtilitiesApiRepository
    {
        public Task<IActionResult> requestGetJSON(string metodoAPI, string contentJSON);
        public Task<IActionResult> requestGetURL(string metodoAPI, string id);
        public List<T> IActionResultToList<T>(IActionResult objeto) where T : class;
        public List<T> BuscaEntidadeDB<T>(string condicao);
    }


}
