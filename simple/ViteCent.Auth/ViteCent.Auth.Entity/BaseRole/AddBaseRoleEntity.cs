#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseRole;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_role")]
public class AddBaseRoleEntity : BaseRoleEntity
{

}