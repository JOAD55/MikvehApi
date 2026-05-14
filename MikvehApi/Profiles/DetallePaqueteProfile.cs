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
        CreateMap<DetallePaquete, DetallePaqueteDto>()
            .ForMember(dest => dest.NombrePaquete,
                opt => opt.MapFrom(src => src.Paquete != null ?
                    src.Paquete.Nombre : string.Empty))
            .ForMember(dest => dest.NombreServicio,
                opt => opt.MapFrom(src => src.Servicio != null ?
                    src.Servicio.Nombre : string.Empty));
    }
}
