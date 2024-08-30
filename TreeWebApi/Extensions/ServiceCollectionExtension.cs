using BusinessLayer;
using BusinessLayer.Mediatr;
using BusinessLayer.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TreeWebApi.Options;
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
    /// <returns></returns>

    public static IServiceCollection AddPostgresDb(this IServiceCollection services)
    {
        services.AddDbContext<TreeContext>((serviceProvider, dbContextOptionsBuilder) =>
            ConfigureDbContext(serviceProvider, dbContextOptionsBuilder)
        );

        services.AddDbContext<LogContext>((serviceProvider, dbContextOptionsBuilder) =>
            ConfigureDbContext(serviceProvider, dbContextOptionsBuilder)
        );

        return services;
    }
    private static void ConfigureDbContext(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        DatabaseOptions dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        dbContextOptionsBuilder.UseNpgsql(dbOptions.ConnectionString, sqlOptionsAction =>
        {
            sqlOptionsAction.CommandTimeout(dbOptions.CommandTimeout);
            sqlOptionsAction.EnableRetryOnFailure(dbOptions.MaxRetryCount);


            sqlOptionsAction.MigrationsAssembly("TreeWebApi");
        });

        dbContextOptionsBuilder.EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging);
        dbContextOptionsBuilder.EnableDetailedErrors(dbOptions.EnableDetailedErrors);
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
