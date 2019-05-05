using EstoqueService.Data.Interfaces;
using EstoqueService.Services.Interfaces;
using EstoqueService.Services.Messages;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services)
    {
        services.AddTransient<IEstoqueApiService, EstoqueApiService>();
        // Add all other services here.
        return services;
    }
}