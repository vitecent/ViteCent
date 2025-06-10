#region

using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// </summary>
public partial class SignSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(ScheduleEntity entity, CancellationToken cancellationToken)
    {
        var siginTime = DateTime.Now;
        var now = siginTime.Date;
        var time = string.Empty;

        //可以打卡的时间
        if (string.IsNullOrWhiteSpace(entity.Times))
            return new BaseResult(500, "打卡时间格式错误");

        var arrays = entity.Times.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        if (arrays.Count == 0)
            return new BaseResult(500, "打卡时间格式错误(,)");

        foreach (var array in arrays)
        {
            var times = array.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList();

            if (times.Count != 2)
                return new BaseResult(500, "打卡时间格式错误(-)");

            var strStart = $"{now:yyyy-MM-dd} {times[0]}:00";
            var strEnd = $"{now:yyyy-MM-dd} {times[1]}:59";

            if (!DateTime.TryParse(strStart, out var start))
                return new BaseResult(500, "开始打卡时间格式错误(");

            if (!DateTime.TryParse(strEnd, out var end))
                return new BaseResult(500, "结束打卡时间格式错误");

            if (start > end)
                end = end.AddDays(1);

            if (siginTime >= start && siginTime <= end)
            {
                time = array;
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(time))
            return new BaseResult(500, "非可打卡时间");

        //已经打卡的时间
        var sign = string.Empty;

        var signTimes = entity?.SignTimes?.Split(',', StringSplitOptions.RemoveEmptyEntries)?.ToList() ?? [];

        if (signTimes.Any(x => x == time))
            return new BaseResult(500, "重复打卡");

        signTimes.Add(time);

        entity.SignTimes = string.Join(",", signTimes);

        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(SignScheduleArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (!hasCompany.Success)
            return hasCompany;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment =
            await departmentInvoke.CheckDepartment(request.CompanyId, request.DepartmentId,
                user?.Token ?? string.Empty);

        if (!hasDepartment.Success)
            return hasDepartment;

        var positionId = user?.Position?.Id ?? string.Empty;

        var hasUser = await userInvoke.CheckUser(request.CompanyId, request.DepartmentId, positionId, request.UserId,
            user?.Token ?? string.Empty);

        if (!hasUser.Success)
            return hasUser;

        return new BaseResult();
    }
}