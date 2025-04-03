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
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseDepartment;

/// <summary>
/// 新增部门信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class AddBaseDepartmentList(ILogger<AddBaseDepartmentList> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddBaseDepartmentListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增部门信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseDepartmentListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseDepartment.AddBaseDepartmentList");

        user = httpContextAccessor.InitUser();

        var check = await AddBaseDepartment.OverrideHandle(mediator, request, user);

        if (!check.Success)
            return check;

        var entitys = new AddBaseDepartmentEntityListArgs()
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            if (string.IsNullOrWhiteSpace(companyId))
                companyId = item.CompanyId;

            var entity = mapper.Map<AddBaseDepartmentEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "BaseDepartment");

            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        return await mediator.Send(entitys, cancellationToken);
    }
}