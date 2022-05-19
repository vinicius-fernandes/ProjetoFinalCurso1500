namespace ProjetoFinalCurso1500.Models
{
    public class Salesman
    {
        public string Id { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }

        public double Salarie { get; set; }

        public virtual Concessionaire Concessionaire { get; set; }
        public string IdConcessionaire{ get; set; }
        public virtual ICollection<TestDrive> TestDrives { get; set; }

    }
}
