using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Mind.Business.Mapping;

public static class MapServiceExtensions
{
    public static void AddMapperService(this IServiceCollection services)
    {
        services.AddScoped(provider => new MapperConfiguration(mc =>
        {
            mc.AddProfile(new Map());
        }).CreateMapper());
    }
}
