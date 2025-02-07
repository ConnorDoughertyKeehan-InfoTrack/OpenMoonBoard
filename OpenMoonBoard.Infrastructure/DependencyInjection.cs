using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Infrastructure.Contexts;
using OpenMoonBoard.Infrastructure.Repositories;
using System.Reflection;

namespace OpenMoonBoard.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddOpenMoonBoardApiInfrastructure(
        this IServiceCollection services)
    {
        string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        // Build the configuration
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(assemblyDirectory) // Set the base path for the configuration file
            .AddJsonFile("mbappsettings.json", optional: false, reloadOnChange: true) // Add appsettings.json
            .Build();

        // Register your DbContext
        services.AddDbContext<OpenMoonBoardContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("OpenMoonBoard")));

        //Register repos
        services.AddScoped<IBoardsRepository, BoardsRepository>();
        services.AddScoped<IGradesRepository, GradesRepository>();
        services.AddScoped<IMoonBoardHoldsRepository, MoonBoardHoldsRepository>();
        services.AddScoped<IMoonBoardRoutesRepository, MoonBoardRoutesRepository>();
        services.AddScoped<ISettersRepository, SettersRepository>();

        return services;
    }
}
