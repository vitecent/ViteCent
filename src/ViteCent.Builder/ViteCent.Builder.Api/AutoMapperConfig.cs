/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Builder.Api;

/// <summary>
/// AutoMapper对象映射配置类
/// </summary>
/// <remarks>
/// 该类负责配置ViteCent.Builder模块中所有需要的对象映射关系，主要功能包括：
/// 1. ViteCent.Builder请求参数与模型参数之间的映射
/// 2. 继承自BaseMapperConfig基类，实现自动化的对象映射配置
/// </remarks>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 配置对象映射关系
    /// </summary>
    /// <remarks>在此方法中配置所有需要的对象映射规则</remarks>
    public override void Map()
    {
        // 其他对象映射配置
        OverrideMap();
    }
}