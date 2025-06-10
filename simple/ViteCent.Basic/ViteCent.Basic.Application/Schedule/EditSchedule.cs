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

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Http;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入公司相关的数据参数
using ViteCent.Auth.Data.BaseCompany;

// 引入部门相关的数据参数
using ViteCent.Auth.Data.BaseDepartment;

// 引入用户相关的数据参数
using ViteCent.Auth.Data.BaseUser;

// 引入排班信息相关的数据参数
using ViteCent.Basic.Data.Schedule;

// 引入排班信息相关的数据模型
using ViteCent.Basic.Entity.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 编辑排班信息应用
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditSchedule(
    // 注入日志记录器
    ILogger<EditSchedule> logger,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator,
    IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<EditScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 编辑排班信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditScheduleArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.EditSchedule");

        var check = await OverrideHandle(request, cancellationToken);

        if (!check.Success)
            return check;

        var args = mapper.Map<GetScheduleEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "排班信息不存在");

        check = await OverrideHandle(entity, cancellationToken);

        if (!check.Success)
            return check;

        entity.CompanyId = request.CompanyId;

        if (request.CompanyName is not null)
            entity.CompanyName = request.CompanyName;

        entity.DepartmentId = request.DepartmentId;

        if (request.DepartmentName is not null)
            entity.DepartmentName = request.DepartmentName;

        entity.PostId = request.PostId;

        if (request.PostName is not null)
            entity.PostName = request.PostName;

        entity.SceduleTimes = request.SceduleTimes;

        if (request.SignTimes is not null)
            entity.SignTimes = request.SignTimes;

        if (request.Status.HasValue)
            entity.Status = request.Status.Value;

        entity.Times = request.Times;

        entity.TypeId = request.TypeId;

        entity.TypeName = request.TypeName;

        entity.UserId = request.UserId;

        if (request.UserName is not null)
            entity.UserName = request.UserName;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        await AddSchedule.OverrideTopic(mediator, TopicEnum.Edit, entity, cancellationToken);

        return result;
    }
}