using ComidasTipicasDelSur.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using ComidasTipicasDelSur.Application.Interfaces;
using ComidasTipicasDelSur.Application.Services;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using ComidasTipicasDelSur.Infrastructure.Repositories;
using Oracle.ManagedDataAccess.Client;

// Configuración inicial del wallet
var walletPath = Path.Combine(AppContext.BaseDirectory, "Wallet");
OracleConfiguration.TnsAdmin = walletPath;
OracleConfiguration.WalletLocation = walletPath;
OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
OracleConfiguration.SqlNetEncryptionClient = "REQUIRED";
OracleConfiguration.SqlNetAuthenticationServices = "TCPS";

var builder = WebApplication.CreateBuilder(args);

// Configuración del logging para diagnóstico
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de servicios (mantén tus servicios actuales)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IDetalleFacturaRepository, DetalleFacturaRepository>();
builder.Services.AddScoped<IDetalleFacturaService, DetalleFacturaService>();

builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

builder.Services.AddScoped<IMesaRepository, MesaRepository>();
builder.Services.AddScoped<IMesaService, MesaService>();

builder.Services.AddScoped<IMeseroRepository, MeseroRepository>();
builder.Services.AddScoped<IMeseroService, MeseroService>();

builder.Services.AddScoped<ISupervisorRepository, SupervisorRepository>();
builder.Services.AddScoped<ISupervisorService, SupervisorService>();

builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<IReporteService, ReporteService>();

// Configuración de DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"),
    oracleOptions =>
    {
        oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
        oracleOptions.CommandTimeout(60); // Timeout de 60 segundos para comandos
        oracleOptions.MigrationsAssembly("ComidasTipicasDelSur.Infrastructure");
    });

    // Habilita logging detallado para diagnóstico
    options.EnableSensitiveDataLogging(false);
    options.EnableDetailedErrors();
    options.LogTo(Console.WriteLine, LogLevel.Information);
});
var app = builder.Build();

// Configuración del middleware
    app.UseSwagger();
    app.UseSwaggerUI();

app.MapGet("/", () => "API Comidas Típicas del Sur está funcionando");
app.UseAuthorization();
app.MapControllers();

// Verificación de conexión al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            Console.WriteLine("Conexión a la base de datos establecida con éxito!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
    }
}

app.Run();