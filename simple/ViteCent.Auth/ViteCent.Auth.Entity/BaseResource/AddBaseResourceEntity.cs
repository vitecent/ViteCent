#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseResource;

/// <summary>
/// 新增资源信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_resource")]
public class AddBaseResourceEntity : BaseResourceEntity
{
}