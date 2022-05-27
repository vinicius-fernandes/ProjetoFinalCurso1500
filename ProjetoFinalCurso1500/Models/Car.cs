namespace ProjetoFinalCurso1500.Models
{
    public class Car
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string IdConcessionaire { get; set; }
        public virtual Concessionaire Concessionaire { get; set; }
        public virtual ICollection<TestDrive> TestDrives { get; set; }


    }
}
