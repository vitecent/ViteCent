#region

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
public class DeleteBaseCompany(ILogger<DeleteBaseCompany> logger, IMapper mapper, IMediator mediator) : IRequestHandler<DeleteBaseCompanyArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseCompany.DeleteBaseCompany");

        var args = mapper.Map<DeleteBaseCompanyEntityArgs>(request);

        return await mediator.Send(args, cancellationToken);
    }
}