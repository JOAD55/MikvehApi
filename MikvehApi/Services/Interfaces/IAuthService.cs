using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto?> LoginAsync(LoginDto dto);
}
