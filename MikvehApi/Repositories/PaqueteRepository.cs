using System;
using AutoMapper;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class PaqueteRepository : Repository<Paquete>, IPaqueteRepository
{
    public PaqueteRepository(AppDbContext context) : base(context) { }
}
