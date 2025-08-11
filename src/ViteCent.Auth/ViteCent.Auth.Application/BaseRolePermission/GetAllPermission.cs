#region

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// 获取所有权限仓储
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public class GetAllPermission(
    ILogger<GetAllPermission> logger,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllPermissionArgs, DataResult<AllPermissionResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 获取所有权限
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<DataResult<AllPermissionResult>> Handle(GetAllPermissionArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.AllPermission.GetAllPermission");

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        var result = await GetSystem(request.CompanyId, cancellationToken);

        return new DataResult<AllPermissionResult>
        {
            Data = new AllPermissionResult
            {
                Items = result
            }
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="companyId">公司标识</param>
    /// <param name="systems">系统信息</param>
    /// <param name="resources">资源信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<List<BaseSystemInfo>> GetOperation(string companyId, List<BaseSystemResult> systems,
        List<BaseResourceResult> resources, CancellationToken cancellationToken)
    {
        var systemIds = systems.Select(x => x.Id).ToList();
        var resourceIds = systems.Select(x => x.Id).ToList();

        var args = new SearchBaseOperationArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "Status",
                    Value = ((int)StatusEnum.Enable).ToString()
                }
            ]
        };

        if (systemIds.Count > 0)
            args.AddArgs("SystemId", systemIds.ToJson(), SearchEnum.In);

        if (resourceIds.Count > 0)
            args.AddArgs("ResourceId", resourceIds.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(companyId))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = companyId
            });

        var operations = await mediator.Send(args, cancellationToken);

        if (operations.Total < 1)
            return [];

        var result = new List<BaseSystemInfo>();

        foreach (var system in systems)
        {
            var _system = new BaseSystemInfo
            {
                Id = system.Id,
                Name = system.Name,
                Code = system.Code
            };

            var _resources = resources.Where(x => x.SystemId == system.Id).ToList();

            foreach (var resource in _resources)
            {
                var _resource = new BaseResourceInfo
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Code = resource.Code
                };

                var _operations = operations.Rows.Where(x => x.SystemId == system.Id && x.ResourceId == resource.Id)
                    .ToList();

                foreach (var operation in _operations)
                {
                    var _operation = new BaseOperationInfo
                    {
                        Id = operation.Id,
                        Name = operation.Name,
                        Code = operation.Code
                    };

                    _resource.Operations.Add(_operation);
                }

                _system.Resources.Add(_resource);
            }

            result.Add(_system);
        }

        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="companyId">公司标识</param>
    /// <param name="systems">系统信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<List<BaseSystemInfo>> GetResource(string companyId, List<BaseSystemResult> systems,
        CancellationToken cancellationToken)
    {
        var systemIds = systems.Select(x => x.Id).ToList();

        var args = new SearchBaseResourceArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "Status",
                    Value = ((int)StatusEnum.Enable).ToString()
                }
            ]
        };

        if (systems.Count > 0)
            args.AddArgs("SystemId", systemIds.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(companyId))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = companyId
            });

        var resources = await mediator.Send(args, cancellationToken);

        if (resources.Total < 1)
            return [];

        return await GetOperation(companyId, systems, resources.Rows, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="companyId">公司标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<List<BaseSystemInfo>> GetSystem(string companyId, CancellationToken cancellationToken)
    {
        var args = new SearchBaseSystemArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "Status",
                    Value = ((int)StatusEnum.Enable).ToString()
                }
            ]
        };

        if (!string.IsNullOrWhiteSpace(companyId))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = companyId
            });

        var systems = await mediator.Send(args, cancellationToken);

        if (systems.Total < 1)
            return [];

        return await GetResource(companyId, systems.Rows, cancellationToken);
    }
}