using AutoMapper;
using DB.Models;
using Desarrollo.DTOs;
using System.Data;

namespace Desarrollo.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Trabajadores, TrabajadoresMostrarDto>();
            CreateMap<TrabajadoresCrearDto, Trabajadores>();

            CreateMap<Departamento, DepartamentoMostrarDto>();
            CreateMap<DepartamentoCrearDto, Departamento>();

            CreateMap<Provincia, ProvinciaMostrarDto>()
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.IdDepartamentoNavigation ?? null));
            CreateMap<ProvinciaCrearDto, Provincia>();

            CreateMap<Distrito, DistritoMostrarDto>()
                .ForMember(dest => dest.Provincia, act => act.MapFrom(src => src.IdProvinciaNavigation ?? null));
            CreateMap<DistritoCrearDto, Distrito>();
        }
    }
}
