using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models
{
    public class TestDriveDTO
    {


        public DateTime Date { get; set; }
        public virtual Client? Client { get; set; }

        public virtual Concessionaire? Concessionaire { get; set; }

        public virtual Car? Car { get; set; }
        [Required(ErrorMessage = "Selecione um carro!")]
        [Display(Name = "Carro")]
        public string IdCar { get; set; }
        [Required(ErrorMessage = "Selecione uma concessionária!")]
        [Display(Name ="Concessionária")]
        public string IdConcessionaire { get; set; }
        [Required(ErrorMessage = "Selecione um cliente!")]
        [Display(Name = "Cliente")]
        public string? IdClient { get; set; }
        [Required(ErrorMessage ="Selecione um vendedor!")]
        [Display(Name = "Vendedor")]
        public string? IdSalesman { get; set; }

    }
}
