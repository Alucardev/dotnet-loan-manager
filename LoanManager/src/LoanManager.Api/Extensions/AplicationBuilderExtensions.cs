using Microsoft.EntityFrameworkCore;
using LoanManager.Api.Middleware;

namespace LoanManager.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {


        public static async Task ApplyMigration(this IApplicationBuilder app)
        {
            // Crear un nuevo scope para resolver servicios.
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider; // Obtener el proveedor de servicios.
                var loggerFactory = service.GetRequiredService<ILoggerFactory>(); // Obtener la fábrica de loggers.

                try
                {
                    // Obtener el contexto de la base de datos.
                    var context = service.GetRequiredService<ApplicationDbContext>();
                    // Aplicar migraciones pendientes de manera asincrónica.
                    await context.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, registrar el mensaje de error.
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Error en migracion"); // Loguear el error de migración.
                }
            }
        }

        // Métodos comentados que podrían implementarse más adelante

       
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
 
        public static IApplicationBuilder UseRequestContextLoging(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestContextLoggingMiddleware>();
            return app;
        }
        
    }
}
