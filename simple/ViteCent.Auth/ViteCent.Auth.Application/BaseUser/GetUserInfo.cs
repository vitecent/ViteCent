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
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
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
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
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