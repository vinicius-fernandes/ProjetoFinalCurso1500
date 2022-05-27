using AutoMapper;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Profiles
{
    public class TestDriveProfile : Profile
    {
        public TestDriveProfile()
        {
            CreateMap<TestDrive, TestDriveDTO>();
            CreateMap<TestDriveDTO, TestDrive>();

        }
    }
}