/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入Autofac IoC容器的功能
using Autofac;

// 引入System.Reflection 程序集加载和类型操作的功能
using System.Reflection;

// 将Autofac.Module重命名为Module，避免与System.Reflection.Module的命名冲突
using Module = Autofac.Module;

#endregion 引入命名空间

namespace ViteCent.Auth.Api;

/// <summary>
/// AutoFac依赖注入配置类
/// </summary>
/// <remarks>
/// 该类继承自Autofac.Module，用于配置依赖注入容器。
///
/// 主要功能包括：
/// 1. 扫描并加载应用和领域的程序集
/// 2. 自动注册程序集中的类型到依赖注入容器
/// 3. 配置服务生命周期管理
///
/// 使用说明：
/// - 该类会自动扫描指定模式的程序集（*Application.dll和*Domain.dll）
/// - 将找到的非抽象类按照其实现的接口注册到容器中
/// - 所有服务默认使用InstancePerLifetimeScope生命周期
/// </remarks>
public partial class AutoFacConfig : Module
{
    /// <summary>
    /// 加载依赖注入配置
    /// </summary>
    /// <param name="builder">容器构建器，用于注册类型到IoC容器</param>
    /// <remarks>
    /// 该方法实现以下功能：
    /// 1. 获取应用程序基础目录
    /// 2. 扫描并加载指定模式的程序集
    /// 3. 自动注册符合条件的类型
    /// 4. 配置服务的生命周期范围
    ///
    /// 实现细节：
    /// - 扫描*Application.dll和*Domain.dll文件
    /// - 加载找到的程序集
    /// - 注册所有非抽象类
    /// - 配置InstancePerLifetimeScope生命周期
    /// </remarks>
    protected override void Load(ContainerBuilder builder)
    {
        // 调用基类的Load方法，确保基础配置得到正确初始化
        base.Load(builder);

        // 获取应用程序的基础目录路径，用于查找程序集文件
        var path = AppDomain.CurrentDomain.BaseDirectory;

        // 创建一个列表用于存储加载的程序集，后续用于批量注册
        var assemblies = new List<Assembly>();

        // 定义需要扫描的dll文件匹配模式
        // *Application.dll：应用程序集，包含应用服务、结构对象和业务逻辑实现
        // *Domain.dll：领域程序集，包含领域模型、领域服务和领域事件
        var dlls = new List<string>
        {
            "*Application.dll",
            "*Domain.dll"
        };

        // 遍历每个dll匹配模式，查找符合条件的程序集文件
        foreach (var dll in dlls)
        {
            // 根据匹配模式查找应用程序目录中的dll文件 Directory.GetFiles方法将返回所有匹配的文件完整路径
            var files = Directory.GetFiles(path, dll);

            // 遍历找到的每个dll文件，将其加载为程序集
            foreach (var item in files)
            {
                // 使用Assembly.LoadFrom加载dll文件到程序集 这里使用LoadFrom而不是Load，因为需要完整的文件路径
                var assembly = Assembly.LoadFrom(item);

                // 将加载的程序集添加到列表中，准备后续注册
                assemblies.Add(assembly);
            }
        }

        // 配置依赖注入容器，注册找到的类型
        builder.RegisterAssemblyTypes([.. assemblies]) // 使用收集的程序集列表，注册其中的所有类型
            .Where(t => t.IsClass && !t.IsAbstract) // 筛选条件：必须是具体类（非抽象类），确保可以实例化
            .AsImplementedInterfaces() // 将类型注册为其实现的所有接口，支持依赖注入的接口编程
            .InstancePerLifetimeScope(); // 生命周期配置：每个生命周期作用域创建一个新实例，适合大多数业务场景

        // 其他依赖注入配置
        OverrideLoad(builder);
    }
}