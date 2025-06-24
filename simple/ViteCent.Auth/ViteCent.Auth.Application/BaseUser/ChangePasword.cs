#region

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Domain.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 修改密码仓储
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public class ChangePasword(
    ILogger<ChangePasword> logger,
    IBaseCache cache,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<ChangePaswordArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(ChangePaswordArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.ChangePasword");

        user = httpContextAccessor.InitUser();

        request.OriginalPassword = $"{user.Name}{request.OriginalPassword}{BaseConst.Salf}".EncryptMD5();

        var args = new LoginEntityArgs
        {
            Username = user.Code,
            Password = request.OriginalPassword
        };

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new DataResult<LoginResult>(500, "用户名或密码错误");

        if (entity.Status == (int)StatusEnum.Disable)
            return new DataResult<LoginResult>(500, "用户已被禁用");

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