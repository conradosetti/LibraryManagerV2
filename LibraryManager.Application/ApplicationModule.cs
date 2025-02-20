using LibraryManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .addServices();
        return services;
    }

    private static IServiceCollection addServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoanService, LoansService>();
        return services;
    }
}