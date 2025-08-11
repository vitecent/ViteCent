#region

using Autofac;

#endregion

namespace ViteCent.Gateway.Api;

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