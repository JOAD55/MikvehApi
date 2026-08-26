using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Profiles;

public class RolProfile : Profile
{
    public RolProfile()
    {
        CreateMap<Rol, RolDto>();
        CreateMap<CreateRolDto, Rol>();
    }
}
