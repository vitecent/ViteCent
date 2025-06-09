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

// 引入字典信息相关的数据模型
using ViteCent.Auth.Entity.BaseDictionary;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Auth.Domain.BaseDictionary;

// <summary>
/// 字典信息分页查询领域服务
/// </summary>
/// <remarks>
/// 该类负责处理字典信息的分页查询请求，主要功能包括：
/// 1. 接收并处理分页查询参数
/// 2. 调用基础设施层执行分页查询
/// 3. 返回符合条件的字典信息列表
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理过程中的关键信息</param>
public class PageBaseDictionary(
    // 注入日志记录器
    ILogger<PageBaseDictionary> logger)
    // 继承基类，指定查询参数和返回结果类型
    : BaseDomain<BaseDictionaryEntity>, IRequestHandler<SearchBaseDictionaryEntityArgs, List<BaseDictionaryEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 处理字典信息分页查询请求
    /// </summary>
    /// <param name="request">分页查询参数，包含页码、每页大小、查询条件等</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>符合条件的字典信息模型列表</returns>
    /// <remarks>
    /// 处理流程：
    /// 1. 记录操作日志
    /// 2. 调用基类的分页查询方法
    /// 3. 返回查询结果
    /// </remarks>
    public async Task<List<BaseDictionaryEntity>> Handle(SearchBaseDictionaryEntityArgs request,
        CancellationToken cancellationToken)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDictionary.PageBaseDictionary");

        // 调用基类的分页查询方法并返回结果
        return await base.PageAsync(request);
    }
}