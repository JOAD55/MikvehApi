using System;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context) { }
}
