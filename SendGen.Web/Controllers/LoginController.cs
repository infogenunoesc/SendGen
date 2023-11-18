using Microsoft.AspNetCore.Mvc;
using SendGen.Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SendGen.Web.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (loginModel.Login == "adm" && loginModel.Senha == "123")
					{
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Tente novamente!";

                }
				return View("Index");
			}

			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente. Detalhe do erro: {erro.Message}";
				return RedirectToAction("Index");
			}
			
		}
	}
}
