using LibraryManager.Application.Books.Commands.Create;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            //.AddServices()
            .AddHandlers();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining <CreateBookCommand>());
        return services;
    }
}