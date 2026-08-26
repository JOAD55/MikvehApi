using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto?> LoginAsync(LoginDto dto);
    Task<TrabajadorDto?> GetPerfilAsync(int trabajadorId);
    Task<bool> CambiarPasswordAsync(int trabajadorId, CambiarPasswordDto dto);
}
