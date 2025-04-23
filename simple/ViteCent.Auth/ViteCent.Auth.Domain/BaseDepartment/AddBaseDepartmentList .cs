/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDepartment;

/// <summary>
/// 批量新增部门信息
/// </summary>
/// <param name="logger"></param>
public class AddBaseDepartmentList(
    ILogger<AddBaseDepartmentList> logger)
    : BaseDomain<AddBaseDepartmentEntity>, IRequestHandler<AddBaseDepartmentEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 批量新增部门信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseDepartmentEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDepartment.AddBaseDepartmentList");

        return await base.AddAsync(request.Items);
    }
}