#region

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 初始化仓储
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
public class Initialize(
    ILogger<Initialize> logger,
    IMediator mediator,
    IMapper mapper) : IRequestHandler<InitializeArgs, BaseResult>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(InitializeArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Initialize");

        var args = mapper.Map<AddBaseUserArgs>(request);
        args.IsSuper = (int)YesNoEnum.Yes;

        var validator = new BaseUserValidator(true);
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
        {
            var message = string.Join(",", result.Errors.Select(x => x.ErrorMessage));

            return new BaseResult(500, message);
        }

        var chackArgs = new PreInitializeArgs();

        var check = await mediator.Send(chackArgs, cancellationToken);

        if (!check.Data.Flag)
            return new BaseResult(500, "系统已初始化");

        return await mediator.Send(args, cancellationToken);
    }
}