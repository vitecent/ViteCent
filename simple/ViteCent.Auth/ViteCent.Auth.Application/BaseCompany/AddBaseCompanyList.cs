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
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// 新增公司信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class AddBaseCompanyList(ILogger<AddBaseCompanyList> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddBaseCompanyListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 新增公司信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddBaseCompanyListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseCompany.AddBaseCompanyList");

        user = httpContextAccessor.InitUser();

        var check = await AddBaseCompany.OverrideHandle(mediator, request, user);

        if (!check.Success)
            return check;

        var entitys = new AddBaseCompanyEntityListArgs()
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            var entity = mapper.Map<AddBaseCompanyEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "BaseCompany");

            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        return await mediator.Send(entitys, cancellationToken);
    }
}