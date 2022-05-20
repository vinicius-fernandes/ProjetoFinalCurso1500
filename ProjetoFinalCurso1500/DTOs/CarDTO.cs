using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models
{
    public class CarDTO
    {
        public string Model { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        [Required(ErrorMessage = "Selecione uma concessionária")]
        [Display(Name="Concessionária")]
        public string IdConcessionaire { get; set; }
        public virtual Concessionaire? Concessionaire { get; set; }
        public virtual ICollection<TestDrive>? TestDrives { get; set; }


    }
}
