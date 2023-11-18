using System.ComponentModel.DataAnnotations;

namespace SendGen.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o senha")]
        public string Senha { get; set; }
    }
}
