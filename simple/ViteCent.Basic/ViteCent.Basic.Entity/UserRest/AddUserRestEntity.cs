#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("user_rest")]
public class AddUserRestEntity : UserRestEntity
{
}