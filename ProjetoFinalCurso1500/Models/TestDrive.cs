namespace ProjetoFinalCurso1500.Models
{
    public class TestDrive
    {

        public string Id { get; set; }

        public DateTime Date { get; set; }
        public virtual Client Client { get; set; }

        public virtual Concessionaire Concessionaire { get; set; }

        public virtual Salesman Salesman { get; set; }

        public virtual Car Car { get; set; }

        public string IdSalesman { get; set; }
        public string IdCar { get; set; }

        public string IdConcessionaire { get; set; }
        public string IdClient { get; set; }


        public bool Completed { get; set; }
    }
}
