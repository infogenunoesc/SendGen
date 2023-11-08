using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGen.Repository.SendGenRepositories
{
    public interface IUtilitiesRepository
    {
        public Task<IActionResult> RequestGet(string metodoAPI, string contentJSON);
    }
}
