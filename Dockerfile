# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . . 
WORKDIR /app/ComidasTipicasDelSur.API
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar el proyecto publicado
COPY --from=build /out ./

# Copiar el wallet
COPY Wallet_comidasdb /opt/oracle/wallet

# Variables de entorno requeridas para usar el wallet
ENV TNS_ADMIN=/opt/oracle/wallet

# Configurar ASP.NET Core para escuchar en todas las interfaces
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80
ENTRYPOINT ["dotnet", "ComidasTipicasDelSur.API.dll"]
