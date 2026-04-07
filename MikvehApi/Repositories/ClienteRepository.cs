using System;
using AutoMapper;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class ClienteRepository(AppDbContext context) : Repository<Cliente>(context), IClienteRepository
{
}
