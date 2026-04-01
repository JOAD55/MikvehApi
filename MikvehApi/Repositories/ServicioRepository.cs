using System;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class ServicioRepository : Repository<Servicio>, IServicioRepository
{
    public ServicioRepository(AppDbContext context) : base(context) { }
}
