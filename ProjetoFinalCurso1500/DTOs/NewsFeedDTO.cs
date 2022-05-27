using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models
{
    public class NewsFeedDTO
    {
        [Required(ErrorMessage="O campo do titulo é obrigatório")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Required(ErrorMessage = "O campo do conteúdo é obrigatório")]
        [Display(Name = "Conteúdo")]

        public string Content { get; set; }
        [Required(ErrorMessage = "O campo da imagem é obrigatório")]
        [Display(Name = "Imagem")]
        public string Image { get; set; }
    }
}
