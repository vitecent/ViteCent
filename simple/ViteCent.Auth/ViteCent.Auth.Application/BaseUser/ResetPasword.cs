#region

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 重置密码仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class ResetPasword(ILogger<ResetPasword> logger,
    IBaseCache cache,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<ResetPaswordArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(ResetPaswordArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.ResetPasword");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(companyId))
            companyId = request.CompanyId;

        var args = new GetBaseUserEntityArgs()
        {
            Id = request.UserId,
            CompanyId = companyId,
        };

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<LoginResult>(500, "用户不存在");

        request.Password = $"{entity.Username}{request.Password}{BaseConst.Salf}".EncryptMD5();

        entity.Password = request.Password;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        cache.DeleteKey($"User{user?.Id}");
        cache.DeleteKey($"UserInfo{user?.Id}");

        return result;
    }
}