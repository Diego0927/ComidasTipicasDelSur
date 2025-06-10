using ComidasTipicasDelSur.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using ComidasTipicasDelSur.Application.Interfaces;
using ComidasTipicasDelSur.Application.Services;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using ComidasTipicasDelSur.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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

// Configuración de Oracle EF Core (DEBE estar dentro de Main)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//app.MapGet("/", () => "API Facturación Restaurante Comidas Típicas del Sur en funcionamiento.");
app.Run();