// 引入必要的命名空间
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

namespace ViteCent.Basic.Application;

/// <summary>
/// 基础应用服务类，提供公司、部门、用户等基础数据的验证和检查功能
/// </summary>
public static class BaseAppliction
{
    /// <summary>
    /// 检查单个公司的有效性
    /// </summary>
    /// <param name="companyInvoke">公司服务调用接口</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回公司信息的数据结果，如果公司不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<DataResult<BaseCompanyResult>> CheckCompany(
        this IBaseInvoke<GetBaseCompanyArgs, DataResult<BaseCompanyResult>> companyInvoke,
        string companyId,
        string token)
    {
        // 验证公司标识是否为空
        if (string.IsNullOrWhiteSpace(companyId))
            return new DataResult<BaseCompanyResult>();

        // 构造获取公司信息的参数
        var getCompanyArgs = new GetBaseCompanyArgs
        {
            Id = companyId
        };

        // 调用服务获取公司信息
        var company = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Get", getCompanyArgs, token);

        // 如果调用失败，直接返回错误信息
        if (!company.Success)
            return company;

        // 检查公司是否存在
        if (company.Data is null)
            return new DataResult<BaseCompanyResult>(500, "公司不存在");

        // 检查公司是否被禁用
        if (company.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseCompanyResult>(500, "公司已禁用");

        return company;
    }

    /// <summary>
    /// 批量检查多个公司的有效性
    /// </summary>
    /// <param name="companyInvoke">公司服务调用接口</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回公司信息的分页结果，如果任一公司不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<PageResult<BaseCompanyResult>> CheckCompanys(
        this IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
        List<string> companyIds,
        string token)
    {
        // 移除空的公司标识
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        // 如果没有有效的公司标识，直接返回空结果
        if (companyIds.Count == 0)
            return new PageResult<BaseCompanyResult>();

        // 构造查询公司列表的参数
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

        // 调用服务获取公司列表
        var companys = await companyInvoke.InvokePostAsync("Auth", "/BaseCompany/Page", searchCompanyArgs, token);

        // 如果调用失败，直接返回错误信息
        if (!companys.Success)
            return companys;

        // 检查是否找到任何公司
        if (companys.Total == 0)
            return new PageResult<BaseCompanyResult>(500, $"公司{companyIds.FirstOrDefault()}不存在");

        // 获取查询结果中的公司标识列表
        var _companyIds = companys.Rows.Select(y => y.Id).ToList();
        // 查找是否有未找到的公司标识
        var _companyId = companyIds.FirstOrDefault(x => !_companyIds.Contains(x));

        // 如果存在未找到的公司标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_companyId))
            return new PageResult<BaseCompanyResult>(500, $"公司{_companyId}不存在");

        // 检查是否有被禁用的公司
        var company = companys.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在被禁用的公司，返回错误信息
        if (company is not null)
            return new PageResult<BaseCompanyResult>(500, $"公司{company.Name}已经禁用");

        return companys;
    }

    /// <summary>
    /// 检查单个部门的有效性
    /// </summary>
    /// <param name="departmentInvoke">部门服务调用接口</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="departmentId">部门标识</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回部门信息的数据结果，如果部门不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<DataResult<BaseDepartmentResult>> CheckDepartment(
        this IBaseInvoke<GetBaseDepartmentArgs, DataResult<BaseDepartmentResult>> departmentInvoke,
        string companyId, string departmentId, string token)
    {
        // 验证部门标识是否为空
        if (string.IsNullOrWhiteSpace(departmentId))
            return new DataResult<BaseDepartmentResult>();

        // 构造获取部门信息的参数
        var getDepartmentArgs = new GetBaseDepartmentArgs
        {
            CompanyId = companyId,  // 设置公司标识
            Id = departmentId       // 设置部门标识
        };

        // 调用服务获取部门信息
        var department =
            await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Get", getDepartmentArgs, token);

        // 如果调用失败，直接返回错误信息
        if (!department.Success)
            return department;

        // 检查部门是否存在
        if (department.Data is null)
            return new DataResult<BaseDepartmentResult>(500, "部门不存在");

        // 检查部门是否被禁用
        if (department.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseDepartmentResult>(500, "部门已禁用");

        return department;
    }

    /// <summary>
    /// 批量检查多个部门的有效性
    /// </summary>
    /// <param name="departmentInvoke">部门服务调用接口</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="departmentIds">部门标识列表</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回部门信息的分页结果，如果任一部门不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<PageResult<BaseDepartmentResult>> CheckDepartments(
        this IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke,
        List<string> companyIds,
        List<string> departmentIds,
        string token)
    {
        // 移除空的公司标识和部门标识
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        departmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        // 如果没有有效的公司标识和部门标识，直接返回空结果
        if (companyIds.Count == 0 && departmentIds.Count == 0)
            return new PageResult<BaseDepartmentResult>();

        // 构造查询部门列表的参数
        var searchDepartmentArgs = new SearchBaseDepartmentArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 如果有公司标识，添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchDepartmentArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 如果有部门标识，添加部门标识筛选条件
        if (departmentIds.Count > 0)
            searchDepartmentArgs.AddArgs("Id", departmentIds.ToJson(), SearchEnum.In);

        // 调用服务获取部门列表
        var departments =
            await departmentInvoke.InvokePostAsync("Auth", "/BaseDepartment/Page", searchDepartmentArgs, token);

        // 如果调用失败，直接返回错误信息
        if (!departments.Success)
            return departments;

        // 检查是否找到任何部门
        if (departments.Total == 0)
            return new PageResult<BaseDepartmentResult>(500, $"部门{departmentIds.FirstOrDefault()}不存在");

        // 获取查询结果中的部门标识列表
        var _departmentIds = departments.Rows.Select(y => y.Id).ToList();
        // 查找是否有未找到的部门标识
        var _departmentId = departmentIds.FirstOrDefault(x => !_departmentIds.Contains(x));

        // 如果存在未找到的部门标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_departmentId))
            return new PageResult<BaseDepartmentResult>(500, $"部门{_departmentId}不存在");

        // 检查是否有被禁用的部门
        var department = departments.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在被禁用的部门，返回错误信息
        if (department is not null)
            return new PageResult<BaseDepartmentResult>(500, $"部门{department.Name}已经禁用");

        return departments;
    }

    /// <summary>
    /// 检查单个用户的有效性
    /// </summary>
    /// <param name="userInvoke">用户服务调用接口</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="departmentId">部门标识</param>
    /// <param name="positionId">职位标识</param>
    /// <param name="userId">用户标识</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回用户信息的数据结果，如果用户不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<DataResult<BaseUserResult>> CheckUser(
        this IBaseInvoke<GetBaseUserArgs, DataResult<BaseUserResult>> userInvoke,
        string companyId,
        string departmentId,
        string positionId,
        string userId,
        string token)
    {
        // 验证用户标识是否为空
        if (string.IsNullOrWhiteSpace(userId))
            return new DataResult<BaseUserResult>();

        // 构造获取用户信息的参数
        var getUserArgs = new GetBaseUserArgs
        {
            CompanyId = companyId,      // 设置公司标识
            DepartmentId = departmentId, // 设置部门标识
            PositionId = positionId,     // 设置职位标识
            Id = userId                  // 设置用户标识
        };

        // 调用服务获取用户信息
        var user = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Get", getUserArgs, token);

        // 如果调用失败，直接返回错误信息
        if (!user.Success)
            return user;

        // 检查用户是否存在
        if (user.Data is null)
            return new DataResult<BaseUserResult>(500, "用户不存在");

        // 检查用户是否被禁用
        if (user.Data.Status == (int)StatusEnum.Disable)
            return new DataResult<BaseUserResult>(500, "用户已禁用");

        return user;
    }

    /// <summary>
    /// 批量检查多个用户的有效性
    /// </summary>
    /// <param name="userInvoke">用户服务调用接口</param>
    /// <param name="companyIds">公司标识列表</param>
    /// <param name="departmentIds">部门标识列表</param>
    /// <param name="positionIds">职位标识列表</param>
    /// <param name="userIds">用户标识列表</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回用户信息的分页结果，如果任一用户不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<PageResult<BaseUserResult>> CheckUsers(
        this IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke,
        List<string> companyIds,
        List<string> departmentIds,
        List<string> positionIds,
        List<string> userIds,
        string token)
    {
        // 移除所有空的标识
        companyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        departmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        positionIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        userIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        // 如果没有有效的查询条件，直接返回空结果
        if (companyIds.Count == 0 && departmentIds.Count == 0 && userIds.Count == 0)
            return new PageResult<BaseUserResult>();

        // 构造查询用户列表的参数
        var searchUserArgs = new SearchBaseUserArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
            Args = []
        };

        // 添加公司标识筛选条件
        if (companyIds.Count > 0)
            searchUserArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        // 添加部门标识筛选条件
        if (departmentIds.Count > 0)
            searchUserArgs.AddArgs("DepartmentId", departmentIds.ToJson(), SearchEnum.In);

        // 添加职位标识筛选条件
        if (positionIds.Count > 0)
            searchUserArgs.AddArgs("PositionId", positionIds.ToJson(), SearchEnum.In);

        // 添加用户标识筛选条件
        if (userIds.Count > 0)
            searchUserArgs.AddArgs("Id", userIds.ToJson(), SearchEnum.In);

        // 调用服务获取用户列表
        var users = await userInvoke.InvokePostAsync("Auth", "/BaseUser/Page", searchUserArgs, token);

        // 如果调用失败，直接返回错误信息
        if (!users.Success)
            return users;

        // 检查是否找到任何用户
        if (users.Total == 0)
            return new PageResult<BaseUserResult>(500, $"用户{userIds.FirstOrDefault()}不存在");

        // 获取查询结果中的用户标识列表
        var _userIds = users.Rows.Select(y => y.Id).ToList();
        // 查找是否有未找到的用户标识
        var _userId = userIds.FirstOrDefault(x => !_userIds.Contains(x));

        // 如果存在未找到的用户标识，返回错误信息
        if (!string.IsNullOrWhiteSpace(_userId))
            return new PageResult<BaseUserResult>(500, $"用户{_userId}不存在");

        // 检查是否有被禁用的用户
        var user = users.Rows.FirstOrDefault(x => x.Status == (int)StatusEnum.Disable);

        // 如果存在被禁用的用户，返回错误信息
        if (user is not null)
            return new PageResult<BaseUserResult>(500, $"用户{user?.RealName}已经禁用");

        return users;
    }

    /// <summary>
    /// 获取下一个标识值
    /// </summary>
    /// <param name="cache">缓存接口</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="table">表名</param>
    /// <returns>返回生成的下一个标识值</returns>
    public static async Task<string> GetIdAsync(this IBaseCache cache,
        string companyId,
        string table)
    {
        // 调用缓存接口获取下一个标识值
        return await cache.NextIdentity(new NextIdentifyArg
        {
            CompanyId = companyId,  // 设置公司标识
            Name = table            // 设置表名
        });
    }

    /// <summary>
    /// 从HTTP上下文中初始化用户信息
    /// </summary>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <returns>返回初始化的用户信息对象</returns>
    public static BaseUserInfo InitUser(this IHttpContextAccessor httpContextAccessor)
    {
        // 创建新的用户信息对象
        var user = new BaseUserInfo();

        // 获取HTTP上下文
        var context = httpContextAccessor.HttpContext;

        // 从请求头中获取Token
        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        // 从Claims中获取用户数据
        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        // 如果存在用户数据，反序列化为用户信息对象
        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        // 设置用户Token
        user.Token = token;

        return user;
    }

    /// <summary>
    /// 检查单个字典的有效性
    /// </summary>
    /// <param name="dictionaryInvoke">字典服务调用接口</param>
    /// <param name="companyId">公司标识</param>
    /// <param name="key">字典标识</param>
    /// <param name="token">认证令牌</param>
    /// <returns>返回字典信息的数据结果，如果字典不存在或已禁用则返回相应的错误信息</returns>
    public static async Task<PageResult<BaseDictionaryResult>> GetDictionary(
        this IBaseInvoke<SearchBaseDictionaryArgs, PageResult<BaseDictionaryResult>> dictionaryInvoke,
        string companyId, string key, string token)
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
}