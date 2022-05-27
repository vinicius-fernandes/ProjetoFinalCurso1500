using AutoMapper;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Profiles
{
    public class NewsFeedProfile : Profile
    {
        public NewsFeedProfile()
        {
            CreateMap<NewsFeed, NewsFeedDTO>();
            CreateMap<NewsFeedDTO, NewsFeed>();

        }
    }
}