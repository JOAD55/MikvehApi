using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MikvehApi.Models;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(Trabajador trabajador, out DateTime expiresAt)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection["Key"]
            ?? throw new InvalidOperationException("No se ha configurado Jwt:Key.");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var expiresInMinutes = jwtSection.GetValue<int?>("ExpiresInMinutes") ?? 60;

        expiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, trabajador.TrabajadorId.ToString()),
            new(ClaimTypes.NameIdentifier, trabajador.TrabajadorId.ToString()),
            new(ClaimTypes.Name, trabajador.Usuario),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (trabajador.Rol is not null)
        {
            claims.Add(new Claim(ClaimTypes.Role, trabajador.Rol.Nombre));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
