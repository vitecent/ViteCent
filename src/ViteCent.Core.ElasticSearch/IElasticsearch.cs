#region

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Elasticsearch;

/// <summary>
/// Elasticsearch操作接口，定义了基本的索引和文档管理功能
/// </summary>
public interface IElasticsearch
{
    /// <summary>
    /// 创建Elasticsearch索引
    /// </summary>
    /// <param name="index">索引名称</param>
    /// <returns>创建是否成功</returns>
    Task<bool> CreateIndexAsync(string index);

    /// <summary>
    /// 删除Elasticsearch索引
    /// </summary>
    /// <param name="index">索引名称</param>
    /// <returns>删除是否成功</returns>
    Task<bool> DeleteIndexAsync(string index);

    /// <summary>
    /// 根据标识获取文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="id">文档标识</param>
    /// <param name="index">索引名称</param>
    /// <returns>文档对象，如果不存在则返回null</returns>
    Task<T> GetDocumentAsync<T>(string id, string index) where T : class, new();

    /// <summary>
    /// 索引单个文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <param name="document">要索引的文档对象</param>
    /// <returns>索引是否成功</returns>
    Task<bool> IndexDocumentAsync<T>(string index, T document) where T : class, new();

    /// <summary>
    /// 批量索引多个文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <param name="documents">要索引的文档对象列表</param>
    /// <returns>批量索引是否成功</returns>
    Task<bool> IndexDocumentsAsync<T>(string index, List<T> documents) where T : class, new();

    /// <summary>
    /// 检查索引是否存在
    /// </summary>
    /// <param name="index">索引名称</param>
    /// <returns>索引是否存在</returns>
    Task<bool> IndexExistsAsync(string index);

    /// <summary>
    /// 分页查询文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <param name="pageNumber">页码，从1开始</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="Query">查询条件构建器</param>
    /// <param name="Sort">排序条件构建器</param>
    /// <returns>分页查询结果</returns>
    Task<PageResult<T>> PageDocumentsAsync<T>(string index, int pageNumber = 1, int pageSize = 10,
        Action<QueryDescriptor<T>>? Query = null,
        Action<SortOptionsDescriptor<T>>? Sort = null) where T : class, new();

    /// <summary>
    /// 搜索文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <returns>搜索结果</returns>
    Task<PageResult<T>> SearchDocumentsAsync<T>(string index) where T : class, new();
}