#region

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlSugar;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Domain.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Authorize.Jwt;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="configuration"></param>
public class Login(ILogger<AddBaseUser> logger, IBaseCache cache, IMapper mapper, IMediator mediator, IConfiguration configuration) : IRequestHandler<LoginArgs, DataResult<LoginResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<LoginResult>> Handle(LoginArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Login");

        var args = mapper.Map<LoginEntityArgs>(request);
        args.Password = $"{args.Username}{args.Password}{Const.Salf}".EncryptMD5();

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new DataResult<LoginResult>(500, "用户名或者密码错误");

        if (entity.Status == (int)StatusEnum.Disable)
            return new DataResult<LoginResult>(500, "用户已被禁用");

        var userInfo = new BaseUserInfo()
        {
            Id = entity.Id,
            Name = entity.Username,
            Code = entity.UserNo,
        };

        if (string.IsNullOrWhiteSpace(entity.CompanyId))
            userInfo.IsSuper = (int)YesNoEnum.Yes;

        if (!string.IsNullOrWhiteSpace(entity.CompanyId))
        {
            var companyArgs = new GetBaseCompanyArgs()
            {
                Id = entity.CompanyId,
            };

            var baseCompany = await mediator.Send(companyArgs, cancellationToken);

            if (baseCompany.Data == null)
                return new DataResult<LoginResult>(500, "公司信息不存在");

            userInfo.Company = new BaseCompanyInfo()
            {
                Id = baseCompany.Data.Id,
                Name = baseCompany.Data.Name,
                Code = baseCompany.Data.Code,
            };
        }

        if (!string.IsNullOrWhiteSpace(entity.DepartmentId))
        {
            var departmenArgs = new GetBaseDepartmentArgs()
            {
                Id = entity.DepartmentId,
            };

            var baseDepartment = await mediator.Send(departmenArgs, cancellationToken);

            if (baseDepartment.Data == null)
                return new DataResult<LoginResult>(500, "部门信息不存在");

            userInfo.Department = new BaseDepartmentInfo()
            {
                Id = baseDepartment.Data.Id,
                Name = baseDepartment.Data.Name,
                Code = baseDepartment.Data.Code,
            };
        }

        if (userInfo.IsSuper == (int)YesNoEnum.No)
            await GetRole(entity, userInfo, cancellationToken);

        var token = BaseJwt.GenerateJwtToken(userInfo, configuration);

        token = $"Bearer {token}";

        userInfo.Token = token;

        cache.SetString(entity.Id, token, TimeSpan.FromHours(24));

        var result = new DataResult<LoginResult>()
        {
            Data = new LoginResult()
            {
                Token = token,
                ExpireTime = DateTime.Now.AddHours(24),
            }
        };

        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="operationIds"></param>
    /// <param name="userInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task GetOperation(List<string> operationIds, BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
        var rolepermissionArgs = new SearchBaseOperationArgs()
        {
            Args =
            [
                new()
                {
                    Field = "Status",
                    Value = StatusEnum.Enable,
                },
                new()
                {
                    Field = "Id",
                    Value = operationIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var operations = await mediator.Send(rolepermissionArgs, cancellationToken);

        if (operations.Total > 0)
        {
            var resourceIds = operations.Rows.Select(x => x.ResourceId).ToList();

            await GetResource(resourceIds, userInfo, operations.Rows, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="resourceIds"></param>
    /// <param name="userInfo"></param>
    /// <param name="operations"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task GetResource(List<string> resourceIds, BaseUserInfo userInfo, List<BaseOperationResult> operations, CancellationToken cancellationToken)
    {
        var rolepermissionArgs = new SearchBaseResourceArgs()
        {
            Args =
            [
                new()
                {
                    Field = "Status",
                    Value = StatusEnum.Enable,
                },
                new()
                {
                    Field = "Id",
                    Value = resourceIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var resources = await mediator.Send(rolepermissionArgs, cancellationToken);

        if (resources.Total > 0)
        {
            var systemIds = resources.Rows.Select(x => x.SystemId).ToList();

            await GetSystem(resourceIds, userInfo, operations, resources.Rows, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="userInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task GetRole(BaseUserEntity entity, BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
        var roleArgs = new SearchBaseRoleArgs()
        {
            Args =
            [
                new ()
                {
                    Field = "Status",
                    Value = StatusEnum.Enable,
                },
                new()
                {
                    Field = "UserId",
                    Value = entity.Id,
                    Group = "Role",
                }
            ]
        };

        if (!string.IsNullOrWhiteSpace(entity.DepartmentId))
            roleArgs.Args.Add(new()
            {
                Field = "DepartmentId",
                Value = entity.DepartmentId,
                Group = "Role",
            });

        var roles = await mediator.Send(roleArgs, cancellationToken);

        if (roles.Total > 0)
        {
            var roleIds = roles.Rows.Select(x => x.Id).ToList();

            await GetRolePermission(roleIds, userInfo, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="roleIds"></param>
    /// <param name="userInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task GetRolePermission(List<string> roleIds, BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
        var rolepermissionArgs = new SearchBaseRolePermissionArgs()
        {
            Args =
            [
                new()
                {
                    Field = "Status",
                    Value = StatusEnum.Enable,
                },
                new()
                {
                    Field = "Id",
                    Value = roleIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var rolePermissions = await mediator.Send(rolepermissionArgs, cancellationToken);

        if (rolePermissions.Total > 0)
        {
            var operationIds = rolePermissions.Rows.Select(x => x.OperationId).ToList();

            await GetOperation(operationIds, userInfo, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="resourceIds"></param>
    /// <param name="userInfo"></param>
    /// <param name="operations"></param>
    /// <param name="resources"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task GetSystem(List<string> resourceIds, BaseUserInfo userInfo, List<BaseOperationResult> operations, List<BaseResourceResult> resources, CancellationToken cancellationToken)
    {
        var rolepermissionArgs = new SearchBaseSystemArgs()
        {
            Args =
            [
                new()
                {
                    Field = "Status",
                    Value = StatusEnum.Enable,
                },
                new()
                {
                    Field = "Id",
                    Value = resourceIds,
                    Method = SearchEnum.In
                }
            ]
        };

        var systems = await mediator.Send(rolepermissionArgs, cancellationToken);

        var auths = new List<BaseSystemInfo>();

        if (systems.Total > 0)
        {
            foreach (var system in systems.Rows)
            {
                var _system = new BaseSystemInfo()
                {
                    Id = system.Id,
                    Name = system.Name,
                    Code = system.Code,
                };

                var _resources = resources.Where(x => x.SystemId == system.Id).ToList();

                foreach (var resource in _resources)
                {
                    var _resource = new BaseResourceInfo()
                    {
                        Id = resource.Id,
                        Name = resource.Name,
                        Code = resource.Code,
                    };

                    var _operations = operations.Where(x => x.SystemId == system.Id && x.ResourceId == resource.Id).ToList();

                    foreach (var operation in _operations)
                    {
                        var _operation = new BaseOperationInfo()
                        {
                            Id = operation.Id,
                            Name = operation.Name,
                            Code = operation.Code,
                        };

                        _resource.Operations.Add(_operation);
                    }

                    _system.Resources.Add(_resource);
                }

                auths.Add(_system);
            }
        }

        userInfo.Auth = auths;
    }
}