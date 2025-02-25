using LibraryManager.Application.Books.Commands.Create;
using LibraryManager.Application.Services;
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

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoanService, LoanService>();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining <CreateBookCommand>());
        return services;
    }
}