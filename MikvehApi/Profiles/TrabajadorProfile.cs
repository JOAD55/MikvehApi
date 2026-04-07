using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class TrabajadorProfile : Profile
{
    public TrabajadorProfile()
    {
        CreateMap<Trabajador, SummaryTrabajadorDto>()
            .ForMember(dest => dest.NombreCompleto, 
                opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellidos}"));
    }
}
