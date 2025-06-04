#region 引用命名空间

// 中介者模式接口，用于处理请求
using MediatR;

// HTTP上下文访问器，用于获取请求信息
using Microsoft.AspNetCore.Http;

// 身份认证相关
using System.Security.Claims;

// 基础实体类型
using ViteCent.Auth.Entity.BaseCompany;      // 公司实体
using ViteCent.Auth.Entity.BaseDepartment;   // 部门实体
using ViteCent.Auth.Entity.BaseOperation;    // 操作实体
using ViteCent.Auth.Entity.BasePosition;     // 职位实体
using ViteCent.Auth.Entity.BaseResource;     // 资源实体
using ViteCent.Auth.Entity.BaseRole;         // 角色实体
using ViteCent.Auth.Entity.BaseSystem;       // 系统实体
using ViteCent.Auth.Entity.BaseUser;         // 用户实体

// 核心功能
using ViteCent.Core;                         // 核心功能
using ViteCent.Core.Cache;                   // 缓存接口
using ViteCent.Core.Data;                    // 数据访问
using ViteCent.Core.Enums;                   // 枚举定义

#endregion

namespace ViteCent.Auth.Application;

/// <summary>
/// 基础应用服务类，提供公司、部门、用户、角色、系统、资源等基础数据的检查和验证功能
/// </summary>
/// <remarks>
/// 该静态类包含一系列扩展方法，用于：
/// 1. 检查各种基础实体的存在性和可用性
/// 2. 支持单个和批量检查操作
/// 3. 处理实体状态验证
/// 4. 提供标识生成和用户信息初始化功能
/// </remarks>
public static class BaseApplication
{
    /// <summary>
    /// 批量检查公司是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">要检查的公司标识列表</param>
    /// <returns>返回公司实体的分页结果，包含公司列表或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的公司是否存在
    /// 2. 公司的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<PageResult<BaseCompanyEntity>> CheckCompanys(this IMediator mediator,
        List<string> companyIds)
    {
        // 构造查询参数
        var searchCompanyArgs = new SearchBaseCompanyEntityArgs
        {
            Offset = 1,                    // 起始页码
            Limit = int.MaxValue,          // 不限制返回数量
            Args =                         // 查询条件
            [
                new SearchItem            // 查询条件项
                {
                    Field = "Id",         // 查询字段为标识
                    Value = companyIds.ToJson(), // 将标识列表转换为JSON字符串
                    Method = SearchEnum.In // 使用IN查询方式
                }
            ]
        };

        // 发送查询请求并等待结果
        var companys = await mediator.Send(searchCompanyArgs);

        // 检查是否找到任何公司
        if (companys.Count == 0)
            return new PageResult<BaseCompanyEntity>(500, $"公司{companyIds.FirstOrDefault()}不存在");

        // 获取查询结果中的公司标识列表
        var _companyIds = companys.Select(y => y.Id).ToList();
        // 查找是否有未找到的公司标识
        var _companyId = companyIds.FirstOrDefault(x => !_companyIds.Contains(x));

        // 如果存在未找到的公司标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_companyId))
            return new PageResult<BaseCompanyEntity>(500, $"公司{_companyId}不存在");

        // 检查是否有被禁用的公司
        var company = companys.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在被禁用的公司，返回错误信息
        if (company is not null)
            return new PageResult<BaseCompanyEntity>(500, $"公司{company.Name}已经禁用");

        // 返回查询结果
        return new PageResult<BaseCompanyEntity>
        {
            Rows = companys
        };
    }

    /// <summary>
    /// 检查单个公司是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">要检查的公司标识</param>
    /// <returns>返回公司实体的数据结果，包含公司信息或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的公司是否存在
    /// 2. 公司的状态是否正常（未禁用）
    /// </remarks>
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

        if (company is null)
            return new DataResult<BaseCompanyEntity>(500, "公司不存在");

        if (company.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseCompanyEntity>(500, "公司已禁用");

        return new DataResult<BaseCompanyEntity>
        {
            Data = company
        };
    }

    /// <summary>
    /// 批量检查部门是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="departmentIds">要检查的部门标识列表</param>
    /// <returns>返回部门实体的分页结果，包含部门列表或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的部门是否存在
    /// 2. 部门的状态是否正常（未禁用）
    /// </remarks>
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

        if (department is not null)
            return new PageResult<BaseDepartmentEntity>(500, $"部门{department.Name}已经禁用");

        return new PageResult<BaseDepartmentEntity>
        {
            Rows = departments
        };
    }

    /// <summary>
    /// 检查单个部门是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="departmentId">要检查的部门标识</param>
    /// <returns>返回部门实体的数据结果，包含部门信息或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的部门是否存在
    /// 2. 部门的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<DataResult<BaseDepartmentEntity>> CheckDepartment(this IMediator mediator,
        string companyId,
        string departmentId)
    {
        // 验证部门标识是否为空
        if (string.IsNullOrWhiteSpace(departmentId))
            return new DataResult<BaseDepartmentEntity>();

        // 构造获取部门实体的参数
        var getDepartmentArgs = new GetBaseDepartmentEntityArgs
        {
            CompanyId = companyId,
            Id = departmentId
        };

        // 通过中介者模式获取部门信息
        var department = await mediator.Send(getDepartmentArgs);

        // 检查部门是否存在
        if (department is null)
            return new DataResult<BaseDepartmentEntity>(500, "部门不存在");

        // 检查部门状态是否正常
        if (department.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseDepartmentEntity>(500, "部门已禁用");

        // 返回部门信息
        return new DataResult<BaseDepartmentEntity>
        {
            Data = department
        };
    }

    /// <summary>
    /// 批量检查操作权限是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="systemIds">系统标识列表</param>
    /// <param name="resourceIds">资源标识列表</param>
    /// <param name="operationIds">要检查的操作标识列表</param>
    /// <returns>返回操作实体的分页结果，包含操作列表或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的操作权限是否存在
    /// 2. 操作权限的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<PageResult<BaseOperationEntity>> CheckOperations(this IMediator mediator,
        List<string> companyIds,
        List<string> systemIds,
        List<string> resourceIds,
        List<string> operationIds)
    {
        // 构造查询操作权限的参数对象
        var searchOperationArgs = new SearchBaseOperationEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchOperationArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 添加系统标识筛选条件
        if (systemIds.Count > 0)
            searchOperationArgs.AddArgs("SystemId", systemIds.ToJson(), SearchEnum.In);

        // 添加资源标识筛选条件
        if (resourceIds.Count > 0)
            searchOperationArgs.AddArgs("ResourceId", resourceIds.ToJson(), SearchEnum.In);

        // 添加操作标识筛选条件
        if (operationIds.Count > 0)
            searchOperationArgs.AddArgs("Id", operationIds.ToJson(), SearchEnum.In);

        // 通过中介者模式查询操作权限列表
        var operations = await mediator.Send(searchOperationArgs);

        // 检查是否存在匹配的操作权限
        if (operations.Count == 0)
            return new PageResult<BaseOperationEntity>(500, $"操作{operationIds.FirstOrDefault()}不存在");

        // 获取查询结果中的操作标识列表
        var _operationIds = operations.Select(y => y.Id).ToList();
        // 查找是否有未找到的操作标识
        var _operationId = operationIds.FirstOrDefault(x => !_operationIds.Contains(x));

        // 如果存在未找到的操作标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_operationId))
            return new PageResult<BaseOperationEntity>(500, $"操作{_operationId}不存在");

        // 检查是否存在已禁用的操作
        var operation = operations.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在已禁用的操作，返回错误信息
        if (operation is not null)
            return new PageResult<BaseOperationEntity>(500, $"操作{operation.Name}已经禁用");

        // 返回操作权限列表
        return new PageResult<BaseOperationEntity>
        {
            Rows = operations
        };
    }

    /// <summary>
    /// 检查单个操作权限是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="systemId">系统标识</param>
    /// <param name="resourceId">资源标识</param>
    /// <param name="operationId">要检查的操作标识</param>
    /// <returns>返回操作实体的数据结果，包含操作信息或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的操作权限是否存在
    /// 2. 操作权限的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<DataResult<BaseOperationEntity>> CheckOperation(this IMediator mediator,
        string companyId,
        string systemId,
        string resourceId,
        string operationId)
    {
        // 验证操作标识是否为空
        if (string.IsNullOrWhiteSpace(operationId))
            return new DataResult<BaseOperationEntity>();

        // 构造获取操作权限实体的参数
        var getOperationArgs = new GetBaseOperationEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            ResourceId = resourceId,
            Id = operationId
        };

        // 通过中介者模式获取操作权限信息
        var operation = await mediator.Send(getOperationArgs);

        // 检查操作权限是否存在
        if (operation is null)
            return new DataResult<BaseOperationEntity>(500, "操作不存在");

        // 检查操作权限状态是否正常
        if (operation.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseOperationEntity>(500, "操作已禁用");

        // 返回操作权限信息
        return new DataResult<BaseOperationEntity>
        {
            Data = operation
        };
    }

    /// <summary>
    /// 批量检查职位是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="positionIds">要检查的职位标识列表</param>
    /// <returns>返回职位实体的分页结果，包含职位列表或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的职位是否存在
    /// 2. 职位的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<PageResult<BasePositionEntity>> CheckPositions(this IMediator mediator,
        List<string> companyIds,
        List<string> positionIds)
    {
        // 构造查询职位的参数对象
        var searchPositionArgs = new SearchBasePositionEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchPositionArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 添加职位标识筛选条件
        if (positionIds.Count > 0)
            searchPositionArgs.AddArgs("Id", positionIds.ToJson(), SearchEnum.In);

        // 通过中介者模式查询职位列表
        var positions = await mediator.Send(searchPositionArgs);

        // 检查是否存在匹配的职位
        if (positions.Count == 0)
            return new PageResult<BasePositionEntity>(500, $"职位{positionIds.FirstOrDefault()}不存在");

        // 获取查询结果中的职位标识列表
        var _positionIds = positions.Select(y => y.Id).ToList();
        // 查找是否有未找到的职位标识
        var _positionId = positionIds.FirstOrDefault(x => !_positionIds.Contains(x));

        // 如果存在未找到的职位标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_positionId))
            return new PageResult<BasePositionEntity>(500, $"职位{_positionId}不存在");

        // 检查是否存在已禁用的职位
        var position = positions.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在已禁用的职位，返回错误信息
        if (position is not null)
            return new PageResult<BasePositionEntity>(500, $"职位{position.Name}已经禁用");

        // 返回职位列表
        return new PageResult<BasePositionEntity>
        {
            Rows = positions
        };
    }

    /// <summary>
    /// 检查单个职位是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="positionId">要检查的职位标识</param>
    /// <returns>返回职位实体的数据结果，包含职位信息或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的职位是否存在
    /// 2. 职位的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<DataResult<BasePositionEntity>> CheckPosition(this IMediator mediator,
        string companyId,
        string positionId)
    {
        // 验证职位标识是否为空
        if (string.IsNullOrWhiteSpace(positionId))
            return new DataResult<BasePositionEntity>();

        // 构造获取职位实体的参数
        var getPositionArgs = new GetBasePositionEntityArgs
        {
            CompanyId = companyId,
            Id = positionId
        };

        // 通过中介者模式获取职位信息
        var position = await mediator.Send(getPositionArgs);

        // 检查职位是否存在
        if (position is null)
            return new DataResult<BasePositionEntity>(500, "职位不存在");

        // 检查职位状态是否正常
        if (position.Status == (int)StatusEnum.Disable)
            return new DataResult<BasePositionEntity>(500, "职位已禁用");

        // 返回职位信息
        return new DataResult<BasePositionEntity>
        {
            Data = position
        };
    }

    /// <summary>
    /// 批量检查资源是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="systemIds">系统标识列表</param>
    /// <param name="resourceIds">要检查的资源标识列表</param>
    /// <returns>返回资源实体的分页结果，包含资源列表或错误消息</returns>
    /// <remarks>
    /// 该方法会检查：
    /// 1. 指定的资源是否存在
    /// 2. 资源的状态是否正常（未禁用）
    /// </remarks>
    public static async Task<PageResult<BaseResourceEntity>> CheckResources(this IMediator mediator,
        List<string> companyIds,
        List<string> systemIds,
        List<string> resourceIds)
    {
        // 构造查询资源的参数对象
        var searchResourceArgs = new SearchBaseResourceEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchResourceArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 添加系统标识筛选条件
        if (systemIds.Count > 0)
            searchResourceArgs.AddArgs("SystemId", systemIds.ToJson(), SearchEnum.In);

        // 添加资源标识筛选条件
        if (resourceIds.Count > 0)
            searchResourceArgs.AddArgs("Id", resourceIds.ToJson(), SearchEnum.In);

        // 通过中介者模式查询资源列表
        var resources = await mediator.Send(searchResourceArgs);

        // 检查是否存在匹配的资源
        if (resources.Count == 0)
            return new PageResult<BaseResourceEntity>(500, $"资源{resourceIds.FirstOrDefault()}不存在");

        // 获取查询结果中的资源标识列表
        var _resourceIds = resources.Select(y => y.Id).ToList();
        // 查找是否有未找到的资源标识
        var _resourceId = resourceIds.FirstOrDefault(x => !_resourceIds.Contains(x));

        // 如果存在未找到的资源标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_resourceId))
            return new PageResult<BaseResourceEntity>(500, $"资源{_resourceId}不存在");

        // 检查是否存在已禁用的资源
        var resource = resources.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在已禁用的资源，返回错误信息
        if (resource is not null)
            return new PageResult<BaseResourceEntity>(500, $"资源{resource.Name}已经禁用");

        // 返回资源列表
        return new PageResult<BaseResourceEntity>
        {
            Rows = resources
        };
    }

    /// <summary>
    /// 检查资源是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="systemId">系统标识</param>
    /// <param name="resourceId">资源标识</param>
    /// <returns>返回资源实体的数据结果，包含资源信息或错误消息</returns>
    public static async Task<DataResult<BaseResourceEntity>> CheckResource(this IMediator mediator,
        string companyId,
        string systemId,
        string resourceId)
    {
        // 验证资源标识是否为空
        if (string.IsNullOrWhiteSpace(resourceId))
            return new DataResult<BaseResourceEntity>();

        // 构造获取资源实体的参数
        var getResourceArgs = new GetBaseResourceEntityArgs
        {
            CompanyId = companyId,
            SystemId = systemId,
            Id = resourceId
        };

        // 通过中介者模式获取资源信息
        var resource = await mediator.Send(getResourceArgs);

        // 检查资源是否存在
        if (resource is null)
            return new DataResult<BaseResourceEntity>(500, "资源不存在");

        // 检查资源状态是否正常
        if (resource.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseResourceEntity>(500, "资源已禁用");

        // 返回资源信息
        return new DataResult<BaseResourceEntity>
        {
            Data = resource
        };
    }

    /// <summary>
    /// 批量检查角色是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="roleIds">角色标识列表</param>
    /// <returns>返回角色实体的分页结果，包含角色列表或错误消息</returns>
    public static async Task<PageResult<BaseRoleEntity>> CheckRoles(this IMediator mediator,
        List<string> companyIds,
        List<string> roleIds)
    {
        // 构造查询角色的参数对象
        var searchRoleArgs = new SearchBaseRoleEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchRoleArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 添加角色标识筛选条件
        if (roleIds.Count > 0)
            searchRoleArgs.AddArgs("Id", roleIds.ToJson(), SearchEnum.In);

        // 通过中介者模式查询角色列表
        var roles = await mediator.Send(searchRoleArgs);

        // 检查是否存在匹配的角色
        if (roles.Count == 0)
            return new PageResult<BaseRoleEntity>(500, $"角色{roleIds.FirstOrDefault()}不存在");

        // 获取查询结果中的角色标识列表
        var _roleIds = roles.Select(y => y.Id).ToList();
        // 查找是否有未找到的角色标识
        var _roleId = roleIds.FirstOrDefault(x => !_roleIds.Contains(x));

        // 如果存在未找到的角色标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_roleId))
            return new PageResult<BaseRoleEntity>(500, $"角色{_roleId}不存在");

        // 检查是否存在已禁用的角色
        var role = roles.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在已禁用的角色，返回错误信息
        if (role is not null)
            return new PageResult<BaseRoleEntity>(500, $"角色{role.Name}已经禁用");

        // 返回角色列表
        return new PageResult<BaseRoleEntity>
        {
            Rows = roles
        };
    }

    /// <summary>
    /// 检查单个角色是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="roleId">角色标识</param>
    /// <returns>返回角色实体的数据结果，包含角色信息或错误消息</returns>
    public static async Task<DataResult<BaseRoleEntity>> CheckRole(this IMediator mediator,
        string companyId,
        string roleId)
    {
        // 验证角色标识是否为空
        if (string.IsNullOrWhiteSpace(roleId))
            return new DataResult<BaseRoleEntity>();

        // 构造获取角色实体的参数
        var getRoleArgs = new GetBaseRoleEntityArgs
        {
            CompanyId = companyId,
            Id = roleId
        };

        // 通过中介者模式获取角色信息
        var role = await mediator.Send(getRoleArgs);

        // 检查角色是否存在
        if (role is null)
            return new DataResult<BaseRoleEntity>(500, "角色不存在");

        // 检查角色状态是否正常
        if (role.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseRoleEntity>(500, "角色已禁用");

        // 返回角色信息
        return new DataResult<BaseRoleEntity>
        {
            Data = role
        };
    }

    /// <summary>
    /// 批量检查系统是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="systemIds">系统标识列表</param>
    /// <returns>返回系统实体的分页结果，包含系统列表或错误消息</returns>
    public static async Task<PageResult<BaseSystemEntity>> CheckSystems(this IMediator mediator,
        List<string> companyIds,
        List<string> systemIds)
    {
        // 构造查询系统的参数对象
        var searchSystemArgs = new SearchBaseSystemEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchSystemArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 添加系统标识筛选条件
        if (systemIds.Count > 0)
            searchSystemArgs.AddArgs("Id", systemIds.ToJson(), SearchEnum.In);

        // 通过中介者模式查询系统列表
        var systems = await mediator.Send(searchSystemArgs);

        // 检查是否存在匹配的系统
        if (systems.Count == 0)
            return new PageResult<BaseSystemEntity>(500, $"系统{systemIds.FirstOrDefault()}不存在");

        // 获取查询结果中的系统标识列表
        var _systemIds = systems.Select(y => y.Id).ToList();
        // 查找是否有未找到的系统标识
        var _systemId = systemIds.FirstOrDefault(x => !_systemIds.Contains(x));

        // 如果存在未找到的系统标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_systemId))
            return new PageResult<BaseSystemEntity>(500, $"系统{_systemId}不存在");

        // 检查是否存在已禁用的系统
        var system = systems.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在已禁用的系统，返回错误信息
        if (system is not null)
            return new PageResult<BaseSystemEntity>(500, $"系统{system.Name}已经禁用");

        // 返回系统列表
        return new PageResult<BaseSystemEntity>
        {
            Rows = systems
        };
    }

    /// <summary>
    /// 检查单个系统是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="systemId">系统标识</param>
    /// <returns>返回系统实体的数据结果，包含系统信息或错误消息</returns>
    public static async Task<DataResult<BaseSystemEntity>> CheckSystem(this IMediator mediator,
        string companyId,
        string systemId)
    {
        // 验证系统标识是否为空
        if (string.IsNullOrWhiteSpace(systemId))
            return new DataResult<BaseSystemEntity>();

        // 构造获取系统实体的参数
        var getSystemArgs = new GetBaseSystemEntityArgs
        {
            CompanyId = companyId,
            Id = systemId
        };

        // 通过中介者模式获取系统信息
        var system = await mediator.Send(getSystemArgs);

        // 检查系统是否存在
        if (system is null)
            return new DataResult<BaseSystemEntity>(500, "系统不存在");

        // 检查系统状态是否正常
        if (system.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseSystemEntity>(500, "系统已禁用");

        // 返回系统信息
        return new DataResult<BaseSystemEntity>
        {
            Data = system
        };
    }

    /// <summary>
    /// 批量检查用户是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="departmentIds">部门标识列表</param>
    /// <param name="userIds">用户标识列表</param>
    /// <returns>返回用户实体的分页结果，包含用户列表或错误消息</returns>
    public static async Task<PageResult<BaseUserEntity>> CheckUsers(this IMediator mediator,
        List<string> companyIds,
        List<string> departmentIds,
        List<string> userIds)
    {
        // 构造查询用户的参数对象
        var searchUserArgs = new SearchBaseUserEntityArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 根据公司标识筛选用户
        if (userIds.Count == 0)
            searchUserArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 根据部门标识筛选用户
        if (companyIds.Count == 0)
            searchUserArgs.AddArgs("DepartmentId", departmentIds.ToJson(), SearchEnum.In);

        // 根据用户标识筛选用户
        if (departmentIds.Count == 0)
            searchUserArgs.AddArgs("Id", userIds.ToJson(), SearchEnum.In);

        // 通过中介者模式查询用户列表
        var users = await mediator.Send(searchUserArgs);

        // 检查是否存在匹配的用户
        if (users.Count == 0)
            return new PageResult<BaseUserEntity>(500, $"用户{userIds.FirstOrDefault()}不存在");

        // 获取查询结果中的用户标识列表
        var _userIds = users.Select(y => y.Id).ToList();
        // 查找是否有未找到的用户标识
        var _userId = userIds.FirstOrDefault(x => !_userIds.Contains(x));

        // 如果存在未找到的用户标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_userId))
            return new PageResult<BaseUserEntity>(500, $"用户{_userId}不存在");

        // 检查是否存在已禁用的用户
        var user = users.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在已禁用的用户，返回错误信息
        if (user is not null)
            return new PageResult<BaseUserEntity>(500, $"用户{user?.RealName}已经禁用");

        // 返回用户列表
        return new PageResult<BaseUserEntity>
        {
            Rows = users
        };
    }

    /// <summary>
    /// 检查单个用户是否存在且可用
    /// </summary>
    /// <param name="mediator">中介者接口，用于发送请求</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="departmentId">部门标识</param>
    /// <param name="userId">用户标识</param>
    /// <returns>返回用户实体的数据结果，包含用户信息或错误消息</returns>
    public static async Task<DataResult<BaseUserEntity>> CheckUser(this IMediator mediator,
        string companyId,
        string departmentId,
        string userId)
    {
        // 验证用户标识是否为空
        if (string.IsNullOrWhiteSpace(userId))
            return new DataResult<BaseUserEntity>();

        // 构造获取用户实体的参数
        var getUserArgs = new GetBaseUserEntityArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            Id = userId
        };

        // 通过中介者模式获取用户信息
        var user = await mediator.Send(getUserArgs);

        // 检查用户是否存在
        if (user is null)
            return new DataResult<BaseUserEntity>(500, "用户不存在");

        // 检查用户状态是否正常
        if (user.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseUserEntity>(500, "用户已禁用");

        // 返回用户信息
        return new DataResult<BaseUserEntity>
        {
            Data = user
        };
    }

    /// <summary>
    /// 获取下一个标识值
    /// </summary>
    /// <param name="cache">缓存接口</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="table">表名</param>
    /// <returns>返回生成的下一个标识值</returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache,
        string companyId,
        string table)
    {
        // 通过缓存接口获取下一个标识值 使用NextIdentifyArg构造参数，包含公司标识和表名
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
        // 创建用户信息对象
        var user = new BaseUserInfo();

        // 获取HTTP上下文
        var context = httpContextAccessor.HttpContext;

        // 从请求头中获取Token
        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        // 从Claims中获取用户数据
        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        // 如果存在用户数据，则反序列化为用户信息对象
        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        // 设置用户Token
        user.Token = token;

        // 返回初始化后的用户信息
        return user;
    }
}