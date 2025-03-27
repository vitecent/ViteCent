#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_user")]
public class AddBaseUserEntity : BaseUserEntity
{

}