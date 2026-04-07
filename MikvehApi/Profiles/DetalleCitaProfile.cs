using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class DetalleCitaProfile : Profile
{
    public DetalleCitaProfile()
    {
        CreateMap<DetalleCita, DetalleCitaDto>();
    }
}
