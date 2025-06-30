# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todo y restaurar dependencias
COPY . ./
RUN dotnet restore

# Publicar en modo release
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Puerto expuesto por la app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Iniciar la app
ENTRYPOINT ["dotnet", "LAB5-LinGuzman.dll"]
