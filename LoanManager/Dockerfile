# Etapa base para la imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9003

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura las dependencias
COPY ["src/LoanManager.Domain/LoanManager.Domain.csproj", "src/LoanManager.Domain/"]
COPY ["src/LoanManager.Application/LoanManager.Application.csproj", "src/LoanManager.Application/"]
COPY ["src/LoanManager.Infrastructure/LoanManager.Infrastructure.csproj", "src/LoanManager.Infrastructure/"]
COPY ["src/LoanManager.Api/LoanManager.Api.csproj", "src/LoanManager.Api/"]
RUN dotnet restore "src/LoanManager.Api/LoanManager.Api.csproj"

# Copia el resto del código y construye el proyecto
COPY . .
WORKDIR "/src/src/LoanManager.Api"
RUN dotnet build "LoanManager.Api.csproj" -c Debug -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "LoanManager.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanManager.Api.dll"]


