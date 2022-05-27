using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models
{
    public class SalesmanDTO
    {

        public virtual User? User { get; set; }
        [Required(ErrorMessage = "Selecione um usuário")]
        [Display(Name = "Usuário")]

        public string UserId { get; set; }

        public double Salarie { get; set; }

        public virtual Concessionaire? Concessionaire { get; set; }
        [Required(ErrorMessage="Selecione uma concessionária")]
        [Display(Name = "Concessionária")]
        public string IdConcessionaire{ get; set; }
        public virtual ICollection<TestDrive>? TestDrives { get; set; }

    }
}
