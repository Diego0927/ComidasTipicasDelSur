using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComidasTipicasDelSur.Infrastructure.Context;

[ApiController]
[Route("api/[controller]")]
public class HealthController(AppDbContext context, ILogger<HealthController> logger) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<HealthController> _logger = logger;

    [HttpGet("database")]
    public async Task<IActionResult> CheckDatabase()
    {
        try
        {
            // Intenta conectar a la base de datos
            var canConnect = await _context.Database.CanConnectAsync();

            if (canConnect)
            {
                // Obtiene información adicional de la conexión
                var connectionString = _context.Database.GetConnectionString();

                return Ok(new
                {
                    Status = "Connected",
                    Message = "Base de datos conectada correctamente",
                    ConnectionString = MaskConnectionString(connectionString),
                    Timestamp = DateTime.Now
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    Status = "Disconnected",
                    Message = "No se pudo conectar a la base de datos",
                    Timestamp = DateTime.Now
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar la conexión a la base de datos");

            return StatusCode(500, new
            {
                Status = "Error",
                ex.Message,
                Timestamp = DateTime.Now
            });
        }
    }

    [HttpGet("database/detailed")]
    public async Task<IActionResult> CheckDatabaseDetailed()
    {
        try
        {
            var result = new
            {
                CanConnect = await _context.Database.CanConnectAsync(),
                DatabaseName = _context.Database.GetDbConnection().Database,
                ConnectionState = _context.Database.GetDbConnection().State.ToString(),
                ProviderName = _context.Database.ProviderName,
                ConnectionString = MaskConnectionString(_context.Database.GetConnectionString()),
                ServerVersion = await GetServerVersion(),
                Timestamp = DateTime.Now
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar detalles de la conexión");
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    private async Task<string> GetServerVersion()
    {
        try
        {
            await _context.Database.OpenConnectionAsync();
            return _context.Database.GetDbConnection().ServerVersion;
        }
        catch
        {
            return "No disponible";
        }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }
    }

    private string MaskConnectionString(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            return "No disponible";

        // Oculta la contraseña por seguridad
        return System.Text.RegularExpressions.Regex.Replace(
            connectionString,
            @"Password=[^;]*",
            "Password=***",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );
    }
}