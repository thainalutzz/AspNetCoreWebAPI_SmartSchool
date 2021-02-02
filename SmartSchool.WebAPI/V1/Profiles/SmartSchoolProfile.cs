using AutoMapper;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.V1.Dtos;

namespace SmartSchool.WebAPI.V1.Profiles
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                );

                CreateMap<AlunoDto, Aluno>();
                CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

             CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );

                CreateMap<ProfessorDto, Professor>();
                CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}