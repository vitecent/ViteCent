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
/// <param name="logger"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class GetAllPermission(
    ILogger<GetAllPermission> logger,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllPermissionArgs, DataResult<AllPermissionResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 获取所有权限
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<AllPermissionResult>> Handle(GetAllPermissionArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.AllPermission.GetAllPermission");

        user = httpContextAccessor.InitUser();

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
    /// <param name="companyId"></param>
    /// <param name="systems"></param>
    /// <param name="resources"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// <param name="companyId"></param>
    /// <param name="systems"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// <param name="companyId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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