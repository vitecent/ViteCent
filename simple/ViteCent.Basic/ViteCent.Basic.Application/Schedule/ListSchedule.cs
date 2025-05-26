#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 排班信息分页仓储
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="userInvoke"></param>
public class ListSchedule(
    ILogger<ListSchedule> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor,
    IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke)
    : IRequestHandler<ListScheduleArgs, PageResult<UserScheduleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 排班信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PageResult<UserScheduleResult>> Handle(ListScheduleArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.ListSchedule");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<SearchScheduleEntityArgs>(request);

        args.AddCompanyId(user);

        var list = await mediator.Send(args, cancellationToken);

        var rows = mapper.Map<List<UserScheduleResult>>(list);

        var companyIds = rows.Select(x => x.CompanyId).Distinct().ToList();
        var departmentIds = rows.Select(x => x.DepartmentId).Distinct().ToList();
        var positionIds = rows.Select(x => x.PositionId).Distinct().ToList();
        var userIds = rows.Select(x => x.UserId).Distinct().ToList();

        var users = await userInvoke.CheckUsers(companyIds, departmentIds, positionIds, userIds,
            user?.Token ?? string.Empty);

        if (!users.Success)
            return new PageResult<UserScheduleResult>(users.Code, users.Message);

        foreach (var item in users.Rows)
        {
            var userInfo = rows.FirstOrDefault(x => x.UserId == item.Id);

            if (userInfo != null)
            {
                userInfo.PositionId = item.PositionId;
                userInfo.PositionName = item.PositionName;
                userInfo.Description = item.Description;
            }
        }

        var result = new PageResult<UserScheduleResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}