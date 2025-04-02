/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.RepairSchedule;

/// <summary>
/// 删除补卡申请仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class DeleteRepairSchedule(ILogger<DeleteRepairSchedule> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteRepairScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 删除补卡申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteRepairScheduleArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.RepairSchedule.DeleteRepairSchedule");

        InitUser(httpContextAccessor);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        var args = mapper.Map<DeleteRepairScheduleEntityArgs>(request);

        return await mediator.Send(args, cancellationToken);
    }

    /// <summary>
    /// 获取补卡申请用户信息
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