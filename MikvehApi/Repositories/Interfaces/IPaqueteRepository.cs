using System;
using System.Formats.Asn1;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Repositories.Interfaces;

public interface IPaqueteRepository : IRepository<Paquete>
{
    Task AddDetalleAsync(DetallePaquete detallePaquete);
    Task DeleteDetalleAsync(int id);
    Task<DetallePaquete?> GetDetallaByIdAsync(int id);
}
