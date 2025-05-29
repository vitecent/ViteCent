/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Basic.Data.BaseLogs;
using ViteCent.Basic.Entity.BaseLogs;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.BaseLogs;

/// <summary>
/// 编辑日志信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="operationInvoke"></param>
/// <param name="resourceInvoke"></param>
/// <param name="systemInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseLogs(
    ILogger<EditBaseLogs> logger,
    IMapper mapper,
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseOperationArgs, DataResult<BaseOperationResult>> operationInvoke,
    IBaseInvoke<GetBaseResourceArgs, DataResult<BaseResourceResult>> resourceInvoke,
    IBaseInvoke<GetBaseSystemArgs, DataResult<BaseSystemResult>> systemInvoke,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<EditBaseLogsArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑日志信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseLogsArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.BaseLogs.EditBaseLogs");

        user = httpContextAccessor.InitUser();

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var args = mapper.Map<GetBaseLogsEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "日志信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if (request.Args is not null)
            entity.Args = request.Args;

        entity.CompanyId = request.CompanyId;

        if (request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        entity.DepartmentId = request.DepartmentId;

        if (request.DepartmentName is not null)
            entity.DepartmentName = request.DepartmentName;

        if (request.Description is not null)
            entity.Description = request.Description;

        entity.OperationId = request.OperationId;

        entity.OperationName = request.OperationName;

        entity.ResourceId = request.ResourceId;

        if (request.ResourceName is not null)
            entity.ResourceName = request.ResourceName;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.SystemId = request.SystemId;

        if (request.SystemName is not null)
            entity.SystemName = request.SystemName;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseLogs.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}