using Microsoft.AspNetCore.Identity;

namespace ProjetoFinalCurso1500.Models
{
    public class User:IdentityUser
    {
        public virtual Client Client { get; set; }


    }
}
