#region

using SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseDepartment;

/// <summary>
/// 新增部门信息数据参数
/// </summary>
[Serializable]
[SugarTable("base_department")]
public class AddBaseDepartmentEntity : BaseDepartmentEntity
{
}