#region

using MediatR;
using ViteCent.Auth.Data.BaseLogs;
using ViteCent.Core;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api;

/// <summary>
/// </summary>
public static class BaseApi
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="args"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task LogError(this IMediator mediator, AddBaseLogsArgs args, string message, CancellationToken cancellationToken)
    {
        var logger = new BaseLogger(typeof(BaseApi));

        args.Status = (int)YesNoEnum.No;
        args.Description = message;

        try
        {
            await mediator.Send(args, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="args"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task LogSuccess(this IMediator mediator, AddBaseLogsArgs args, CancellationToken cancellationToken)
    {
        var logger = new BaseLogger(typeof(BaseApi));

        args.Status = (int)YesNoEnum.Yes;

        try
        {
            await mediator.Send(args, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
    }
}