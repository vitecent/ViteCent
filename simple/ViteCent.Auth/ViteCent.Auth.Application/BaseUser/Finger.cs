#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 绑定指纹仓储
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public class Finger(
    ILogger<Finger> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<FingerArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 绑定指纹
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(FingerArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Finger");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await mediator.CheckDepartment(request.CompanyId, request.DepartmentId);

        if (!hasDepartment.Success)
            return hasDepartment;

        var positionId = user?.Position?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.PositionId))
            request.PositionId = positionId;

        var hasPosition = await mediator.CheckPosition(request.CompanyId, request.PositionId);

        if (!hasPosition.Success)
            return hasPosition;

        if (!cache.HasKey("RegisterFinger"))
            return new BaseResult(500, "请先录入指纹信息");

        var finger = cache.GetString<string>("RegisterFinger");

        if (string.IsNullOrWhiteSpace(finger))
            return new BaseResult(500, "指纹信息不存在");

        var getArgs = mapper.Map<GetBaseUserEntityArgs>(request);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "用户信息不存在");

        entity.Finger = finger;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseUser.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}