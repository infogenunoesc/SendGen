using Microsoft.AspNetCore.Mvc;

namespace SendGen.Repository.SendGenRepositories
{
    public interface IUtilitiesApiRepository
    {
        public Task<IActionResult> requestGetJSON(string metodoAPI, string contentJSON);
        public Task<IActionResult> requestGetURL(string metodoAPI, string id);
    }


}
