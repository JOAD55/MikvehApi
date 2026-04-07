using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class PaqueteProfile : Profile
{
    public PaqueteProfile()
    {
        CreateMap<Paquete, SummaryPaqueteDto>();
    }
}
