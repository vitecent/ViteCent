#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseDepartment;

/// <summary>
/// 搜索部门信息数据参数
/// </summary>
[Serializable]
public class SearchBaseDepartmentEntityArgs : SearchArgs, IRequest<List<BaseDepartmentEntity>>
{
}