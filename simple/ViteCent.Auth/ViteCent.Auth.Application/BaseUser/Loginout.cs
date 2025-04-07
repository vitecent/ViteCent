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
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="httpContextAccessor"></param>
public class Loginout(ILogger<AddBaseUser> logger,
    IBaseCache cache,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<LoginoutArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(LoginoutArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Loginout");

        user = httpContextAccessor.InitUser();

        if (user != null)
            cache.DeleteKey($"User{user.Id}");

        return await Task.FromResult(new BaseResult());
    }
}