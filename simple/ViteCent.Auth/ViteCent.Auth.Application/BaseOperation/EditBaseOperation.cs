#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// 编辑操作信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseOperation(ILogger<EditBaseOperation> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditBaseOperationArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑操作信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseOperationArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseOperation.EditBaseOperation");

        InitUser(httpContextAccessor);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
        {
            var hasCompanyArgs = new GetBaseCompanyEntityArgs
            {
                Id = request.CompanyId,
            };

            var hasCompany = await mediator.Send(hasCompanyArgs, cancellationToken);

            if (hasCompany == null)
                return new BaseResult(500, "公司不存在");

            if (hasCompany.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "公司已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.SystemId))
        {
            var hasSystemArgs = new GetBaseSystemEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.SystemId,
            };

            var hasSystem = await mediator.Send(hasSystemArgs, cancellationToken);

            if (hasSystem == null)
                return new BaseResult(500, "系统不存在");

            if (hasSystem.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "系统已禁用");
        }

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.SystemId) && !string.IsNullOrWhiteSpace(request.ResourceId))
        {
            var hasResourceArgs = new GetBaseResourceEntityArgs
            {
                CompanyId = request.CompanyId,
                SystemId = request.SystemId,
                Id = request.ResourceId,
            };

            var hasResource = await mediator.Send(hasResourceArgs, cancellationToken);

            if (hasResource == null)
                return new BaseResult(500, "资源不存在");

            if (hasResource.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "资源已禁用");
        }

        var preResult = await OverrideHandle(request, cancellationToken);

        if (!preResult.Success)
            return preResult;

        var args = mapper.Map<GetBaseOperationEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "数据不存在或无权限");

        var result = await OverrideHandle(entity, cancellationToken);

        if (!result.Success)
            return result;

        entity.Abbreviation = request.Abbreviation;
        entity.Code = request.Code;
        entity.Color = request.Color;
        entity.Description = request.Description;
        entity.Name = request.Name;
        entity.ResourceId = request.ResourceId;
        entity.Status = request.Status;
        entity.SystemId = request.SystemId;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }

    /// <summary>
    /// 获取操作信息用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    private void InitUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();
    }
}