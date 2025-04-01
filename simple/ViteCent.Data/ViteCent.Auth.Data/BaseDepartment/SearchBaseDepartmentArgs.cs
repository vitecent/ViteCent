#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// 搜索部门信息参数
/// </summary>
[Serializable]
public class SearchBaseDepartmentArgs : SearchArgs, IRequest<PageResult<BaseDepartmentResult>>
{
}