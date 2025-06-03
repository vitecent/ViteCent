/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Entity.BaseDepartment;

/// <summary>
/// 批量新增部门信息模型
/// </summary>
[Serializable]
public class AddBaseDepartmentEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public List<AddBaseDepartmentEntity> Items = [];
}