namespace ViteCent.Core.Register;

/// <summary>
/// 服务工具类，提供服务配置的相关操作方法
/// </summary>
public class BaseService
{
    /// <summary>
    /// 从服务配置列表中随机获取一个服务配置
    /// </summary>
    /// <param name="list">服务配置列表</param>
    /// <returns>随机选择的服务配置，如果列表为空则返回null</returns>
    public static ServiceConfig? GetServiceRandom(List<ServiceConfig> list)
    {
        if (list.Count == 0) return null;

        if (list.Count == 1) return list?.FirstOrDefault();

        var random = new Random();

        var index = random.Next(0, list.Count);

        return list[index];
    }
}