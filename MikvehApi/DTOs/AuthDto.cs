using System;
using System.ComponentModel.DataAnnotations;

namespace MikvehApi.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contrasena es obligatoria")]
    public string Contrasena { get; set; } = string.Empty;
}

public class TokenResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEn { get; set; }
    public TrabajadorDto Trabajador { get; set; } = null!;
}
