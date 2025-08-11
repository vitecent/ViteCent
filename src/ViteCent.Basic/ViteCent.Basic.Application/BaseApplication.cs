#region 引用命名空间

using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion 引用命名空间

namespace ViteCent.Basic.Application;

/// <summary>
/// </summary>
public static class BaseAppliction
{
    /// <summary>
    /// </summary>
    /// <param name="companyInvoke">公司信息访问对象</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<DataResult<BaseCompanyResult>> CheckCompany(
        this IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
        string companyId,
        string token)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            return new DataResult<BaseCompanyResult>();

        var getCompanyArgs = new GetBaseCompanyArgs
        {
            Id = companyId
        };

        var company = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Get", getCompanyArgs, token);

        if (!company.Success)
            return company;

        if (company.Data is null)
            return new DataResult<BaseCompanyResult>(500, "公司不存在");

        if (company.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseCompanyResult>(500, "公司已禁用");

        return company;
    }

    /// <summary>
    /// </summary>
    /// <param name="companyInvoke">公司信息访问对象</param>
    /// <param name="companyIds">公司标识</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<PageResult<BaseCompanyResult>> CheckCompanys(
        this IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
        List<string> companyIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0)
            return new PageResult<BaseCompanyResult>();

        var searchCompanyArgs = new SearchBaseCompanyArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
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

        if (company is not null)
            return new PageResult<BaseCompanyResult>(500, $"公司{company.Name}已经禁用");

        return companys;
    }

    /// <summary>
    /// </summary>
    /// <param name="departmentInvoke">部门信息访问对象</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="departmentId">部门标识</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<DataResult<BaseDepartmentResult>> CheckDepartment(
        this IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
        string companyId,
        string departmentId,
        string token)
    {
        if (string.IsNullOrWhiteSpace(departmentId))
            return new DataResult<BaseDepartmentResult>();

        var getDepartmentArgs = new GetBaseDepartmentArgs
        {
            CompanyId = companyId,
            Id = departmentId
        };

        var department = await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Get", getDepartmentArgs, token);

        if (!department.Success)
            return department;

        if (department.Data is null)
            return new DataResult<BaseDepartmentResult>(500, "部门不存在");

        if (department.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseDepartmentResult>(500, "部门已禁用");

        return department;
    }

    /// <summary>
    /// </summary>
    /// <param name="departmentInvoke">部门信息访问对象</param>
    /// <param name="companyIds">公司标识</param>
    /// <param name="departmentIds">部门标识</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<PageResult<BaseDepartmentResult>> CheckDepartments(
        this IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke,
        List<string> companyIds,
        List<string> departmentIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        departmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0 && departmentIds.Count == 0)
            return new PageResult<BaseDepartmentResult>();

        var searchDepartmentArgs = new SearchBaseDepartmentArgs
        {
            Offset = 1,
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

        if (department is not null)
            return new PageResult<BaseDepartmentResult>(500, $"部门{department.Name}已经禁用");

        return departments;
    }

    /// <summary>
    /// </summary>
    /// <param name="userInvoke">用户信息访问对象</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="departmentId">部门标识</param>
    /// <param name="positionId">职位标识</param>
    /// <param name="userId">用户标识</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<DataResult<BaseUserResult>> CheckUser(
        this IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
        string companyId,
        string departmentId,
        string positionId,
        string userId,
        string token)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new DataResult<BaseUserResult>();

        var getUserArgs = new GetBaseUserArgs
        {
            CompanyId = companyId,
            DepartmentId = departmentId,
            PositionId = positionId,
            Id = userId
        };

        var user = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Get", getUserArgs, token);

        if (!user.Success)
            return user;

        if (user.Data is null)
            return new DataResult<BaseUserResult>(500, "用户不存在");

        if (user.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseUserResult>(500, "用户已禁用");

        return user;
    }

    /// <summary>
    /// </summary>
    /// <param name="userInvoke">用户信息访问对象</param>
    /// <param name="companyIds">公司标识</param>
    /// <param name="departmentIds">部门标识</param>
    /// <param name="positionIds">职位标识</param>
    /// <param name="userIds">用户标识</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<PageResult<BaseUserResult>> CheckUsers(
        this IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke,
        List<string> companyIds,
        List<string> departmentIds,
        List<string> positionIds,
        List<string> userIds,
        string token)
    {
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        departmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        positionIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        userIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (companyIds.Count == 0 && departmentIds.Count == 0 && userIds.Count == 0)
            return new PageResult<BaseUserResult>();

        var searchUserArgs = new SearchBaseUserArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        if (companyIds.Count > 0)
            searchUserArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (departmentIds.Count > 0)
            searchUserArgs.AddArgs("DepartmentId", departmentIds.ToJson(), SearchEnum.In);

        if (positionIds.Count > 0)
            searchUserArgs.AddArgs("PositionId", positionIds.ToJson(), SearchEnum.In);

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

        if (!string.IsNullOrWhiteSpace(user?.Id))
            return new PageResult<BaseUserResult>(500, $"用户{user?.RealName}已经禁用");

        return users;
    }

    /// <summary>
    /// </summary>
    /// <param name="dictionaryInvoke">字典信息访问对象</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="key">键</param>
    /// <param name="token">令牌</param>
    /// <returns>处理结果</returns>
    public static async Task<PageResult<BaseDictionaryResult>> GetDictionary(
        this IBaseInvoke<SearchBaseDictionaryArgs, PageResult<BaseDictionaryResult>> dictionaryInvoke,
        string companyId,
        string key,
        string token)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new PageResult<BaseDictionaryResult>();

        var searchDictionaryArgs = new SearchBaseDictionaryArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args =
            [
                new SearchItem
                {
                    Field = "Name",
                    Value = key,
                    Method = SearchEnum.Like,
                    Group = "Key"
                },
                new SearchItem
                {
                    Field = "Code",
                    Value = key,
                    Method = SearchEnum.Like,
                    Group = "Key"
                },
                new SearchItem
                {
                    Field = "Status",
                    Value = ((int)YesNoEnum.Yes).ToString(),
                    Method = SearchEnum.Equal
                },
            ]
        };

        if (!string.IsNullOrWhiteSpace(companyId))
            searchDictionaryArgs.AddArgs("CompanyId", companyId, SearchEnum.Equal);

        var dictionary =
            await dictionaryInvoke.InvokePostAsync("Auth", "/BaseDictionary/Page", searchDictionaryArgs, token);

        if (!dictionary.Success)
            return dictionary;

        if (dictionary.Rows is null)
            return new PageResult<BaseDictionaryResult>(500, "字典不存在");

        return dictionary;
    }

    /// <summary>
    /// </summary>
    /// <param name="cache">缓存器，用于处理缓存信息</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="table">数据表名称</param>
    /// <returns>处理结果</returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache,
        string companyId,
        string table)
    {
        return await cache.NextIdentity(new NextIdentifyArg
        {
            CompanyId = companyId,
            Name = table
        });
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
    /// <returns>处理结果</returns>
    public static BaseUserInfo InitUser(this IHttpContextAccessor httpContextAccessor)
    {
        // 创建用户信息对象
        var user = new BaseUserInfo();

        // 获取HTTP上下文
        var context = httpContextAccessor.HttpContext;

        // 从请求头中获取Token
        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        // 从Claims中获取用户数据
        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        // 如果存在用户数据，则反序列化为用户信息对象
        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        // 设置用户Token
        user.Token = token;

        // 返回初始化后的用户信息
        return user;
    }
}