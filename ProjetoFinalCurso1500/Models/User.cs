using Microsoft.AspNetCore.Identity;

namespace ProjetoFinalCurso1500.Models
{
    public class User:IdentityUser
    {
        public virtual Client Client { get; set; }

        public virtual Salesman Salesman { get; set; }
        public string Address { get; set; }

        public string Name { get; set; }
    }
}
