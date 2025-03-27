#region

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
/// <param name="mapper"></param>
public class Initialize(ILogger<AddBaseUser> logger, IMediator mediator, IMapper mapper) : IRequestHandler<InitializeArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(InitializeArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Initialize");

        var args = mapper.Map<AddBaseUserArgs>(request);

        var validator = new BaseUserValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
        {
            var message = string.Join(",", result.Errors.Select(x => x.ErrorMessage));

            return new BaseResult(500, message);
        }

        args.Password = $"{args.Username}{args.Password}{Const.Salf}".EncryptMD5();
        args.Status = (int)StatusEnum.Enable;

        var chackArgs = new PreInitializeArgs();

        var check = await mediator.Send(chackArgs, cancellationToken);

        if (!check.Data.Flag)
            return new BaseResult(500, "ϵͳ�ѳ�ʼ��");

        return await mediator.Send(args, cancellationToken);
    }
}