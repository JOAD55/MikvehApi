using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class AuthService(ITrabajadorRepository trabajadorRepository, ITokenService tokenService, IMapper mapper) : IAuthService
{
    private readonly ITrabajadorRepository _trabajadorRepository = trabajadorRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper;

    public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
    {
        var trabajador = await _trabajadorRepository.GetByUserAsync(dto.Usuario.ToLower());
        if (trabajador is null) return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Contrasena, trabajador.ContrasenaHash)) return null;

        var token = _tokenService.GenerateToken(trabajador, out var expiresAt);

        return new TokenResponseDto
        {
            Token = token,
            ExpiraEn = expiresAt,
            Trabajador = _mapper.Map<TrabajadorDto>(trabajador)
        };
    }
}
