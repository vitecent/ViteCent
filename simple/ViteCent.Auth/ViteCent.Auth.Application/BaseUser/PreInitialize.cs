#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
public class PreInitialize(ILogger<AddBaseUser> logger,
    IMediator mediator) : IRequestHandler<PreInitializeArgs, DataResult<PreInitializeResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<PreInitializeResult>> Handle(PreInitializeArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.PreInitialize");

        var input = new SearchBaseUserArgs()
        {
            Offset = 0,
            Limit = 1
        };

        var result = await mediator.Send(input, cancellationToken);

        if (result.Rows.Count == 0)
            return new DataResult<PreInitializeResult>(new PreInitializeResult()
            {
                Flag = true
            });
        else
            return new DataResult<PreInitializeResult>(new PreInitializeResult()
            {
                Flag = false
            });
    }
}