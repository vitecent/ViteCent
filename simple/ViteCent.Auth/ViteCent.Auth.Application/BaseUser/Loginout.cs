#region

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="httpContextAccessor"></param>
public class Loginout(ILogger<AddBaseUser> logger, IBaseCache cache, IHttpContextAccessor httpContextAccessor) : IRequestHandler<LoginoutArgs, BaseResult>
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

        InitUser(httpContextAccessor);

        if (user != null)
            cache.DeleteKey(user.Id);

        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    private void InitUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();
    }
}