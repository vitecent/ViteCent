#region

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Elasticsearch;

/// <summary>
/// Elasticsearch工厂类，提供Elasticsearch数据操作的实现
/// </summary>
public class ElasticsearchFactory : IElasticsearch
{
    /// <summary>
    /// Elasticsearch客户端实例
    /// </summary>
    private readonly ElasticsearchClient client;

    /// <summary>
    /// 日志记录器实例
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// 初始化Elasticsearch工厂类的新实例
    /// </summary>
    /// <param name="url">Elasticsearch服务器的URL地址</param>
    /// <param name="index">默认索引名称，默认值为"default_index"</param>
    public ElasticsearchFactory(string url, string index = "default_index")
    {
        logger = new BaseLogger(typeof(ElasticsearchFactory));

        client = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri(url))
            .DefaultIndex(index)
            .EnableDebugMode()
            .DisableDirectStreaming()
            .OnRequestCompleted(details =>
            {
                logger.LogInformation($"Request {details.RequestBodyInBytes?.ByteToString()}");
                logger.LogInformation($"Response {details.ResponseBodyInBytes?.ByteToString()}");
            }));
    }

    /// <summary>
    /// 创建Elasticsearch索引
    /// </summary>
    /// <param name="index">索引名称</param>
    /// <returns>创建是否成功</returns>
    public async Task<bool> CreateIndexAsync(string index)
    {
        var response = await client.Indices.CreateAsync(index);

        return response.IsValidResponse;
    }

    /// <summary>
    /// 删除Elasticsearch索引
    /// </summary>
    /// <param name="index">索引名称</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteIndexAsync(string index)
    {
        var response = await client.Indices.DeleteAsync(index);

        return response.IsValidResponse;
    }

    /// <summary>
    /// 根据标识获取文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="id">文档标识</param>
    /// <param name="index">索引名称</param>
    /// <returns>文档对象，如果不存在则返回null</returns>
    public async Task<T> GetDocumentAsync<T>(string id, string index) where T : class, new()
    {
        var response = await client.GetAsync<T>(id, idx => idx.Index(index));

        return response.Source ?? default!;
    }

    /// <summary>
    /// 索引单个文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <param name="document">要索引的文档对象</param>
    /// <returns>索引是否成功</returns>
    public async Task<bool> IndexDocumentAsync<T>(string index, T document) where T : class, new()
    {
        var response = await client.IndexAsync(document, idx => idx.Index(index));

        return response.IsValidResponse;
    }

    /// <summary>
    /// 批量索引多个文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <param name="documents">要索引的文档对象列表</param>
    /// <returns>批量索引是否成功</returns>
    public async Task<bool> IndexDocumentsAsync<T>(string index, List<T> documents) where T : class, new()
    {
        var bulkRequest = new BulkRequest(index)
        {
            Operations = new List<IBulkOperation>()
        };

        foreach (var doc in documents)
        {
            var op = new BulkIndexOperation<T>(doc);
            bulkRequest.Operations.Add(op);
        }

        var response = await client.BulkAsync(bulkRequest);

        return response.IsValidResponse;
    }

    /// <summary>
    /// 检查索引是否存在
    /// </summary>
    /// <param name="index">索引名称</param>
    /// <returns>索引是否存在</returns>
    public async Task<bool> IndexExistsAsync(string index)
    {
        var response = await client.Indices.ExistsAsync(index);

        return response.Exists;
    }

    /// <summary>
    /// 分页查询文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <param name="pageNumber">页码，默认为1</param>
    /// <param name="pageSize">每页大小，默认为10</param>
    /// <param name="Query">查询条件构建器</param>
    /// <param name="Sort">排序选项构建器</param>
    /// <returns>分页结果对象</returns>
    public async Task<PageResult<T>> PageDocumentsAsync<T>(string index, int pageNumber = 1,
        int pageSize = 10, Action<QueryDescriptor<T>>? Query = null,
        Action<SortOptionsDescriptor<T>>? Sort = null) where T : class, new()
    {
        var response = await client.SearchAsync<T>(s => s
            .Indices(index)
            .From((pageNumber - 1) * pageSize)
            .Size(pageSize)
            .Query(q => Query?.Invoke(q))
            .Sort(so => Sort?.Invoke(so))
        );

        return new PageResult<T>(pageNumber, pageSize, (int)response.Total, [.. response.Documents]);
    }

    /// <summary>
    /// 搜索所有文档
    /// </summary>
    /// <typeparam name="T">文档类型</typeparam>
    /// <param name="index">索引名称</param>
    /// <returns>包含所有文档的分页结果对象</returns>
    public async Task<PageResult<T>> SearchDocumentsAsync<T>(string index) where T : class, new()
    {
        var response = await client.SearchAsync<T>(s => { s.Indices(index); });

        if (!response.IsValidResponse) throw new Exception($"Search Failed {response.DebugInformation}");

        return new PageResult<T>(1, int.MaxValue, (int)response.Total, [.. response.Documents]);
    }
}