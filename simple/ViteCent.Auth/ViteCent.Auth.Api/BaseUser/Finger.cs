#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Application;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 绑定指纹接口
/// </summary>
/// <param name="logger"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class Finger(
    ILogger<Finger> logger,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator) : BaseApi<FingerArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 绑定指纹
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Finger")]
    public override async Task<BaseResult> InvokeAsync(FingerArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Finger");

        var cancellationToken = new CancellationToken();
        var validator = new FingerValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new BaseResult(500, "公司标识不能为空");

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new BaseResult(500, "部门标识不能为空");

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.PositionId))
                return new BaseResult(500, "职位标识不能为空");

        return await mediator.Send(args, cancellationToken);
    }
}