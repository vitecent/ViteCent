using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BasePosition;
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
    /// </summary>
    /// <param name="companyInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseCompanyResult>> CheckCompany(this IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
        string companyId,
        string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId))
            return new DataResult<BaseCompanyResult>();

        var getCompanyArgs = new GetBaseCompanyArgs
        {
            Id = companyId,
        };

        var company = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Get", getCompanyArgs, token);

        if (!company.Success)
            return company;

        if (company.Data == null)
            return new DataResult<BaseCompanyResult>(500, "公司不存在");

        if (company.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseCompanyResult>(500, "公司已禁用");

        return company;
    }

    /// <summary>
    /// </summary>
    /// <param name="companyInvoke"></param>
    /// <param name="companyIds"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseCompanyResult>> CheckCompany(this IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
        List<string> companyIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0)
            return new PageResult<BaseCompanyResult>();

        var searchCompanyArgs = new SearchBaseCompanyArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args =
            [
                new ()
                {
                    Field = "Id",
                    Value = companyIds.ToJson(),
                    Method = SearchEnum.In
                }
            ]
        };

        var companys = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Page", searchCompanyArgs, token);

        if (!companys.Success)
            return companys;

        if (companys.Total == 0)
            return new PageResult<BaseCompanyResult>(500, $"公司{companyIds.FirstOrDefault()}不存在");

        var _companyIds = companys.Rows.Select(y => y.Id).ToList();
        var _companyId = companyIds.FirstOrDefault(x => !_companyIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_companyId))
            return new PageResult<BaseCompanyResult>(500, $"公司{_companyId}不存在");

        var company = companys.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (company != null)
            return new PageResult<BaseCompanyResult>(500, $"公司{company.Name}已经禁用");

        return companys;
    }

    /// <summary>
    /// </summary>
    /// <param name="departmentInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseDepartmentResult>> CheckDepartment(this IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
        string companyId, string departmentId, string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(departmentId))
            return new DataResult<BaseDepartmentResult>();

        var getDepartmentArgs = new GetBaseDepartmentArgs
        {
            CompanyId = companyId,
            Id = departmentId,
        };

        var department = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Get", getDepartmentArgs, token);

        if (!department.Success)
            return department;

        if (department.Data == null)
            return new DataResult<BaseDepartmentResult>(500, "部门不存在");

        if (department.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseDepartmentResult>(500, "部门已禁用");

        return department;
    }

    /// <summary>
    /// </summary>
    /// <param name="departmentInvoke"></param>
    /// <param name="companyIds"></param>
    /// <param name="departmentIds"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<PageResult<BaseDepartmentResult>> CheckDepartment(this IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke,
        List<string> companyIds,
        List<string> departmentIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        departmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0 && departmentIds.Count == 0)
            return new PageResult<BaseDepartmentResult>();

        var searchDepartmentArgs = new SearchBaseDepartmentArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchDepartmentArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (departmentIds.Count > 0)
            searchDepartmentArgs.AddArgs("Id", departmentIds.ToJson(), SearchEnum.In);

        var departments = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Page", searchDepartmentArgs, token);

        if (!departments.Success)
            return departments;

        if (departments.Total == 0)
            return new PageResult<BaseDepartmentResult>(500, $"部门{departmentIds.FirstOrDefault()}不存在");

        var _departmentIds = departments.Rows.Select(y => y.Id).ToList();
        var _departmentId = departmentIds.FirstOrDefault(x => !_departmentIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_departmentId))
            return new PageResult<BaseDepartmentResult>(500, $"部门{_departmentId}不存在");

        var department = departments.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (department != null)
            return new PageResult<BaseDepartmentResult>(500, $"部门{department.Name}已经禁用");

        return departments;
    }

    /// <summary>
    /// </summary>
    /// <param name="positionInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="positionId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<DataResult<BasePositionResult>> CheckPosition(this IBaseInvoke<GetBasePositionArgs, DataResult<BasePositionResult>> positionInvoke,
        string companyId, string positionId, string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(positionId))
            return new DataResult<BasePositionResult>();

        var getPositionArgs = new GetBasePositionArgs
        {
            CompanyId = companyId,
            Id = positionId,
        };

        var position = await positionInvoke.InvokePostAsync("Auth", "/BasePosition/Get", getPositionArgs, token);

        if (!position.Success)
            return position;

        if (position.Data == null)
            return new DataResult<BasePositionResult>(500, "职位不存在");

        if (position.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BasePositionResult>(500, "职位已禁用");

        return position;
    }

    /// <summary>
    /// </summary>
    /// <param name="positionInvoke"></param>
    /// <param name="companyIds"></param>
    /// <param name="positionIds"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<PageResult<BasePositionResult>> CheckPosition(this IBaseInvoke<SearchBasePositionArgs, PageResult<BasePositionResult>> positionInvoke,
        List<string> companyIds,
        List<string> positionIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        positionIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0 && positionIds.Count == 0)
            return new PageResult<BasePositionResult>();

        var searchPositionArgs = new SearchBasePositionArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchPositionArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (positionIds.Count > 0)
            searchPositionArgs.AddArgs("Id", positionIds.ToJson(), SearchEnum.In);

        var positions = await positionInvoke.InvokePostAsync("Auth", "/BasePosition/Page", searchPositionArgs, token);

        if (!positions.Success)
            return positions;

        if (positions.Total == 0)
            return new PageResult<BasePositionResult>(500, $"职位{positionIds.FirstOrDefault()}不存在");

        var _positionIds = positions.Rows.Select(y => y.Id).ToList();
        var _positionId = positionIds.FirstOrDefault(x => !_positionIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_positionId))
            return new PageResult<BasePositionResult>(500, $"职位{_positionId}不存在");

        var position = positions.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (position != null)
            return new PageResult<BasePositionResult>(500, $"职位{position.Name}已经禁用");

        return positions;
    }

    /// <summary>
    /// </summary>
    /// <param name="userInvoke"></param>
    /// <param name="companyId"></param>
    /// <param name="departmentId"></param>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<DataResult<BaseUserResult>> CheckUser(this IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
        string companyId,
        string departmentId,
        string userId,
        string token)
    {
        if (!string.IsNullOrWhiteSpace(companyId) && !string.IsNullOrWhiteSpace(departmentId) && !string.IsNullOrWhiteSpace(userId))
            return new DataResult<BaseUserResult>();

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
            return new DataResult<BaseUserResult>(500, "用户不存在");

        if (user.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseUserResult>(500, "用户已禁用");

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
    public static async Task<PageResult<BaseUserResult>> CheckUser(this IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke,
        List<string> companyIds,
        List<string> departmentIds,
        List<string> userIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        departmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        userIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0 && departmentIds.Count == 0 && userIds.Count == 0)
            return new PageResult<BaseUserResult>();

        var searchUserArgs = new SearchBaseUserArgs()
        {
            Offset = 0,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchUserArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (departmentIds.Count > 0)
            searchUserArgs.AddArgs("DepartmentId", departmentIds.ToJson(), SearchEnum.In);

        if (userIds.Count > 0)
            searchUserArgs.AddArgs("Id", userIds.ToJson(), SearchEnum.In);

        var users = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Page", searchUserArgs, token);

        if (!users.Success)
            return users;

        if (users.Total == 0)
            return new PageResult<BaseUserResult>(500, $"用户{userIds.FirstOrDefault()}不存在");

        var _userIds = users.Rows.Select(y => y.Id).ToList();
        var _userId = userIds.FirstOrDefault(x => !_userIds.Contains(x));

        if (!string.IsNullOrWhiteSpace(_userId))
            return new PageResult<BaseUserResult>(500, $"用户{_userId}不存在");

        var user = users.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        if (user != null)
            return new PageResult<BaseUserResult>(500, $"用户{user.RealName}已经禁用");

        return users;
    }

    /// <summary>
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="companyId"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache,
        string companyId,
        string table)
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

        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        user.Token = token;

        return user;
    }
}