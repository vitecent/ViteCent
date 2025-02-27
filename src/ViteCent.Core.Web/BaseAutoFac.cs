#region

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using MediatR;
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
            // 注册 MediatR 的核心服务
            configuration.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            configuration.RegisterModule(module);
        });
    }
}