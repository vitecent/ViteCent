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
public static class BaseApplication
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseCompanyEntity>> CheckCompanys(this IMediator mediator,
        List<string> companyIds)
    {
        var searchCompanyArgs = new SearchBaseCompanyEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "Id",
                    Value = companyIds.ToJson(),
                    Method = SearchEnum.In
                }
            ]
        };

        var companys = await mediator.Send(searchCompanyArgs);

        if (companys.Count == 0)
            return new PageResult<BaseCompanyEntity>(500, $"公司{companyIds.FirstOrDefault()}不存在");

        var _companyIds = companys.Select(y => y.Id).ToList();
        var _companyId = companyIds.FirstOrDefault(x => !_companyIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_companyId))
            return new PageResult<BaseCompanyEntity>(500, $"公司{_companyId}不存在");

        var company = companys.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (company != null)
            return new PageResult<BaseCompanyEntity>(500, $"公司{company.Name}已经禁用");

        return new PageResult<BaseCompanyEntity>
        {
            Rows = companys
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseCompanyEntity>> CheckCompany(this IMediator mediator,
        string companyId)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            return new DataResult<BaseCompanyEntity>();

        var getCompanyArgs = new GetBaseCompanyEntityArgs
        {
            Id = companyId
        };

        var company = await mediator.Send(getCompanyArgs);

        if (company == null)
            return new DataResult<BaseCompanyEntity>(500, "公司不存在");

        if (company.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseCompanyEntity>(500, "公司已禁用");

        return new DataResult<BaseCompanyEntity>
        {
            Data = company
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseDepartmentEntity>> CheckDepartments(this IMediator mediator,
        List<string> companyIds,
        List<string> departmentIds)
    {
        var searchDepartmentArgs = new SearchBaseDepartmentEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchDepartmentArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (departmentIds.Count > 0)
            searchDepartmentArgs.AddArgs("Id", departmentIds.ToJson(), SearchEnum.In);

        var departments = await mediator.Send(searchDepartmentArgs);

        if (departments.Count == 0)
            return new PageResult<BaseDepartmentEntity>(500, $"部门{departmentIds.FirstOrDefault()}不存在");

        var _departmentIds = departments.Select(y => y.Id).ToList();
        var _departmentId = departmentIds.FirstOrDefault(x => !_departmentIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_departmentId))
            return new PageResult<BaseDepartmentEntity>(500, $"部门{_departmentId}不存在");

        var department = departments.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (department != null)
            return new PageResult<BaseDepartmentEntity>(500, $"部门{department.Name}已经禁用");

        return new PageResult<BaseDepartmentEntity>
        {
            Rows = departments
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseDepartmentEntity>> CheckDepartment(this IMediator mediator,
        string companyId,
        string departmentId)
    {
        if (string.IsNullOrWhiteSpace(departmentId))
            return new DataResult<BaseDepartmentEntity>();

        var getDepartmentArgs = new GetBaseDepartmentEntityArgs
        {
            CompanyId = companyId,
            Id = departmentId
        };

        var department = await mediator.Send(getDepartmentArgs);

        if (department == null)
            return new DataResult<BaseDepartmentEntity>(500, "部门不存在");

        if (department.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseDepartmentEntity>(500, "部门已禁用");

        return new DataResult<BaseDepartmentEntity>
        {
            Data = department
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="systemIds"></param>
    /// <param name="resourceIds"></param>
    /// <param name="operationIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseOperationEntity>> CheckOperations(this IMediator mediator,
        List<string> companyIds,
        List<string> systemIds,
        List<string> resourceIds,
        List<string> operationIds)
    {
        var searchOperationArgs = new SearchBaseOperationEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchOperationArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (systemIds.Count > 0)
            searchOperationArgs.AddArgs("SystemId", systemIds.ToJson(), SearchEnum.In);

        if (resourceIds.Count > 0)
            searchOperationArgs.AddArgs("ResourceId", resourceIds.ToJson(), SearchEnum.In);

        if (operationIds.Count > 0)
            searchOperationArgs.AddArgs("Id", operationIds.ToJson(), SearchEnum.In);

        var operations = await mediator.Send(searchOperationArgs);

        if (operations.Count == 0)
            return new PageResult<BaseOperationEntity>(500, $"操作{operationIds.FirstOrDefault()}不存在");

        var _operationIds = operations.Select(y => y.Id).ToList();
        var _operationId = operationIds.FirstOrDefault(x => !_operationIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_operationId))
            return new PageResult<BaseOperationEntity>(500, $"操作{_operationId}不存在");

        var operation = operations.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (operation != null)
            return new PageResult<BaseOperationEntity>(500, $"操作{operation.Name}已经禁用");

        return new PageResult<BaseOperationEntity>
        {
            Rows = operations
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="systemId"></param>
    /// <param name="resourceId"></param>
    /// <param name="operationId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseOperationEntity>> CheckOperation(this IMediator mediator,
        string companyId,
        string systemId,
        string resourceId,
        string operationId)
    {
        if (string.IsNullOrWhiteSpace(operationId))
            return new DataResult<BaseOperationEntity>();

        var getOperationArgs = new GetBaseOperationEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            ResourceId = resourceId,
            Id = operationId
        };

        var operation = await mediator.Send(getOperationArgs);

        if (operation == null)
            return new DataResult<BaseOperationEntity>(500, "操作不存在");

        if (operation.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseOperationEntity>(500, "操作已禁用");

        return new DataResult<BaseOperationEntity>
        {
            Data = operation
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="positionIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BasePositionEntity>> CheckPositions(this IMediator mediator,
        List<string> companyIds,
        List<string> positionIds)
    {
        var searchPositionArgs = new SearchBasePositionEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchPositionArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (positionIds.Count > 0)
            searchPositionArgs.AddArgs("Id", positionIds.ToJson(), SearchEnum.In);

        var positions = await mediator.Send(searchPositionArgs);

        if (positions.Count == 0)
            return new PageResult<BasePositionEntity>(500, $"职位{positionIds.FirstOrDefault()}不存在");

        var _positionIds = positions.Select(y => y.Id).ToList();
        var _positionId = positionIds.FirstOrDefault(x => !_positionIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_positionId))
            return new PageResult<BasePositionEntity>(500, $"职位{_positionId}不存在");

        var position = positions.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (position != null)
            return new PageResult<BasePositionEntity>(500, $"职位{position.Name}已经禁用");

        return new PageResult<BasePositionEntity>
        {
            Rows = positions
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="positionId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BasePositionEntity>> CheckPosition(this IMediator mediator,
        string companyId,
        string positionId)
    {
        if (string.IsNullOrWhiteSpace(positionId))
            return new DataResult<BasePositionEntity>();

        var getPositionArgs = new GetBasePositionEntityArgs
        {
            CompanyId = companyId,
            Id = positionId
        };

        var position = await mediator.Send(getPositionArgs);

        if (position == null)
            return new DataResult<BasePositionEntity>(500, "职位不存在");

        if (position.Status == (int)StatusEnum.Disable)
            return new DataResult<BasePositionEntity>(500, "职位已禁用");

        return new DataResult<BasePositionEntity>
        {
            Data = position
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="systemIds"></param>
    /// <param name="resourceIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseResourceEntity>> CheckResources(this IMediator mediator,
        List<string> companyIds,
        List<string> systemIds,
        List<string> resourceIds)
    {
        var searchResourceArgs = new SearchBaseResourceEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchResourceArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (systemIds.Count > 0)
            searchResourceArgs.AddArgs("SystemId", systemIds.ToJson(), SearchEnum.In);

        if (resourceIds.Count > 0)
            searchResourceArgs.AddArgs("Id", resourceIds.ToJson(), SearchEnum.In);

        var resources = await mediator.Send(searchResourceArgs);

        if (resources.Count == 0)
            return new PageResult<BaseResourceEntity>(500, $"资源{resourceIds.FirstOrDefault()}不存在");

        var _resourceIds = resources.Select(y => y.Id).ToList();
        var _resourceId = resourceIds.FirstOrDefault(x => !_resourceIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_resourceId))
            return new PageResult<BaseResourceEntity>(500, $"资源{_resourceId}不存在");

        var resource = resources.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (resource != null)
            return new PageResult<BaseResourceEntity>(500, $"资源{resource.Name}已经禁用");

        return new PageResult<BaseResourceEntity>
        {
            Rows = resources
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="systemId"></param>
    /// <param name="resourceId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseResourceEntity>> CheckResource(this IMediator mediator,
        string companyId,
        string systemId,
        string resourceId)
    {
        if (string.IsNullOrWhiteSpace(resourceId))
            return new DataResult<BaseResourceEntity>();

        var getResourceArgs = new GetBaseResourceEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            Id = resourceId
        };

        var resource = await mediator.Send(getResourceArgs);

        if (resource == null)
            return new DataResult<BaseResourceEntity>(500, "资源不存在");

        if (resource.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseResourceEntity>(500, "资源已禁用");

        return new DataResult<BaseResourceEntity>
        {
            Data = resource
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseRoleEntity>> CheckRoles(this IMediator mediator,
        List<string> companyIds,
        List<string> roleIds)
    {
        var searchRoleArgs = new SearchBaseRoleEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchRoleArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (roleIds.Count > 0)
            searchRoleArgs.AddArgs("Id", roleIds.ToJson(), SearchEnum.In);

        var roles = await mediator.Send(searchRoleArgs);

        if (roles.Count == 0)
            return new PageResult<BaseRoleEntity>(500, $"角色{roleIds.FirstOrDefault()}不存在");

        var _roleIds = roles.Select(y => y.Id).ToList();
        var _roleId = roleIds.FirstOrDefault(x => !_roleIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_roleId))
            return new PageResult<BaseRoleEntity>(500, $"角色{_roleId}不存在");

        var role = roles.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (role != null)
            return new PageResult<BaseRoleEntity>(500, $"角色{role.Name}已经禁用");

        return new PageResult<BaseRoleEntity>
        {
            Rows = roles
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseRoleEntity>> CheckRole(this IMediator mediator,
        string companyId,
        string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            return new DataResult<BaseRoleEntity>();

        var getRoleArgs = new GetBaseRoleEntityArgs
        {
            CompanyId = companyId,
            Id = roleId
        };

        var role = await mediator.Send(getRoleArgs);

        if (role == null)
            return new DataResult<BaseRoleEntity>(500, "角色不存在");

        if (role.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseRoleEntity>(500, "角色已禁用");

        return new DataResult<BaseRoleEntity>
        {
            Data = role
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="systemIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseSystemEntity>> CheckSystems(this IMediator mediator,
        List<string> companyIds,
        List<string> systemIds)
    {
        var searchSystemArgs = new SearchBaseSystemEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchSystemArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (systemIds.Count > 0)
            searchSystemArgs.AddArgs("Id", systemIds.ToJson(), SearchEnum.In);

        var systems = await mediator.Send(searchSystemArgs);

        if (systems.Count == 0)
            return new PageResult<BaseSystemEntity>(500, $"系统{systemIds.FirstOrDefault()}不存在");

        var _systemIds = systems.Select(y => y.Id).ToList();
        var _systemId = systemIds.FirstOrDefault(x => !_systemIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_systemId))
            return new PageResult<BaseSystemEntity>(500, $"系统{_systemId}不存在");

        var system = systems.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (system != null)
            return new PageResult<BaseSystemEntity>(500, $"系统{system.Name}已经禁用");

        return new PageResult<BaseSystemEntity>
        {
            Rows = systems
        };
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="systemId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseSystemEntity>> CheckSystem(this IMediator mediator,
        string companyId,
        string systemId)
    {
        if (string.IsNullOrWhiteSpace(systemId))
            return new DataResult<BaseSystemEntity>();

        var getSystemArgs = new GetBaseSystemEntityArgs
        {
            CompanyId = companyId,
            Id = systemId
        };

        var system = await mediator.Send(getSystemArgs);

        if (system == null)
            return new DataResult<BaseSystemEntity>(500, "系统不存在");

        if (system.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseSystemEntity>(500, "系统已禁用");

        return new DataResult<BaseSystemEntity>
        {
            Data = system
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <param name="userIds"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseUserEntity>> CheckUsers(this IMediator mediator,
        List<string> companyIds,
        List<string> departmentIds,
        List<string> userIds)
    {
        var searchUserArgs = new SearchBaseUserEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (userIds.Count == 0)
            searchUserArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (companyIds.Count == 0)
            searchUserArgs.AddArgs("DepartmentId", departmentIds.ToJson(), SearchEnum.In);

        if (departmentIds.Count == 0)
            searchUserArgs.AddArgs("Id", userIds.ToJson(), SearchEnum.In);

        var users = await mediator.Send(searchUserArgs);

        if (users.Count == 0)
            return new PageResult<BaseUserEntity>(500, $"用户{userIds.FirstOrDefault()}不存在");

        var _userIds = users.Select(y => y.Id).ToList();
        var _userId = userIds.FirstOrDefault(x => !_userIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_userId))
            return new PageResult<BaseUserEntity>(500, $"用户{_userId}不存在");

        var user = users.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (user != null)
            return new PageResult<BaseUserEntity>(500, $"用户{user.RealName}已经禁用");

        return new PageResult<BaseUserEntity>
        {
            Rows = users
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseUserEntity>> CheckUser(this IMediator mediator,
        string companyId,
        string departmentId,
        string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new DataResult<BaseUserEntity>();

        var getUserArgs = new GetBaseUserEntityArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            Id = userId
        };

        var user = await mediator.Send(getUserArgs);

        if (user == null)
            return new DataResult<BaseUserEntity>(500, "用户不存在");

        if (user.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseUserEntity>(500, "用户已禁用");

        return new DataResult<BaseUserEntity>
        {
            Data = user
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="companyId"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache,
        string companyId,
        string table)
    {
        return await cache.NextIdentity(new NextIdentifyArg
        {
            CompanyId = companyId,
            Name = table
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

        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        user.Token = token;

        return user;
    }
}