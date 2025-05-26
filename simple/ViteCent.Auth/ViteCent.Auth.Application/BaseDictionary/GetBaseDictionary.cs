/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Auth.Entity.BaseDictionary;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseDictionary;

/// <summary>
/// 获取字典信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetBaseDictionary(
    ILogger<GetBaseDictionary> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetBaseDictionaryArgs, DataResult<BaseDictionaryResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取字典信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<BaseDictionaryResult>> Handle(GetBaseDictionaryArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseDictionary.GetBaseDictionary");

        user = httpContextAccessor.InitUser();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var args = mapper.Map<GetBaseDictionaryEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<BaseDictionaryResult>(500, "字典信息不存在");

        var dto = mapper.Map<BaseDictionaryResult>(entity);

        return new DataResult<BaseDictionaryResult>(dto);
    }
}