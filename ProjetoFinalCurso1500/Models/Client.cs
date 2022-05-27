namespace ProjetoFinalCurso1500.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Concessionaire> Concessionairies { get; set; }
        public virtual ICollection<TestDrive> TestDrives { get; set; }

    }
}
