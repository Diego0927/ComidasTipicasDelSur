# 🍽️ Comidas Típicas del Sur - API REST (.NET 8)

Aplicación web desarrollada como parte de una prueba técnica para registrar y consultar las facturas de ventas realizadas en el restaurante **Comidas Típicas del Sur**. Se implementó siguiendo una arquitectura limpia y buenas prácticas modernas de desarrollo en .NET.

---

## 🔧 Tecnologías y herramientas utilizadas

| Herramienta                  | Versión                  |
|------------------------------|--------------------------|
| **.NET**                     | 8.0                      |
| **ASP.NET Core Web API**     | .NET 8.0                 |
| **Oracle Database**          | 21c Express Edition      |
| **Oracle SQL Developer**     | 24.3.1.347               |
| **Entity Framework Core**    | Oracle.EntityFrameworkCore 9.23.80 |
| **Visual Studio**            | 2022 Community Edition   |
| **Swagger (Swashbuckle)**    | Última versión compatible|

---

## 🧱 Arquitectura del proyecto (Clean Architecture)

El sistema sigue una estructura en capas desacoplada:

### 1. `ComidasTipicasDelSur.Domain`
- Define las **entidades del dominio** (`Cliente`, `Factura`, etc.).
- Contiene las **interfaces de repositorios** como abstracción de acceso a datos.

### 2. `ComidasTipicasDelSur.Application`
- Contiene los **servicios de aplicación**, que orquestan la lógica de negocio.
- Define las interfaces de los servicios.

### 3. `ComidasTipicasDelSur.Infrastructure`
- Implementa los **repositorios concretos**.
- Contiene el `AppDbContext.cs` de Entity Framework Core.
- Maneja la persistencia en Oracle DB.

### 4. `ComidasTipicasDelSur.API`
- Expone la funcionalidad como **API REST** mediante controladores.
- Usa los servicios definidos en la capa de aplicación.

### 5. `ComidasTipicasDelSur.Shared`
- Define los **DTOs** para transportar datos entre capas de forma segura.

### 6. `ComidasTipicasDelSur.Tests`
- Proyecto para **pruebas automatizadas** (unitarias/integración).

---

## 🧩 Patrones de diseño aplicados

- ✅ Repository Pattern
- ✅ Dependency Injection (DI)
- ✅ DTO Pattern
- ✅ Service Layer Pattern
- ✅ Controller Pattern (API)
- ✅ Clean Architecture (Onion Architecture)

---

## 📦 Requisitos para ejecución

1. Tener corriendo **Oracle Database 21c Express** localmente.
2. Ejecutar el script SQL `script_oracle.sql` para crear todas las tablas y relaciones.
3. Configurar la cadena de conexión en `appsettings.json`:

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "OracleConnection": "User Id=USUARIOPRUEBA;Password=welcome;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)))"
  }
}
