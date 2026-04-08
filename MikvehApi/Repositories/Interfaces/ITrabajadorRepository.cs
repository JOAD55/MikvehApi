using System;
using MikvehApi.Models;

namespace MikvehApi.Repositories.Interfaces;

public interface ITrabajadorRepository : IRepository<Trabajador>
{
    Task<Trabajador?> GetByUserAsync(string usuario);
    Task<bool> ExistAsync(string usuario);
}
