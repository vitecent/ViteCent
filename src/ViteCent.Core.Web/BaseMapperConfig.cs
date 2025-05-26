#region

using AutoMapper;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// AutoMapper对象映射配置基类
/// </summary>
/// <remarks>用于定义对象映射配置的抽象基类，继承自AutoMapper的Profile类。 派生类需要实现Map方法来定义具体的对象映射规则。</remarks>
public abstract class BaseMapperConfig : Profile
{
    /// <summary>
    /// 初始化对象映射配置
    /// </summary>
    /// <remarks>构造函数会自动调用Map方法来配置对象映射规则</remarks>
    public BaseMapperConfig()
    {
        Map();
    }

    /// <summary>
    /// 配置对象映射规则
    /// </summary>
    /// <remarks>派生类必须实现此方法来定义具体的对象映射配置 使用CreateMap方法来创建类型之间的映射关系</remarks>
    public abstract void Map();
}