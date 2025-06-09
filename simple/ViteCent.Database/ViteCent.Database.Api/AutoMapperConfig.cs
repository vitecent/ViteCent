/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入日志信息相关的数据参数
using ViteCent.Database.Data.BaseLogs;

// 引入日志信息相关的数据模型对象
using ViteCent.Database.Entity.BaseLogs;

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Database.Api;

/// <summary>
/// AutoMapper对象映射配置类
/// </summary>
/// <remarks>
/// 该类负责配置ViteCent.Database模块中所有需要的对象映射关系，主要功能包括：
/// 1. ViteCent.Database请求参数与模型参数之间的映射
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
        #region 日志信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseLogsArgs, AddBaseLogsEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseLogsArgs, SearchBaseLogsEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseLogsEntity, BaseLogsResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseLogsArgs, GetBaseLogsEntityArgs>();
        CreateMap<BaseLogsEntity, DeleteBaseLogsEntity>();

        #endregion 日志信息对象映射配置

        // 其他对象映射配置
        OverrideMap();
    }
}