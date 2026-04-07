using System;
using AutoMapper;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class DetallePaqueteRepository : Repository<DetallePaquete>, IDetallePaqueteRepository
{
    public DetallePaqueteRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}
