#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseDepartment;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseDepartmentEntityArgs : SearchArgs, IRequest<List<BaseDepartmentEntity>>
{
}