#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseDepartment;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_department")]
public class AddBaseDepartmentEntity : BaseDepartmentEntity
{

}