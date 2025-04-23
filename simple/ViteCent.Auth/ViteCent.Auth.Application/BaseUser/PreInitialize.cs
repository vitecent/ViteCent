#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 预初始化仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
public class PreInitialize(
    ILogger<PreInitialize> logger,
    IMediator mediator) : IRequestHandler<PreInitializeArgs, DataResult<PreInitializeResult>>
{
    /// <summary>
    /// 预初始化
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<PreInitializeResult>> Handle(PreInitializeArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.PreInitialize");

        var input = new SearchBaseUserArgs
        {
            Offset = 1,
            Limit = 1
        };

        var result = await mediator.Send(input, cancellationToken);

        if (result.Rows.Count == 0)
            return new DataResult<PreInitializeResult>(new PreInitializeResult
            {
                Flag = true
            });
        return new DataResult<PreInitializeResult>(new PreInitializeResult
        {
            Flag = false
        });
    }
}