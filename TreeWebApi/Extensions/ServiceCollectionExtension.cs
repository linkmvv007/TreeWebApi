using BusinessLayer;
using BusinessLayer.Mediatr;
using BusinessLayer.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
namespace TreeWebApi.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddMediator(this IServiceCollection services) =>
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
        typeof(CreateNodeCommandHandler).Assembly
        ));


    /// <summary>
    /// include postgres server:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>

    public static IServiceCollection AddPostgresDb(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TreeContext>(options =>
        options.UseNpgsql(connectionString, b => b.MigrationsAssembly("TreeWebApi")));

        services.AddDbContext<LogContext>(options =>
       options.UseNpgsql(connectionString, b => b.MigrationsAssembly("TreeWebApi")));

        return services;
    }

    public static IServiceCollection AddFluentValidationDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateNodeContextValidator>(lifetime: ServiceLifetime.Transient)
            .AddValidatorsFromAssemblyContaining<RenameNodeContextValidator>(lifetime: ServiceLifetime.Transient)
            .AddValidatorsFromAssemblyContaining<DeleteNodeContextValidator>(lifetime: ServiceLifetime.Transient)
            .AddValidatorsFromAssemblyContaining<PaginationContextValidator>(lifetime: ServiceLifetime.Transient);
    }
}
