/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

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
/// 公司信息分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
public class PageBaseCompany(ILogger<PageBaseCompany> logger,
    IMapper mapper,
    IMediator mediator) : IRequestHandler<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>>
{
    /// <summary>
    /// 公司信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<BaseCompanyResult>> Handle(SearchBaseCompanyArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseCompany.PageBaseCompany");

        var args = mapper.Map<SearchBaseCompanyEntityArgs>(request);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<BaseCompanyResult>>(list);

        var result = new PageResult<BaseCompanyResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}