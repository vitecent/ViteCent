#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseResource;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("base_resource")]
public class AddBaseResourceEntity : BaseResourceEntity
{

}