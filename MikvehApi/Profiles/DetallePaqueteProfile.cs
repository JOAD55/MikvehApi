using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class DetallePaqueteProfile : Profile
{
    public DetallePaqueteProfile()
    {
        CreateMap<CreateDetallePaqueteDto, DetallePaquete>();
    }
}
