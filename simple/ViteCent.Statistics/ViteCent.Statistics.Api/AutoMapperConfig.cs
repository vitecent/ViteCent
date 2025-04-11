#region

using ViteCent.Core.Web;
using ViteCent.Statistics.Data.Statistics;

#endregion

namespace ViteCent.Statistics.Api;

/// <summary>
/// 用户角色映射
/// </summary>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 映射
    /// </summary>
    public override void Map()
    {
        CreateMap<StatisticsScheduleStatisticsArgs, StatisticsScheduleStatisticsEntityArgs>();
    }
}