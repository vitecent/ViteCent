#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("user_leave")]
public class AddUserLeaveEntity : UserLeaveEntity
{

}