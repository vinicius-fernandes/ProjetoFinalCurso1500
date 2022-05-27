using AutoMapper;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Profiles
{
    public class SalesmanProfile : Profile
    {
        public SalesmanProfile()
        {
            CreateMap<Salesman, SalesmanDTO>();
            CreateMap<SalesmanDTO, Salesman>();

        }
    }
}