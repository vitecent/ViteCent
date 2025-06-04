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

// 引入补卡申请相关的数据参数
using ViteCent.Basic.Data.RepairSchedule;

// 引入补卡申请相关的模型
using ViteCent.Basic.Entity.RepairSchedule;

// 引入缓存接口
using ViteCent.Core.Cache;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Basic.Application.RepairSchedule;

/// <summary>
/// 批量新增补卡申请应用
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="companyInvoke"></param>
/// <param name="departmentInvoke"></param>
/// <param name="userInvoke"></param>
/// <param name="httpContextAccessor"></param>
public class AddRepairScheduleList(
    // 注入日志记录器
    ILogger<AddRepairScheduleList> logger,
    // 注入缓存接口
    IBaseCache cache,
    // 注入映射器接口
    IMapper mapper,
    // 注入中介者接口
    IMediator mediator,
    IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
    IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke,
    IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor)
    // 继承基类，指定查询参数和返回结果类型
    : IRequestHandler<AddRepairScheduleListArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 批量新增补卡申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddRepairScheduleListArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Application.RepairSchedule.AddRepairScheduleList");

        user = httpContextAccessor.InitUser();

        var check = await AddRepairSchedule.OverrideHandle(mediator, request, user, companyInvoke, departmentInvoke, userInvoke, cancellationToken);

        if (!check.Success)
            return check;

        var entitys = new AddRepairScheduleEntityListArgs()
        {
            Items = []
        };

        foreach (var item in request.Items)
        {
            var companyId = user?.Company?.Id ?? string.Empty;

            if (string.IsNullOrWhiteSpace(companyId))
                companyId = item.CompanyId;

            var entity = mapper.Map<AddRepairScheduleEntity>(item);

            entity.Id = await cache.GetIdAsync(companyId, "RepairSchedule");
            entity.Creator = user?.Name ?? string.Empty;
            entity.CreateTime = DateTime.Now;
            entity.DataVersion = DateTime.Now;

            entitys.Items.Add(entity);
        }

        var result = await mediator.Send(entitys, cancellationToken);

        foreach (var entity in entitys.Items)
            await AddRepairSchedule.OverrideTopic(mediator, TopicEnum.Add, entity, cancellationToken);

        return result;
    }
}