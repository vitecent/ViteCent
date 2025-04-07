#region

using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
    /// <param name="companyIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckCompany(this IMediator mediator, List<string> companyIds)
    {
        var searchCompanyArgs = new SearchBaseCompanyEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "Id",
                    Value = companyIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var companys = await mediator.Send(searchCompanyArgs);

        if (companys.Count == 0)
            return new BaseResult(500, $"公司{companyIds.FirstOrDefault()}不存在");

        var _companyIds = companys.Select(y => y.Id).ToList();
        var _companyId = companyIds.FirstOrDefault(x => !_companyIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_companyId))
            return new BaseResult(500, $"公司{_companyId}不存在");

        var company = companys.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (company != null)
            return new BaseResult(500, $"公司{company.Name}已经禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckCompany(this IMediator mediator, string companyId)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            return new BaseResult();

        var getCompanyArgs = new GetBaseCompanyEntityArgs
        {
            Id = companyId,
        };

        var company = await mediator.Send(getCompanyArgs);

        if (company == null)
            return new BaseResult(500, "公司不存在");

        if (company.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckDepartment(this IMediator mediator, List<string> companyIds, List<string> departmentIds)
    {
        var searchDepartmentArgs = new SearchBaseDepartmentEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = departmentIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var departments = await mediator.Send(searchDepartmentArgs);

        if (departments.Count == 0)
            return new BaseResult(500, $"部门{departmentIds.FirstOrDefault()}不存在");

        var _departmentIds = departments.Select(y => y.Id).ToList();
        var _departmentId = departmentIds.FirstOrDefault(x => !_departmentIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_departmentId))
            return new BaseResult(500, $"部门{_departmentId}不存在");

        var department = departments.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (department != null)
            return new BaseResult(500, $"部门{department.Name}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getDepartmentArgs = new GetBaseDepartmentEntityArgs
        {
            CompanyId = companyId,
            Id = departmentId,
        };

        var department = await mediator.Send(getDepartmentArgs);

        if (department == null)
            return new BaseResult(500, "部门不存在");

        if (department.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "部门已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="systemIds"></param>
    /// <param name="resourceIds"></param>
    /// <param name="operationIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckOperation(this IMediator mediator, List<string> companyIds, List<string> systemIds, List<string> resourceIds, List<string> operationIds)
    {
        var searchOperationArgs = new SearchBaseOperationEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "SystemId",
                    Value = systemIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "ResourceId",
                    Value = resourceIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = operationIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var operations = await mediator.Send(searchOperationArgs);

        if (operations.Count == 0)
            return new BaseResult(500, $"操作{operationIds.FirstOrDefault()}不存在");

        var _operationIds = operations.Select(y => y.Id).ToList();
        var _operationId = operationIds.FirstOrDefault(x => !_operationIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_operationId))
            return new BaseResult(500, $"操作{_operationId}不存在");

        var operation = operations.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (operation != null)
            return new BaseResult(500, $"操作{operation.Name}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getOperationArgs = new GetBaseOperationEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            ResourceId = resourceId,
            Id = operationId,
        };

        var operation = await mediator.Send(getOperationArgs);

        if (operation == null)
            return new BaseResult(500, "操作不存在");

        if (operation.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "操作已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="positionIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckPosition(this IMediator mediator, List<string> companyIds, List<string> positionIds)
    {
        var searchPositionArgs = new SearchBaseCompanyEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = positionIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var positions = await mediator.Send(searchPositionArgs);

        if (positions.Count == 0)
            return new BaseResult(500, $"职位{positionIds.FirstOrDefault()}不存在");

        var _positionIds = positions.Select(y => y.Id).ToList();
        var _positionId = positionIds.FirstOrDefault(x => !_positionIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_positionId))
            return new BaseResult(500, $"职位{_positionId}不存在");

        var position = positions.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (position != null)
            return new BaseResult(500, $"职位{position.Name}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getPositionArgs = new GetBasePositionEntityArgs
        {
            CompanyId = companyId,
            Id = positionId,
        };

        var position = await mediator.Send(getPositionArgs);

        if (position == null)
            return new BaseResult(500, "职位不存在");

        if (position.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "职位已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="systemIds"></param>
    /// <param name="resourceIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckResource(this IMediator mediator, List<string> companyIds, List<string> systemIds, List<string> resourceIds)
    {
        var searchResourceArgs = new SearchBaseResourceEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "SystemId",
                    Value = systemIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = resourceIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var resources = await mediator.Send(searchResourceArgs);

        if (resources.Count == 0)
            return new BaseResult(500, $"资源{resourceIds.FirstOrDefault()}不存在");

        var _resourceIds = resources.Select(y => y.Id).ToList();
        var _resourceId = resourceIds.FirstOrDefault(x => !_resourceIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_resourceId))
            return new BaseResult(500, $"资源{_resourceId}不存在");

        var resource = resources.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (resource != null)
            return new BaseResult(500, $"资源{resource.Name}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getResourceArgs = new GetBaseResourceEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            Id = resourceId,
        };

        var resource = await mediator.Send(getResourceArgs);

        if (resource == null)
            return new BaseResult(500, "资源不存在");

        if (resource.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "资源已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckRole(this IMediator mediator, List<string> companyIds, List<string> roleIds)
    {
        var searchRoleArgs = new SearchBaseRoleEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = roleIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var roles = await mediator.Send(searchRoleArgs);

        if (roles.Count == 0)
            return new BaseResult(500, $"角色{roleIds.FirstOrDefault()}不存在");

        var _roleIds = roles.Select(y => y.Id).ToList();
        var _roleId = roleIds.FirstOrDefault(x => !_roleIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_roleId))
            return new BaseResult(500, $"角色{_roleId}不存在");

        var role = roles.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (role != null)
            return new BaseResult(500, $"角色{role.Name}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getRoleArgs = new GetBaseRoleEntityArgs
        {
            CompanyId = companyId,
            Id = roleId,
        };

        var role = await mediator.Send(getRoleArgs);

        if (role == null)
            return new BaseResult(500, "角色不存在");

        if (role.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "角色已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="systemIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckSystem(this IMediator mediator, List<string> companyIds, List<string> systemIds)
    {
        var searchSystemArgs = new SearchBaseSystemEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = systemIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var systems = await mediator.Send(searchSystemArgs);

        if (systems.Count == 0)
            return new BaseResult(500, $"系统{systemIds.FirstOrDefault()}不存在");

        var _systemIds = systems.Select(y => y.Id).ToList();
        var _systemId = systemIds.FirstOrDefault(x => !_systemIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_systemId))
            return new BaseResult(500, $"系统{_systemId}不存在");

        var system = systems.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (system != null)
            return new BaseResult(500, $"系统{system.Name}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getSystemArgs = new GetBaseSystemEntityArgs
        {
            CompanyId = companyId,
            Id = systemId,
        };

        var system = await mediator.Send(getSystemArgs);

        if (system == null)
            return new BaseResult(500, "系统不存在");

        if (system.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "系统已禁用");

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <param name="userIds"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckUser(this IMediator mediator, List<string> companyIds, List<string> departmentIds, List<string> userIds)
    {
        var searchUserArgs = new SearchBaseUserEntityArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "DepartmentId",
                    Value = departmentIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = userIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var users = await mediator.Send(searchUserArgs);

        if (users.Count == 0)
            return new BaseResult(500, $"用户{userIds.FirstOrDefault()}不存在");

        var _userIds = users.Select(y => y.Id).ToList();
        var _userId = userIds.FirstOrDefault(x => !_userIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_userId))
            return new BaseResult(500, $"用户{_userId}不存在");

        var user = users.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (user != null)
            return new BaseResult(500, $"用户{user.RealName}已经禁用");

        return new BaseResult();
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
            return new BaseResult();

        var getUserArgs = new GetBaseUserEntityArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            Id = userId,
        };

        var user = await mediator.Send(getUserArgs);

        if (user == null)
            return new BaseResult(500, "用户不存在");

        if (user.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "用户已禁用");

        return new BaseResult();
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