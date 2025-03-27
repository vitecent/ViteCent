#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public partial class EditBaseUser(ILogger<EditBaseUser> logger, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<EditBaseUserArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
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

        var hasCompanyArgs = new GetBaseCompanyEntityArgs
        {
            Id = request.CompanyId,
        };

        var hasCompany = await mediator.Send(hasCompanyArgs, cancellationToken);

        if (hasCompany == null)
            return new BaseResult(500, "公司不存在");

        if (hasCompany.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司已禁用");

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(departmentId))
            request.DepartmentId = departmentId;

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

        var result = await OverrideHandle(request, cancellationToken);

        if (!result.Success)
            return result;

        var args = mapper.Map<GetBaseUserEntityArgs>(request);

        var entity = await mediator.Send(args, cancellationToken);

        entity.Avatar = request.Avatar;
        entity.Birthday = request.Birthday;
        entity.Description = request.Description;
        entity.Email = request.Email;
        entity.Gender = request.Gender;
        entity.IdCard = request.IdCard;
        entity.Nickname = request.Nickname;
        entity.Password = request.Password;
        entity.Phone = request.Phone;
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