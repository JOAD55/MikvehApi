using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MikvehApi.Middleware;

public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly IHostEnvironment _env = env;
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error no controlado procesando {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);

        var (statusCode, title) = exception switch
        {
            InvalidOperationException => (StatusCodes.Status409Conflict, "Conflicto con el estado actual del recurso"),
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "No tiene permisos para realizar esta accion"),
            _ => (StatusCodes.Status500InternalServerError, "Ocurrio un error inesperado")
        };

        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = _env.IsDevelopment() ? exception.Message : null,
            Instance = httpContext.Request.Path
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
