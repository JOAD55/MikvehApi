using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, SummaryClienteDto>()
            .ForMember(dest => dest.NombreCompleto, 
                opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellidos}"));
    }
}
