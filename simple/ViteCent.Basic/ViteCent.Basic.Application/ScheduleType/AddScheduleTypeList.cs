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
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// 批量新增基础排班仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="httpContextAccessor"></param>
public class AddScheduleTypeList(ILogger<AddScheduleTypeList> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddScheduleTypeListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 批量新增基础排班
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddScheduleTypeListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.ScheduleType.AddScheduleTypeList");

        user = httpContextAccessor.InitUser();

        var check = await AddScheduleType.OverrideHandle(request, user);

        if (!check.Success)
            return check;

        var entitys = new AddScheduleTypeEntityListArgs()
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            if (string.IsNullOrWhiteSpace(companyId))
                companyId = item.CompanyId;

            var entity = mapper.Map<AddScheduleTypeEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "ScheduleType");

            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        return await mediator.Send(entitys, cancellationToken);
    }
}