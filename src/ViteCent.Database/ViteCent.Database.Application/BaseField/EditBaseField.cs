/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 AutoMapper 用于对象映射
using AutoMapper;

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Asp.Net Core Mvc 核心功能
using Microsoft.AspNetCore.Http;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入公司相关的数据参数
using ViteCent.Auth.Data.BaseCompany;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入 Web 核心
using ViteCent.Core.Web;

// 引入表字段信息相关的数据参数
using ViteCent.Database.Data.BaseField;

// 引入表字段信息相关的数据模型
using ViteCent.Database.Entity.BaseField;

#endregion 引入命名空间

namespace ViteCent.Database.Application.BaseField;

/// <summary>
/// 编辑表字段信息应用
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="companyInvoke">公司信息访问对象</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public partial class EditBaseField(
    // 注入日志记录器
    ILogger<EditBaseField> logger,
    // 注入对象映射器
    IMapper mapper,
    // 注入中介者
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<EditBaseFieldArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 编辑表字段信息
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(EditBaseFieldArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Database.Application.BaseField.EditBaseField");

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var args = mapper.Map<GetBaseFieldEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "表字段信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        if (request.Abbreviation is not null)
            entity.Abbreviation = request.Abbreviation;

        if (request.Add.HasValue)
            entity.Add = request.Add.Value;

        if (request.AddSort.HasValue)
            entity.AddSort = request.AddSort.Value;

        if (request.AddWidth.HasValue)
            entity.AddWidth = request.AddWidth.Value;

        if (request.Code is not null)
            entity.Code = request.Code;

        if (request.Color is not null)
            entity.Color = request.Color;

        entity.CompanyId = request.CompanyId;

        if (request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        if (request.Description is not null)
            entity.Description = request.Description;

        if (request.Detail.HasValue)
            entity.Detail = request.Detail.Value;

        if (request.DetailSort.HasValue)
            entity.DetailSort = request.DetailSort.Value;

        if (request.DetailWidth.HasValue)
            entity.DetailWidth = request.DetailWidth.Value;

        if (request.Edit.HasValue)
            entity.Edit = request.Edit.Value;

        if (request.EditSort.HasValue)
            entity.EditSort = request.EditSort.Value;

        if (request.EditWidth.HasValue)
            entity.EditWidth = request.EditWidth.Value;

        if (request.Export.HasValue)
            entity.Export = request.Export.Value;

        if (request.ExportSort.HasValue)
            entity.ExportSort = request.ExportSort.Value;

        if (request.ExportWidth.HasValue)
            entity.ExportWidth = request.ExportWidth.Value;

        if (request.Identity.HasValue)
            entity.Identity = request.Identity.Value;

        if (request.Import.HasValue)
            entity.Import = request.Import.Value;

        if (request.ImportSort.HasValue)
            entity.ImportSort = request.ImportSort.Value;

        if (request.ImportWidth.HasValue)
            entity.ImportWidth = request.ImportWidth.Value;

        if (request.Index.HasValue)
            entity.Index = request.Index.Value;

        entity.Length = request.Length;

        if (request.List.HasValue)
            entity.List = request.List.Value;

        if (request.ListSort.HasValue)
            entity.ListSort = request.ListSort.Value;

        if (request.ListWidth.HasValue)
            entity.ListWidth = request.ListWidth.Value;

        entity.Name = request.Name;

        if (request.PrimaryKey.HasValue)
            entity.PrimaryKey = request.PrimaryKey.Value;

        if (request.Print.HasValue)
            entity.Print = request.Print.Value;

        if (request.PrintSort.HasValue)
            entity.PrintSort = request.PrintSort.Value;

        if (request.PrintWidth.HasValue)
            entity.PrintWidth = request.PrintWidth.Value;

        if (request.Sort.HasValue)
            entity.Sort = request.Sort.Value;

        if (request.SplitField.HasValue)
            entity.SplitField = request.SplitField.Value;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        if (request.Table.HasValue)
            entity.Table = request.Table.Value;

        if (request.TableSort.HasValue)
            entity.TableSort = request.TableSort.Value;

        if (request.TableWidth.HasValue)
            entity.TableWidth = request.TableWidth.Value;

        if (request.Type is not null)
            entity.Type = request.Type;

        if (request.Unique.HasValue)
            entity.Unique = request.Unique.Value;

        if (request.VersionField.HasValue)
            entity.VersionField = request.VersionField.Value;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.Version = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddBaseField.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}