using System;
using AutoMapper;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class RolRepository : Repository<Rol>, IRolRepository
{
    public RolRepository(AppDbContext context) : base(context) { }
}
