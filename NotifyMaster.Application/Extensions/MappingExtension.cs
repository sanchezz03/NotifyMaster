using Microsoft.Extensions.DependencyInjection;
using NotifyMaster.Common.Helpers;
using System.Reflection;
using AutoMapper;

namespace NotifyMaster.Application.Extensions;

public static class MappingExtension
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var profiles = TypesHelper.GetTypesFromAllAssemblies<Profile>();
        var assemblies = new List<Assembly> { Assembly.GetCallingAssembly() };
        assemblies.AddRange(profiles.Select(x => x.Assembly).Distinct());
        return services.AddAutoMapper(assemblies.ToArray());
    }
}
