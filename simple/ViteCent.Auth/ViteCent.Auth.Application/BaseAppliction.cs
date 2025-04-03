#region

using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application;

/// <summary>
/// 基础仓储
/// </summary>
public static class BaseAppliction
{
    /// <summary>
    /// /
    /// </summary>
    /// <param name="user"></param>
    /// <param name="args"></param>
    public static void AddCompanyId(this SearchArgs args, BaseUserInfo user)
    {
        args.Args.RemoveAll(x => x.Field == "CompanyId");
        args.Args.Add(new SearchItem()
        {
            Field = "CompanyId",
            Value = user.Company.Id,
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckCompany(this IMediator mediator, string companyId)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            return new BaseResult(string.Empty);

        var hasCompanyArgs = new GetBaseCompanyEntityArgs
        {
            Id = companyId,
        };

        var hasCompany = await mediator.Send(hasCompanyArgs);

        if (hasCompany == null)
            return new BaseResult(500, "公司不存在");

        if (hasCompany.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckDepartment(this IMediator mediator, string companyId, string departmentId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(departmentId))
            return new BaseResult(string.Empty);

        var hasDepartmentArgs = new GetBaseDepartmentEntityArgs
        {
            CompanyId = companyId,
            Id = departmentId,
        };

        var hasDepartment = await mediator.Send(hasDepartmentArgs);

        if (hasDepartment == null)
            return new BaseResult(500, "部门不存在");

        if (hasDepartment.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "部门已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="systemId"></param>
    /// <param name="resourceId"></param>
    /// <param name="operationId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckOperation(this IMediator mediator, string companyId, string systemId, string resourceId, string operationId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(systemId) && !string.IsNullOrWhiteSpace(resourceId) && !string.IsNullOrWhiteSpace(operationId))
            return new BaseResult(string.Empty);

        var hasOperationArgs = new GetBaseOperationEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            ResourceId = resourceId,
            Id = operationId,
        };

        var hasOperation = await mediator.Send(hasOperationArgs);

        if (hasOperation == null)
            return new BaseResult(500, "操作不存在");

        if (hasOperation.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "操作已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="positionId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckPosition(this IMediator mediator, string companyId, string positionId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(positionId))
            return new BaseResult(string.Empty);

        var hasPositionArgs = new GetBasePositionEntityArgs
        {
            CompanyId = companyId,
            Id = positionId,
        };

        var hasPosition = await mediator.Send(hasPositionArgs);

        if (hasPosition == null)
            return new BaseResult(500, "职位不存在");

        if (hasPosition.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "职位已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="systemId"></param>
    /// <param name="resourceId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckResource(this IMediator mediator, string companyId, string systemId, string resourceId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(systemId) && !string.IsNullOrWhiteSpace(resourceId))
            return new BaseResult(string.Empty);

        var hasResourceArgs = new GetBaseResourceEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            Id = resourceId,
        };

        var hasResource = await mediator.Send(hasResourceArgs);

        if (hasResource == null)
            return new BaseResult(500, "资源不存在");

        if (hasResource.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "资源已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckRole(this IMediator mediator, string companyId, string roleId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(roleId))
            return new BaseResult(string.Empty);

        var hasRoleArgs = new GetBaseRoleEntityArgs
        {
            CompanyId = companyId,
            Id = roleId,
        };

        var hasRole = await mediator.Send(hasRoleArgs);

        if (hasRole == null)
            return new BaseResult(500, "角色不存在");

        if (hasRole.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "角色已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="systemId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckSystem(this IMediator mediator, string companyId, string systemId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(systemId))
            return new BaseResult(string.Empty);

        var hasSystemArgs = new GetBaseSystemEntityArgs
        {
            CompanyId = companyId,
            Id = systemId,
        };

        var hasSystem = await mediator.Send(hasSystemArgs);

        if (hasSystem == null)
            return new BaseResult(500, "系统不存在");

        if (hasSystem.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "系统已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckUser(this IMediator mediator, string companyId, string departmentId, string userId)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(departmentId) && !string.IsNullOrWhiteSpace(userId))
            return new BaseResult(string.Empty);

        var hasUserArgs = new GetBaseUserEntityArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            Id = userId,
        };

        var hasUser = await mediator.Send(hasUserArgs);

        if (hasUser == null)
            return new BaseResult(500, "用户不存在");

        if (hasUser.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "用户已禁用");

        return new BaseResult(string.Empty);
    }

    /// <summary>
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="companyId"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache, string companyId, string table)
    {
        return await cache.NextIdentity(new NextIdentifyArg()
        {
            CompanyId = companyId,
            Name = table,
        });
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public static BaseUserInfo InitUser(this IHttpContextAccessor httpContextAccessor)
    {
        var user = new BaseUserInfo();

        var context = httpContextAccessor.HttpContext;

        var token = context?.Request.Headers[Const.Token].ToString() ?? string.Empty;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        user.Token = token;

        return user;
    }
}