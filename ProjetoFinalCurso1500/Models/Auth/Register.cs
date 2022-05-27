using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models.Auth
{

        public class Register
        {
            [Required(ErrorMessage = "O campo de e-mail é obrigatório!")]
            [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
            [Display(Name = "Email")]
            public string? Email { get; set; }
            [Required(ErrorMessage = "O campo de endereço é obrigatório!")]
            [Display(Name = "Endereço")]
            public string? Address { get; set; }

            [Required(ErrorMessage = "O campo de nome é obrigatório!")]
            [Display(Name = "Nome completo")]
            public string? Name { get; set; }


        [Required(ErrorMessage = "O campo de senha é obrigatório")]
            [StringLength(100, ErrorMessage = "A {0} deve possuir ao menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme a senha")]
            [Compare("Password", ErrorMessage = "A senha e confirmação de senha são diferentes.")]
            public string? ConfirmPassword { get; set; }

        }
    
}
