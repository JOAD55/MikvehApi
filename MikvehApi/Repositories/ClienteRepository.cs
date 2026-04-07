using System;
using AutoMapper;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class ClienteRepository(AppDbContext context, IMapper mapper) : Repository<Cliente>(context, mapper), IClienteRepository
{
}
