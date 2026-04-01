using System;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class DetalleCitaRepository : Repository<DetalleCita>, IDetalleCitaRepository
{
    public DetalleCitaRepository(AppDbContext context) : base(context) { }
}
