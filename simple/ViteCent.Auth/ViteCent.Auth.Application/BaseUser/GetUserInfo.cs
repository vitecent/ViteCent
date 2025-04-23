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
/// 获取用户信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="httpContextAccessor"></param>
public class GetUserInfo(
    ILogger<AddBaseUser> logger,
    IBaseCache cache,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetUserInfoArgs, DataResult<BaseUserInfo>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<BaseUserInfo>> Handle(GetUserInfoArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.GetUserInfo");

        user = httpContextAccessor.InitUser();

        var result = new DataResult<BaseUserInfo>(user);

        var authInfo = new List<BaseSystemInfo>();

        if (cache.HasKey($"UserInfo{user?.Id}"))
            authInfo = cache.GetString<List<BaseSystemInfo>>($"UserInfo{user?.Id}");

        result.Data.AuthInfo = authInfo;

        return await Task.FromResult(result);
    }
}