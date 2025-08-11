#region

// 引入Autofac依赖注入容器相关命名空间，提供IoC容器的核心功能
using Autofac;

#endregion

namespace ViteCent.Job.Api;

/// <summary>
/// 依赖注入配置类
/// </summary>
public class AutoFacConfig : Module
{
    /// <summary>
    /// 配置服务注册
    /// </summary>
    /// <param name="builder">容器构建器</param>
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
    }
}