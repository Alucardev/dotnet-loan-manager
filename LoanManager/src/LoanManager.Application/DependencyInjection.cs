using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using LoanManager.Application.Abstractions.Behaviors;
using LoanManager.Application.Clients.CreateClient;
using LoanManager.Application.Loans.CreateLoans;
using LoanManager.Application.Payments.CraeatePayment;
using LoanManager.Application.Plans.CreatePlans;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Payments;


namespace LoanManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    { 
        services.AddMediatR(configuration => 
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddTransient<IValidator<CreateClientCommand>, CreateClientCommandValidator>();
        services.AddTransient<IValidator<CreatePaymentCommand>, CreatePaymentCommandValidator>();
        services.AddTransient<IValidator<CreatePlanCommand>, CreatePlanCommandValidator>();
        services.AddTransient<IValidator<CreateLoanCommand>, CreateLoanCommandValidator>();

    //   services.AddValidatorsFromAssembly(typeof(CrearClienteCommandValidator).Assembly); NO FUNCIONA (VERSION)
         services.AddTransient<InstallmentCalculatorService>();
         services.AddTransient<PaymentSurchargeCalculator>(); 
         
        return services;
    }
}

