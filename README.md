# LoanManager

LoanManager es una solución para la gestión de préstamos, clientes, cuotas, pagos y métricas financieras. El proyecto está desarrollado en .NET y utiliza PostgreSQL como base de datos principal.

## Arquitectura

El proyecto sigue una arquitectura por capas:

- **Domain**: Entidades de negocio, lógica de dominio y contratos.
- **Application**: Casos de uso, validaciones, handlers y lógica de aplicación.
- **Infrastructure**: Implementaciones de acceso a datos, autenticación, y servicios externos.
- **Api**: Exposición de endpoints HTTP (REST), middlewares y configuración de la aplicación.

## Estructura del repositorio

```
LoanManager/
  src/
    LoanManager.Api/           # API REST principal
    LoanManager.Application/   # Lógica de aplicación y casos de uso
    LoanManager.Domain/        # Entidades y lógica de dominio
    LoanManager.Infrastructure/# Infraestructura y acceso a datos
  test/
    LoanManager.Application.UnitTests/    # Tests unitarios de Application
    LoanManager.Domain.UnitTests/         # Tests unitarios de Domain
    LoanManager.Infrastructure.UnitTests/ # Tests unitarios de Infrastructure
```

## Requisitos

- [.NET 7+ SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 13+](https://www.postgresql.org/download/)
- (Opcional) [Docker](https://www.docker.com/) para levantar la base de datos y servicios

## Configuración

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Alucardev/prestamos-backend.git
   cd prestamos-backend/LoanManager
   ```

2. Configura la base de datos:
   - Por defecto, la conexión está en `src/LoanManager.Api/appsettings.json`:
     ```json
     "ConnectionStrings": {
       "ConnectionString": "Host=localhost;port=5432;Database=dbprestamos;Username=postgres;Password=postgres"
     }
     ```
   - Puedes usar Docker para levantar PostgreSQL:
     ```bash
     docker-compose up -d
     ```

3. Aplica los scripts de base de datos y funciones necesarias (ver carpeta `/database/scripts` si existe).

4. Ejecuta las migraciones si corresponde.

## Ejecución

Desde la raíz del proyecto:

```bash
cd src/LoanManager.Api
 dotnet run
```

La API estará disponible en `http://localhost:5000` (o el puerto configurado).

## Autenticación

La API utiliza JWT. Puedes configurar el secreto, issuer y audience en `appsettings.json`.

## Endpoints principales

- `/api/clients` - Gestión de clientes
- `/api/loans` - Gestión de préstamos
- `/api/installments` - Gestión de cuotas
- `/api/payments` - Gestión de pagos
- `/api/metrics` - Métricas generales (requiere permisos y token JWT)

## Ejemplo de request a métricas

```bash
curl -X GET "http://localhost:5000/api/metrics?CurrencyType=ARS&PaymentMethod=1&FromDate=2024-01-01T00:00:00&ToDate=2024-12-31T23:59:59" \
  -H "accept: application/json" \
  -H "Authorization: Bearer <TU_TOKEN_JWT>"
```

## Tests

Para correr los tests unitarios:

```bash
cd test/LoanManager.Application.UnitTests
 dotnet test
```

Puedes correr todos los tests del repositorio con:
```bash
dotnet test
```

## Contribuciones

1. Haz un fork del repositorio
2. Crea una rama para tu feature/fix
3. Haz un PR describiendo tu cambio

## Licencia

MIT 