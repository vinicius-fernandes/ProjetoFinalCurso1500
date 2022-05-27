namespace ProjetoFinalCurso1500.Models
{
    public class ConcessionaireDTO
    {
        public string Name { get; set; }

        public virtual ICollection<Client>? Clients { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
        public virtual ICollection<Salesman>? Salesmans { get; set; }
        public virtual ICollection<TestDrive>? TestDrives { get; set; }

    }
}
