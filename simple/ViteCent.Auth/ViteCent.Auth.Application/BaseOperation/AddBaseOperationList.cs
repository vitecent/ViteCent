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
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// 新增操作信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class AddBaseOperationList(ILogger<AddBaseOperationList> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddBaseOperationListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增操作信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseOperationListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseOperation.AddBaseOperationList");

        user = httpContextAccessor.InitUser();

        var check = await AddBaseOperation.OverrideHandle(mediator, request, user);

        if (!check.Success)
            return check;

        var entitys = new AddBaseOperationEntityListArgs()
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            if (string.IsNullOrWhiteSpace(companyId))
                companyId = item.CompanyId;

            var entity = mapper.Map<AddBaseOperationEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "BaseOperation");

            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        return await mediator.Send(entitys, cancellationToken);
    }
}