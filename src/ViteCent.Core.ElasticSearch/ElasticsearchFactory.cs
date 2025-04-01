#region

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.QueryDsl;
using log4net;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Elasticsearch;

/// <summary>
/// </summary>
public class ElasticsearchFactory : IElasticsearch
{
    /// <summary>
    /// </summary>
    private readonly ElasticsearchClient client;

    /// <summary>
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// </summary>
    /// <param name="url"></param>
    /// <param name="index"></param>
    public ElasticsearchFactory(string url, string index = "default_index")
    {
        logger = new BaseLogger(typeof(ElasticsearchFactory));

        client = new(new ElasticsearchClientSettings(new Uri(url))
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
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task<bool> CreateIndexAsync(string index)
    {
        var response = await client.Indices.CreateAsync(index);

        return response.IsValidResponse;
    }

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task<bool> DeleteIndexAsync(string index)
    {
        var response = await client.Indices.DeleteAsync(index);

        return response.IsValidResponse;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task<T> GetDocumentAsync<T>(string id, string index) where T : class, new()
    {
        var response = await client.GetAsync<T>(id, idx => idx.Index(index));

        return response.Source ?? default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="document"></param>
    /// <returns></returns>
    public async Task<bool> IndexDocumentAsync<T>(string index, T document) where T : class, new()
    {
        var response = await client.IndexAsync(document, idx => idx.Index(index));

        return response.IsValidResponse;
    }

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="documents"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task<bool> IndexExistsAsync(string index)
    {
        var response = await client.Indices.ExistsAsync(index);

        return response.Exists;
    }

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="Query"></param>
    /// <param name="Sort"></param>
    /// <returns></returns>
    public async Task<PageResult<T>> PageDocumentsAsync<T>(string index, int pageNumber = 1,
        int pageSize = 10, Action<QueryDescriptor<T>>? Query = null,
        Action<SortOptionsDescriptor<T>>? Sort = null) where T : class, new()
    {
        var response = await client.SearchAsync<T>(s => s
            .Index(index)
            .From((pageNumber - 1) * pageSize)
            .Size(pageSize)
            .Query(q => Query?.Invoke(q))
            .Sort(so => Sort?.Invoke(so))
        );

        return new PageResult<T>(pageNumber, pageSize, (int)response.Total, [.. response.Documents]);
    }

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task<PageResult<T>> SearchDocumentsAsync<T>(string index) where T : class, new()
    {
        var response = await client.SearchAsync<T>(s =>
        {
            s.Index(index);
        });

        if (!response.IsValidResponse)
        {
            throw new Exception($"Search Failed {response.DebugInformation}");
        }

        return new PageResult<T>(1, int.MaxValue, (int)response.Total, [.. response.Documents]); ;
    }
}