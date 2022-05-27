using AutoMapper;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Profiles
{
    public class ClientProfiles:Profile
    {
        public ClientProfiles()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

        }
    }
}
