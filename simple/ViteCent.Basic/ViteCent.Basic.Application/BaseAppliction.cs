using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

namespace ViteCent.Basic.Application;

/// <summary>
/// 基础仓储
/// </summary>
public static class BaseAppliction
{
    /// <summary>
    /// /
    /// </summary>
    /// <param name="user"></param>
    /// <param name="args"></param>
    public static void AddCompanyId(this SearchArgs args, BaseUserInfo user)
    {
        args.Args.RemoveAll(x => x.Field == "CompanyId");
        args.Args.Add(new SearchItem()
        {
            Field = "CompanyId",
            Value = user.Company.Id,
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="companyInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckCompany(this IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke, string companyId, string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId))
            return new BaseResult(string.Empty);

        var hasCompanyArgs = new GetBaseCompanyArgs
        {
            Id = companyId,
        };

        var hasCompany = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Get", hasCompanyArgs, token);

        if (!hasCompany.Success)
            return hasCompany;

        if (hasCompany.Data == null)
            return new BaseResult(500, "公司不存在");

        if (hasCompany.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司已禁用");

        return hasCompany;
    }

    /// <summary>
    /// </summary>
    /// <param name="departmentInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckDepartment(this IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke, string companyId, string departmentId, string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(departmentId))
            return new BaseResult(string.Empty);

        var hasDepartmentArgs = new GetBaseDepartmentArgs
        {
            CompanyId = companyId,
            Id = departmentId,
        };

        var hasDepartment = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Get", hasDepartmentArgs, token);

        if (!hasDepartment.Success)
            return hasDepartment;

        if (hasDepartment.Data == null)
            return new BaseResult(500, "部门不存在");

        if (hasDepartment.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "部门已禁用");

        return hasDepartment;
    }

    /// <summary>
    /// </summary>
    /// <param name="userInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckUser(this IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke, string companyId, string departmentId, string userId, string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(departmentId) && !string.IsNullOrWhiteSpace(userId))
            return new BaseResult(string.Empty);

        var hasUserArgs = new GetBaseUserArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            Id = userId,
        };

        var hasUser = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Get", hasUserArgs, token);

        if (!hasUser.Success)
            return hasUser;

        if (hasUser.Data == null)
            return new BaseResult(500, "用户不存在");

        if (hasUser.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "用户已禁用");

        return hasUser;
    }

    /// <summary>
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="companyId"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache, string companyId, string table)
    {
        return await cache.NextIdentity(new NextIdentifyArg()
        {
            CompanyId = companyId,
            Name = table,
        });
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public static BaseUserInfo InitUser(this IHttpContextAccessor httpContextAccessor)
    {
        var user = new BaseUserInfo();

        var context = httpContextAccessor.HttpContext;

        var token = context?.Request.Headers[Const.Token].ToString() ?? string.Empty;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        user.Token = token;

        return user;
    }
}