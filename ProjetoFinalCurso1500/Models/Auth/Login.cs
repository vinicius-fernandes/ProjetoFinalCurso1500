using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models.Auth
{
    public class Login
    {
        [Required(ErrorMessage = "O campo de e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail inserido é inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo de senha é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [Display(Name = "Lembrar de mim?")]
        public bool RememberMe { get; set; }
    }
}
