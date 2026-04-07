using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class CitaProfile : Profile
{
    public CitaProfile()
    {
        CreateMap<Cita, CitaDto>()
            .ForMember(dest => dest.ClienteNombre,
                opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.TrabajadorNombre,
                opt => opt.MapFrom(src => src.Trabajador != null ?
                    src.Trabajador.Nombre : string.Empty));

        CreateMap<Cita, DetailCitaDto>();
    }
}
