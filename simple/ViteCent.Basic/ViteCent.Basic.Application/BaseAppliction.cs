using MediatR;
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
            return new BaseResult();

        var getCompanyArgs = new GetBaseCompanyArgs
        {
            Id = companyId,
        };

        var company = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Get", getCompanyArgs, token);

        if (!company.Success)
            return company;

        if (company.Data == null)
            return new BaseResult(500, "公司不存在");

        if (company.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "公司已禁用");

        return company;
    }

    /// <summary>
    /// </summary>
    /// <param name="companyInvoke"></param>
    /// <param name="companyIds"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckCompany(this IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke, List<string> companyIds, string token)
    {
        var searchCompanyArgs = new SearchBaseCompanyArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
            {
                Field = "Id",
                Value = companyIds,
                Method = SearchEnum.In
            }
            ]
        };

        var companys = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Search", searchCompanyArgs, token);

        if (!companys.Success)
            return companys;

        if (companys.Total == 0)
            return new BaseResult(500, $"公司{companyIds.FirstOrDefault()}不存在");

        var _companyIds = companys.Rows.Select(y => y.Id).ToList();
        var _companyId = companyIds.FirstOrDefault(x => !_companyIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_companyId))
            return new BaseResult(500, $"公司{_companyId}不存在");

        var company = companys.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (company != null)
            return new BaseResult(500, $"公司{company.Name}已经禁用");

        return companys;
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
            return new BaseResult();

        var getDepartmentArgs = new GetBaseDepartmentArgs
        {
            CompanyId = companyId,
            Id = departmentId,
        };

        var department = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Get", getDepartmentArgs, token);

        if (!department.Success)
            return department;

        if (department.Data == null)
            return new BaseResult(500, "部门不存在");

        if (department.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "部门已禁用");

        return department;
    }

    /// <summary>
    /// </summary>
    /// <param name="departmentInvoke"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckDepartment(this IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke, List<string> companyIds, List<string> departmentIds, string token)
    {
        var searchDepartmentArgs = new SearchBaseDepartmentArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
            {
                Field = "CompanyId",
                Value = companyIds,
                Method = SearchEnum.In
            },
            new ()
            {
                Field = "Id",
                Value = departmentIds,
                Method = SearchEnum.In
            }
            ]
        };

        var departments = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Search", searchDepartmentArgs, token);

        if (!departments.Success)
            return departments;

        if (departments.Total == 0)
            return new BaseResult(500, $"部门{departmentIds.FirstOrDefault()}不存在");

        var _departmentIds = departments.Rows.Select(y => y.Id).ToList();
        var _departmentId = departmentIds.FirstOrDefault(x => !_departmentIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_departmentId))
            return new BaseResult(500, $"部门{_departmentId}不存在");

        var department = departments.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (department != null)
            return new BaseResult(500, $"部门{department.Name}已经禁用");

        return departments;
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
            return new BaseResult();

        var getUserArgs = new GetBaseUserArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            Id = userId,
        };

        var user = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Get", getUserArgs, token);

        if (!user.Success)
            return user;

        if (user.Data == null)
            return new BaseResult(500, "用户不存在");

        if (user.Data.Status == (int)StatusEnum.Disable)
            return new BaseResult(500, "用户已禁用");

        return user;
    }

    /// <summary>
    /// </summary>
    /// <param name="userInvoke"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <param name="userIds"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<BaseResult> CheckUser(this IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke, List<string> companyIds, List<string> departmentIds, List<string> userIds, string token)
    {
        var searchUserArgs = new SearchBaseUserArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
                [
                    new ()
                {
                    Field = "CompanyId",
                    Value = companyIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "DepartmentId",
                    Value = departmentIds,
                    Method = SearchEnum.In
                },
                new ()
                {
                    Field = "Id",
                    Value = userIds,
                    Method = SearchEnum.In
                }
                ]
        };

        var users = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Get", searchUserArgs, token);

        if (users.Success)
            return users;

        if (users.Total == 0)
            return new BaseResult(500, $"用户{userIds.FirstOrDefault()}不存在");

        var _userIds = users.Rows.Select(y => y.Id).ToList();
        var _userId = userIds.FirstOrDefault(x => !_userIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_userId))
            return new BaseResult(500, $"用户{_userId}不存在");

        var user = users.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (user != null)
            return new BaseResult(500, $"用户{user.RealName}已经禁用");

        return users;
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