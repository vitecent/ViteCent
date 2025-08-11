#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;

// 引入基础数据传输对象

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入排班模型
using ViteCent.Basic.Entity.Schedule;

// 引入核心数据类型
using ViteCent.Core.Data;
using ViteCent.Core.Web;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 接口
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="userInvoke">用户信息访问对象</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 Api 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Schedule")]
public class PreAddScheduleList(
    // 注入日志记录器
    ILogger<AddSchedule> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseListApi<List<PreAddScheduleArgs>, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <returns>处理结果</returns>
    [HttpPost] // 标记为POST请求
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Add" })]
    // 设置路由名称
    [Route("PreAddList")]
    public override async Task<BaseResult> InvokeAsync(List<PreAddScheduleArgs> args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedu.PreAddList");

        // 创建取消令牌，用于支持操作的取消
        var cancellationToken = new CancellationToken();

        var items = new List<AddScheduleArgs>();

        //获取所有用户
        var searchUserArgs = new SearchBaseUserArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        var users = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Page", searchUserArgs, user?.Token ?? string.Empty);

        if (!users.Success)
            return new BaseResult(users.Code, users.Message);

        foreach (var item in args)
        {
            //按照,拆分用户 第一个为主班，后边均为副班
            var names = item.Name.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Distinct().ToList();

            var firt = true;

            foreach (var name in names)
            {
                var user = users.Rows.FirstOrDefault(x => x.RealName == name);

                if (user is null)
                    return new BaseResult(500, $"用户{name}不存在");

                var _item = new AddScheduleArgs
                {
                    CompanyId = user.CompanyId,
                    DepartmentId = user.DepartmentId,
                    UserId = user.Id,
                    UserName = user.RealName,
                    TypeName = "副班",
                    PostName = item.Job,
                    Times = "",
                    SceduleTimes = DateTime.Parse($"{item.Date}"),
                    Status = (int)ScheduleEnum.None
                };

                if (firt)
                    _item.TypeName = "主班";

                items.Add(_item);

                firt = false;
            }
        }

        var request = new AddScheduleListArgs
        {
            Items = items
        };

        var companyId = user?.Company?.Id ?? string.Empty;
        var departmentId = user?.Department?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

            if (string.IsNullOrWhiteSpace(item.DepartmentId))
                item.DepartmentId = departmentId;
        }

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var departmentIds = request.Items.Select(x => x.DepartmentId).Distinct().ToList();
        var userIds = request.Items.Select(x => x.UserId).Distinct().ToList();

        var deleteScheduleArgs = new DeleteScheduleEntityListArgs
        {
            CompanyIds = companyIds,
            DepartmentIds = departmentIds,
            UserIds = userIds,
            StartTime = request.Items.Min(x => x.SceduleTimes),
            EndTime = request.Items.Max(x => x.SceduleTimes)
        };

        var delete = await mediator.Send(deleteScheduleArgs, cancellationToken);

        if (!delete.Success)
            return delete;

        // 通过中介者发送命令并返回结果
        return await mediator.Send(request, cancellationToken);
    }
}