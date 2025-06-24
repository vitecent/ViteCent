/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region 命名空间引用

// MediatR中介者模式接口，用于处理请求和响应
using MediatR;

// 日志记录接口
using Microsoft.Extensions.Logging;

// 公司实体类
using ViteCent.Auth.Entity.BaseCompany;

// 基础数据参数
using ViteCent.Core.Data;

// SqlSugar ORM基础类
using ViteCent.Core.Orm.SqlSugar;

#endregion 命名空间引用

namespace ViteCent.Auth.Domain.BaseCompany;

/// <summary>
/// 批量公司信息判重处理类
/// </summary>
/// <remarks>
/// 该类用于处理批量公司信息的判重逻辑，包括：
/// 1. 检查公司编码是否重复
/// 2. 检查公司名称是否重复
/// 3. 返回判重结果
/// </remarks>
/// <param name="logger">日志记录器实例</param>
public class HasBaseCompanyList(ILogger<HasBaseCompanyList> logger)
    : BaseDomain<BaseCompanyEntity>, IRequestHandler<HasBaseCompanyEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    /// <remarks>指定当前领域模型使用的数据库名称 用于在多数据库环境中确定操作的目标数据库</remarks>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// 处理批量公司信息判重请求
    /// </summary>
    /// <param name="request">包含待检查的公司编码和名称列表的请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>判重结果，包含状态码和提示信息</returns>
    /// <remarks>
    /// 该方法执行以下步骤：
    /// 1. 记录方法调用日志
    /// 2. 构建查询条件，检查编码和名称是否存在重复
    /// 3. 执行查询并返回结果
    /// </remarks>
    public async Task<BaseResult> Handle(HasBaseCompanyEntityListArgs request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseCompany.HasBaseCompany");

        // 初始化查询对象
        var query = Client.Query<BaseCompanyEntity>();

        // 如果请求中包含多个编码，添加编码匹配条件
        if (request.Codes.Count > 1)
            query = query.Where(x => request.Codes.Contains(x.Code));

        // 如果请求中包含多个名称，添加名称匹配条件
        if (request.Names.Count > 1)
            query = query.Where(x => request.Names.Contains(x.Name));

        // 执行查询，获取匹配记录数
        var entity = await query.CountAsync(cancellationToken);

        // 如果存在匹配记录，返回错误结果
        if (entity > 0)
            return new BaseResult(500, "编码 或 名称 重复");

        // 无重复记录，返回成功结果
        return new BaseResult();
    }
}