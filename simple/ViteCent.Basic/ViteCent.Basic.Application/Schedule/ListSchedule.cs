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
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="userInvoke">用户信息访问对象</param>
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
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
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
        var userIds = rows.Select(x => x.UserId).Distinct().ToList();

        var users = await userInvoke.CheckUsers(companyIds, departmentIds, [], userIds,
            user?.Token ?? string.Empty);

        if (!users.Success)
            return new PageResult<UserScheduleResult>(users.Code, users.Message);

        foreach (var item in users.Rows)
        {
            var userInfo = rows.FirstOrDefault(x => x.UserId == item.Id);

            if (userInfo is not null)
            {
                userInfo.Description = item.Description;
            }
        }

        var result = new PageResult<UserScheduleResult>(args.Offset, args.Limit, args.Total, rows);

        return result;
    }
}