/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 */

#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 编辑用户信息仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseUser(ILogger<EditBaseUser> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditBaseUserArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(EditBaseUserArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.EditBaseUser");

        InitUser(httpContextAccessor);

        var companyId = user?.Company?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(companyId))
            request.CompanyId = companyId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
        {
            var hasCompanyArgs = new GetBaseCompanyEntityArgs
            {
                Id = request.CompanyId,
            };

            var hasCompany = await mediator.Send(hasCompanyArgs, cancellationToken);

            if (hasCompany == null)
                return new BaseResult(500, "公司不存在");

            if (hasCompany.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "公司已禁用");
        }

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.DepartmentId))
        {
            var hasDepartmentArgs = new GetBaseDepartmentEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.DepartmentId,
            };

            var hasDepartment = await mediator.Send(hasDepartmentArgs, cancellationToken);

            if (hasDepartment == null)
                return new BaseResult(500, "部门不存在");

            if (hasDepartment.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "部门已禁用");
        }

        var positionId = user?.Position?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(positionId))
            request.PositionId = positionId;

        if (!string.IsNullOrWhiteSpace(request.CompanyId) && !string.IsNullOrWhiteSpace(request.PositionId))
        {
            var hasPositionArgs = new GetBasePositionEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.PositionId,
            };

            var hasPosition = await mediator.Send(hasPositionArgs, cancellationToken);

            if (hasPosition == null)
                return new BaseResult(500, "职位不存在");

            if (hasPosition.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "职位已禁用");
        }

        var preResult = await OverrideHandle(request, cancellationToken);

        if (!preResult.Success)
            return preResult;

        var args = mapper.Map<GetBaseUserEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "数据不存在或无权限");

        var result = await OverrideHandle(entity, cancellationToken);

        if (!result.Success)
            return result;

        entity.Avatar = request.Avatar;
        entity.Birthday = request.Birthday;
        entity.Color = request.Color;
        entity.Description = request.Description;
        entity.Email = request.Email;
        entity.Gender = request.Gender;
        entity.IdCard = request.IdCard;
        entity.Nickname = request.Nickname;
        entity.Password = request.Password;
        entity.Phone = request.Phone;
        entity.PositionId = request.PositionId;
        entity.RealName = request.RealName;
        entity.Status = request.Status;
        entity.Username = request.Username;
        entity.UserNo = request.UserNo;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        return await mediator.Send(entity, cancellationToken);
    }

    /// <summary>
    /// 获取用户信息用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    private void InitUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();
    }
}