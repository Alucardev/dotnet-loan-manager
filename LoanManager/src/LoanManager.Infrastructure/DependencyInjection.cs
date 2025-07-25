using CleanArchitecture.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Clients;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Payments;
using LoanManager.Domain.Plans;
using LoanManager.Domain.Users;
using LoanManager.Infrastructure.Repositories;

namespace LoanManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
      this IServiceCollection services, 
      IConfiguration configuration
    )
    {
        //services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        var connectionString = configuration.GetConnectionString("ConnectionString")
        ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IPlanRepository, PlanRepository>();
 
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IInstallmentRespository, InstallmentRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddSingleton<ISqlConnectionFactory>( _ => {
            return new SqlConnectionFactory(connectionString);
        });

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}
