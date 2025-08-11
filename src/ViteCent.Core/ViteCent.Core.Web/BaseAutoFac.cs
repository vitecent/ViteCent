#region

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// Autofac依赖注入配置工具类，提供Web应用程序的Autofac服务配置功能
/// </summary>
public static class BaseAutoFac
{
    /// <summary>
    /// 配置Web应用程序使用Autofac作为依赖注入容器
    /// </summary>
    /// <param name="builder">Web应用程序构建器</param>
    /// <param name="module">Autofac模块，用于注册服务和依赖</param>
    /// <remarks>
    /// 该方法执行以下操作：
    /// 1. 设置Autofac作为依赖注入服务提供者
    /// 2. 注册MediatR中介者服务
    /// 3. 注册自定义Autofac模块
    /// </remarks>
    public static void UseAutoFac(this WebApplicationBuilder builder, IModule module)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>((context, configuration) =>
        {
            configuration.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            configuration.RegisterModule(module);
        });
    }
}