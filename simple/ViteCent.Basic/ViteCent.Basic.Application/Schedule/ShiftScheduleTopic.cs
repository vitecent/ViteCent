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
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
public class ShiftScheduleTopic(
    ILogger<ShiftScheduleTopic> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor) : INotificationHandler<ShiftScheduleTopicArgs>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 处理换班信息
    /// </summary>
    /// <param name="notification">通知信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task Handle(ShiftScheduleTopicArgs notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Application.Schedule.ShiftScheduleTopic");

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
        entity.Version = DateTime.Now;

        await mediator.Send(entity, cancellationToken);
    }
}