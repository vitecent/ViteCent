/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
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
/// 获取公司信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
public class GetBaseCompany(ILogger<GetBaseCompany> logger, IMapper mapper, IMediator mediator) : IRequestHandler<GetBaseCompanyArgs, DataResult<BaseCompanyResult>>
{
    /// <summary>
    /// 获取公司信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<BaseCompanyResult>> Handle(GetBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseCompany.GetBaseCompany");

        var args = mapper.Map<GetBaseCompanyEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<BaseCompanyResult>(500, "数据不存在或无权限");

        var dto = mapper.Map<BaseCompanyResult>(entity);

        return new DataResult<BaseCompanyResult>(dto);
    }
}