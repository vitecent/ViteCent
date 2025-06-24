#region

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Authorize.Jwt;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 登录仓储
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="configuration">配置信息</param>
public class Login(
    ILogger<Login> logger,
    IBaseCache cache,
    IMapper mapper,
    IMediator mediator,
    IConfiguration configuration) : IRequestHandler<LoginArgs, DataResult<LoginResult>>
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<DataResult<LoginResult>> Handle(LoginArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.Login");

        var args = mapper.Map<LoginEntityArgs>(request);
        args.Password = $"{args.Username}{args.Password}{BaseConst.Salf}".EncryptMD5();

        var user = await mediator.Send(args, cancellationToken);

        if (user is null)
            return new DataResult<LoginResult>(500, "用户名或密码错误");

        if (user.Status == (int)StatusEnum.Disable)
            return new DataResult<LoginResult>(500, "用户已被禁用");

        var userInfo = new BaseUserInfo
        {
            Id = user.Id,
            Name = user?.RealName,
            Code = user.Username,
            IsSuper = user.IsSuper.Value
        };

        if (!string.IsNullOrWhiteSpace(user.CompanyId))
        {
            var companyArgs = new GetBaseCompanyArgs
            {
                Id = user.CompanyId
            };

            var baseCompany = await mediator.Send(companyArgs, cancellationToken);

            if (baseCompany.Data is null)
                return new DataResult<LoginResult>(500, "公司信息不存在");

            userInfo.Company = new BaseCompanyInfo
            {
                Id = baseCompany.Data.Id,
                Name = baseCompany.Data.Name,
                Code = baseCompany.Data.Code
            };
        }

        if (!string.IsNullOrWhiteSpace(user.DepartmentId))
        {
            var departmentArgs = new GetBaseDepartmentArgs
            {
                Id = user.DepartmentId
            };

            if (!string.IsNullOrWhiteSpace(user.CompanyId))
                departmentArgs.CompanyId = user.CompanyId;

            var baseDepartment = await mediator.Send(departmentArgs, cancellationToken);

            if (baseDepartment.Data is null)
                return new DataResult<LoginResult>(500, "部门信息不存在");

            userInfo.Department = new BaseDepartmentInfo
            {
                Id = baseDepartment.Data.Id,
                Name = baseDepartment.Data.Name,
                Code = baseDepartment.Data.Code
            };
        }

        if (!string.IsNullOrWhiteSpace(user.PositionId))
        {
            var positionArgs = new GetBasePositionArgs
            {
                Id = user.PositionId
            };

            if (!string.IsNullOrWhiteSpace(user.CompanyId))
                positionArgs.CompanyId = user.CompanyId;

            var basePosition = await mediator.Send(positionArgs, cancellationToken);

            if (basePosition.Data is null)
                return new DataResult<LoginResult>(500, "职位信息不存在");

            userInfo.Position = new BasePositionInfo
            {
                Id = basePosition.Data.Id,
                Name = basePosition.Data.Name,
                Code = basePosition.Data.Code
            };
        }

        if (userInfo.IsSuper != (int)YesNoEnum.Yes)
            await GetUserRole(userInfo, cancellationToken);

        var authInfo = userInfo.AuthInfo;
        userInfo.AuthInfo = [];

        var token = BaseJwt.GenerateJwtToken(userInfo, configuration);

        token = $"Bearer {token}";
        var refreshToken = user.Id.EncryptMD5();

        var flagExpires = int.TryParse(configuration["Jwt:Expires"] ?? default!, out var expires);

        if (!flagExpires || expires < 1) expires = 24;

        cache.SetString($"User{user.Id}", token, TimeSpan.FromHours(expires));
        cache.SetString($"User{refreshToken}", request, TimeSpan.FromDays(365));
        cache.SetString($"UserInfo{user.Id}", authInfo, TimeSpan.FromHours(expires));

        var result = new DataResult<LoginResult>(new LoginResult
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpireTime = DateTime.Now.AddHours(24)
        });

        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="ids">标识</param>
    /// <param name="userInfo">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task GetOperation(List<string> ids, BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
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

        if (ids.Count > 0)
            args.AddArgs("Id", ids.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(userInfo?.Company?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = userInfo.Company.Id
            });

        var data = await mediator.Send(args, cancellationToken);

        if (data.Total > 0)
        {
            var _ids = data.Rows.Select(x => x.ResourceId).ToList();

            await GetResource(_ids, userInfo, data.Rows, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="ids">标识</param>
    /// <param name="userInfo">用户信息</param>
    /// <param name="operations">操作信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task GetResource(List<string> ids, BaseUserInfo userInfo, List<BaseOperationResult> operations,
        CancellationToken cancellationToken)
    {
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

        if (ids.Count > 0)
            args.AddArgs("Id", ids.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(userInfo?.Company?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = userInfo.Company.Id
            });

        var data = await mediator.Send(args, cancellationToken);

        if (data.Total > 0)
        {
            var _ids = data.Rows.Select(x => x.SystemId).ToList();

            await GetSystem(ids, userInfo, operations, data.Rows, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="ids">标识</param>
    /// <param name="userInfo">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task GetRole(List<string> ids, BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
        var args = new SearchBaseRoleArgs
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

        if (ids.Count > 0)
            args.AddArgs("Id", ids.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(userInfo?.Company?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = userInfo.Company.Id
            });

        var data = await mediator.Send(args, cancellationToken);

        if (data.Total > 0)
        {
            var _ids = data.Rows.Select(x => x.Id).ToList();

            await GetRolePermission(_ids, userInfo, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="ids">标识</param>
    /// <param name="userInfo">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task GetRolePermission(List<string> ids, BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
        var args = new SearchBaseRolePermissionArgs
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

        if (ids.Count > 0)
            args.AddArgs("Id", ids.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(userInfo?.Company?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = userInfo.Company.Id
            });

        var data = await mediator.Send(args, cancellationToken);

        if (data.Total > 0)
        {
            var _ids = data.Rows.Select(x => x.OperationId).ToList();

            await GetOperation(_ids, userInfo, cancellationToken);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="ids">标识</param>
    /// <param name="userInfo">用户信息</param>
    /// <param name="operations">操作信息</param>
    /// <param name="resources">资源信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task GetSystem(List<string> ids, BaseUserInfo userInfo, List<BaseOperationResult> operations,
        List<BaseResourceResult> resources, CancellationToken cancellationToken)
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

        if (ids.Count > 0)
            args.AddArgs("Id", ids.ToJson(), SearchEnum.In);

        if (!string.IsNullOrWhiteSpace(userInfo?.Company?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = userInfo.Company.Id
            });

        var systems = await mediator.Send(args, cancellationToken);

        var authInfo = new List<BaseSystemInfo>();
        var auth = new List<string>();

        if (systems.Total > 0)
            foreach (var system in systems.Rows)
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

                    var _operations = operations.Where(x => x.SystemId == system.Id && x.ResourceId == resource.Id)
                        .ToList();

                    foreach (var operation in _operations)
                    {
                        var _operation = new BaseOperationInfo
                        {
                            Id = operation.Id,
                            Name = operation.Name,
                            Code = operation.Code
                        };

                        auth.Add($"{system.Code}.{resource.Code}.{operation.Code}");

                        _resource.Operations.Add(_operation);
                    }

                    _system.Resources.Add(_resource);
                }

                authInfo.Add(_system);
            }

        userInfo.AuthInfo = authInfo;
        userInfo.Auth = auth;
    }

    /// <summary>
    /// </summary>
    /// <param name="userInfo">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task GetUserRole(BaseUserInfo userInfo, CancellationToken cancellationToken)
    {
        var args = new SearchBaseUserRoleArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "Status",
                    Value = ((int)StatusEnum.Enable).ToString()
                },
                new SearchItem
                {
                    Field = "UserId",
                    Value = userInfo.Id,
                    Group = "Role"
                }
            ]
        };

        if (!string.IsNullOrWhiteSpace(userInfo?.Company?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "CompanyId",
                Value = userInfo.Company.Id,
                Group = "Role"
            });

        if (!string.IsNullOrWhiteSpace(userInfo?.Department?.Id))
            args.Args.Add(new SearchItem
            {
                Field = "DepartmentId",
                Value = userInfo.Department.Id,
                Group = "Role"
            });

        var data = await mediator.Send(args, cancellationToken);

        if (data.Total > 0)
        {
            var _ids = data.Rows.Select(x => x.RoleId).ToList();

            await GetRole(_ids, userInfo, cancellationToken);
        }
    }
}