/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
public partial class AddBaseUser
{
    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="request">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseUserListArgs request,
        BaseUserInfo user, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;
        var departmentId = user?.Department?.Id ?? string.Empty;
        var positionId = user?.Position?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

            if (string.IsNullOrWhiteSpace(item.DepartmentId))
                item.DepartmentId = departmentId;

            if (string.IsNullOrWhiteSpace(item.PositionId))
                item.PositionId = positionId;
        }

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var departmentIds = request.Items.Select(x => x.DepartmentId).Distinct().ToList();
        var positionIds = request.Items.Select(x => x.PositionId).Distinct().ToList();

        var companys = await mediator.CheckCompanys(companyIds);

        if (!companys.Success)
            return companys;

        foreach (var item in companys.Rows)
        {
            var items = request.Items.Where(x => x.CompanyId == item.Id).ToList();

            foreach (var data in items)
                data.CompanyName = item.Name;
        }

        var departments = await mediator.CheckDepartments(companyIds, departmentIds);

        if (!departments.Success)
            return departments;

        foreach (var item in departments.Rows)
        {
            var items = request.Items.Where(x => x.DepartmentId == item.Id).ToList();

            foreach (var data in items)
                data.DepartmentId = item.Name;
        }

        var positions = await mediator.CheckPositions(companyIds, positionIds);

        if (!positions.Success)
            return positions;

        foreach (var item in positions.Rows)
        {
            var items = request.Items.Where(x => x.PositionId == item.Id).ToList();

            foreach (var data in items)
                data.PositionName = item.Name;
        }

        var hasListArgs = new HasBaseUserEntityListArgs
        {
            CompanyIds = companyIds,
            DepartmentIds = departmentIds,
            PositionIds = positionIds,
            UserNos = [.. request.Items.Select(x => x.UserNo).Distinct()],
            Usernames = [.. request.Items.Select(x => x.Username).Distinct()],
            RealNames = [.. request.Items.Select(x => x.RealName).Distinct()],
            IdCards = [.. request.Items.Select(x => x.IdCard).Distinct()],
            Emails = [.. request.Items.Select(x => x.Email).Distinct()],
            Phones = [.. request.Items.Select(x => x.Phone).Distinct()]
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="topic">话题</param>
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task OverrideTopic(IMediator mediator, TopicEnum topic, BaseUserEntity entity,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(AddBaseUserArgs request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Username) && !string.IsNullOrWhiteSpace(request.Password))
            request.Password = $"{request.Username}{request.Password}{BaseConst.Salf}".EncryptMD5();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany?.Data?.Name;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await mediator.CheckDepartment(request.CompanyId, request.DepartmentId);

        if (!hasDepartment.Success)
            return hasDepartment;

        request.DepartmentName = hasDepartment?.Data?.Name;

        var positionId = user?.Position?.Id ?? string.Empty;
        var positionName = user?.Position?.Name ?? string.Empty;

        if (string.IsNullOrWhiteSpace(positionName))
            positionName = request.PositionName;

        if (string.IsNullOrWhiteSpace(request.PositionId))
            request.PositionId = positionId;

        var hasPosition = await mediator.CheckPosition(request.CompanyId, request.PositionId);

        if (!hasPosition.Success)
            return hasPosition;

        request.PositionName = hasPosition?.Data?.Name;

        if (string.IsNullOrWhiteSpace(request.PositionName))
            request.PositionName = positionName;

        var hasArgs = new HasBaseUserEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId,
            UserNo = request.UserNo,
            Username = request.Username,
            RealName = request.RealName,
            IdCard = request.IdCard,
            Email = request.Email,
            Phone = request.Phone
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}