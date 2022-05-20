using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalCurso1500.Models
{
    public class TestDriveDTO
    {


        public DateTime Date { get; set; }
        public virtual Client? Client { get; set; }

        public virtual Concessionaire? Concessionaire { get; set; }

        public virtual Car? Car { get; set; }

        public string IdCar { get; set; }
        [Display(Name ="Concessionária")]
        public string IdConcessionaire { get; set; }
        [Display(Name = "Cliente")]
        public string? IdClient { get; set; }

    }
}
