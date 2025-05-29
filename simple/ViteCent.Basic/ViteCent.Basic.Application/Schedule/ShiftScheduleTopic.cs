#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 处理换班信息
/// </summary>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="mediator"></param>
/// <param name="httpContextAccessor"></param>
public class ShiftScheduleTopic(
    ILogger<ShiftScheduleTopic> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : INotificationHandler<ShiftScheduleTopicArgs>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 处理换班信息
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(ShiftScheduleTopicArgs notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.ShiftScheduleTopic");

        user = httpContextAccessor.InitUser();

        var args = mapper.Map<GetScheduleEntityArgs>(notification);
        args.Id = notification.ScheduleId;

        var entity = await mediator.Send(args, cancellationToken);

        if (entity is null)
            return;

        entity.DepartmentId = notification.ShiftDepartmentId;
        entity.DepartmentName = notification.ShiftDepartmentName;
        entity.UserId = notification.ShiftUserId;
        entity.UserName = notification.ShiftUserName;
        entity.PostId = notification.ShiftPostId;
        entity.PostName = notification.ShiftPostName;
        entity.TypeId = notification.ShiftTypeId;
        entity.TypeName = notification.ShiftTypeName;

        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.DataVersion = DateTime.Now;

        await mediator.Send(entity, cancellationToken);
    }
}