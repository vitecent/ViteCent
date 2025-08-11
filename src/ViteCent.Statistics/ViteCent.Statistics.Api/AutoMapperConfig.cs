#region

using ViteCent.Core.Web;
using ViteCent.Statistics.Data.Statistics;

#endregion

namespace ViteCent.Statistics.Api;

/// <summary>
/// AutoMapper对象映射配置类
/// </summary>
/// <remarks>
/// 该类负责配置统计模块中所有需要的对象映射关系，包括：
/// 1. 统计请求参数与实体参数之间的映射
/// 2. 继承自BaseMapperConfig基类，实现自动化的对象映射配置
/// </remarks>
public class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 配置对象映射关系
    /// </summary>
    /// <remarks>
    /// 在此方法中配置所有需要的对象映射规则：
    /// - StatisticsScheduleStatisticsArgs 到 StatisticsScheduleStatisticsEntityArgs 的映射，用于考勤统计参数转换
    /// </remarks>
    public override void Map()
    {
        CreateMap<StatisticsScheduleStatisticsArgs, StatisticsScheduleStatisticsEntityArgs>();
    }
}