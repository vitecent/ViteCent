#region

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 退出登录仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="httpContextAccessor"></param>
public class Loginout(
    ILogger<Loginout> logger,
    IBaseCache cache,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<LoginoutArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(LoginoutArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Loginout");

        user = httpContextAccessor.InitUser();

        if (user != null)
        {
            cache.DeleteKey($"User{user.Id}");
            cache.DeleteKey($"UserInfo{user?.Id}");
        }

        return await Task.FromResult(new BaseResult());
    }
}