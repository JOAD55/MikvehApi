using System;
using MikvehApi.Models;

namespace MikvehApi.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(Trabajador trabajador, out DateTime expiresAt);
}
