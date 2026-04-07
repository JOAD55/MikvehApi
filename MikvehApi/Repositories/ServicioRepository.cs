using System;
using AutoMapper;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class ServicioRepository : Repository<Servicio>, IServicioRepository
{
    public ServicioRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}
