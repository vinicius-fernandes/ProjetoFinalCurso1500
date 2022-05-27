using AutoMapper;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Profiles
{
    public class ConcessionaireProfile:Profile
    {
        public ConcessionaireProfile()
        {
            CreateMap<Concessionaire, ConcessionaireDTO>();
            CreateMap<ConcessionaireDTO, Concessionaire>();

        }
    }
}
