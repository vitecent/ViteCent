/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Microsoft.Extensions.Logging 用于日志记录
using Microsoft.Extensions.Logging;

// 引入公司信息相关的数据模型
using ViteCent.Auth.Entity.BaseCompany;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Auth.Domain.BaseCompany;

/// <summary>
/// 删除公司信息领域服务类
/// </summary>
/// <remarks>
/// 该类负责处理公司信息的删除操作，继承自 BaseDomain 基类并实现 IRequestHandler 接口
/// 通过依赖注入方式接收日志记录器，用于记录操作日志
/// </remarks>
/// <param name="logger">日志记录器实例</param>
public class DeleteBaseCompany(
    // 注入日志记录器
    ILogger<DeleteBaseCompany> logger)
    : BaseDomain<DeleteBaseCompanyEntity>, IRequestHandler<DeleteBaseCompanyEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 处理删除公司信息的请求
    /// </summary>
    /// <remarks>
    /// 实现 IRequestHandler 接口的 Handle 方法
    /// 记录操作日志并调用基类的 DeleteAsync 方法执行实际的删除操作
    /// </remarks>
    /// <param name="request">包含要删除的公司信息的请求模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回删除操作的结果</returns>
    public async Task<BaseResult> Handle(DeleteBaseCompanyEntity request, CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseCompany.DeleteBaseCompany");

        // 调用基类的删除方法执行实际的删除操作
        return await DeleteAsync(request);
    }
}