#region

using ViteCent.Core.Web;

#endregion

namespace ViteCent.Files.Api;

/// <summary>
/// 对象映射配置类
/// </summary>
/// <remarks>用于配置文件服务模块中的对象映射关系 继承自BaseMapperConfig基类，实现自动映射功能</remarks>
public class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 配置对象映射关系
    /// </summary>
    /// <remarks>在此方法中配置文件服务模块的DTO、实体等对象之间的映射关系 通过重写基类的Map方法来实现自定义映射配置</remarks>
    public override void Map()
    {
    }
}