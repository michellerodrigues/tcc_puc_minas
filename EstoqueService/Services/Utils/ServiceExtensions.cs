using EstoqueService.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services)
    {
        services.AddTransient<IEstoqueRepository, EstoqueRepository>();
        // Add all other services here.
        return services;
    }
}