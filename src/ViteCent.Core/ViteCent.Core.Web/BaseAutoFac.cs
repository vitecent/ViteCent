#region

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public static class BaseAutoFac
{
    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="module"></param>
    public static void UseAutoFac(this WebApplicationBuilder builder, IModule module)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>((context, configuration) =>
        {
            configuration.RegisterModule(module);
        });
    }
}