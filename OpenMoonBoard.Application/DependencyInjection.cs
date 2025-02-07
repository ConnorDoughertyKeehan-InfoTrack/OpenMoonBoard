using Microsoft.Extensions.DependencyInjection;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddOpenMoonBoardApiApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IMoonBoardClient, MoonBoardClient>();
        return services;
    }
}
