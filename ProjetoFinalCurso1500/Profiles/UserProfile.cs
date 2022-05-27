using AutoMapper;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

        }
    }
}