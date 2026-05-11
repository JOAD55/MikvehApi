using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class ServicioProfile : Profile
{
    public ServicioProfile()
    {
        CreateMap<Servicio, ServicioDto>();
        CreateMap<Servicio, SummaryServicioDto>();
        CreateMap<CreateServicioDto, Servicio>();
        CreateMap<UpdateServicioDto, Servicio>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
