#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseOperation;

/// <summary>
/// 新增操作信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_operation")]
public class AddBaseOperationEntity : BaseOperationEntity
{
}