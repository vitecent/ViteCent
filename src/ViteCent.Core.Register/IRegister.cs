namespace ViteCent.Core.Register;

/// <summary>
/// 服务注册接口，定义了微服务注册、注销和服务发现的基本契约
/// </summary>
public interface IRegister
{
    /// <summary>
    /// 注销指定的微服务
    /// </summary>
    /// <param name="serviceId">要注销的服务实例ID</param>
    /// <returns>表示异步操作的任务</returns>
    Task DeregisterAsync(string serviceId);

    /// <summary>
    /// 注册新的微服务实例
    /// </summary>
    /// <param name="microService">微服务配置信息</param>
    /// <returns>表示异步操作的任务</returns>
    Task RegisterAsync(ServiceConfig microService);

    /// <summary>
    /// 获取当前已注册的所有服务列表
    /// </summary>
    /// <returns>返回服务名称与服务配置列表的字典映射</returns>
    Task<Dictionary<string, List<ServiceConfig>>> ServiceAsync();
}