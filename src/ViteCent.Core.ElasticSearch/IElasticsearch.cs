#region

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Elasticsearch;

/// <summary>
/// </summary>
public interface IElasticsearch
{
    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    Task<bool> CreateIndexAsync(string index);

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    Task<bool> DeleteIndexAsync(string index);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    Task<T> GetDocumentAsync<T>(string id, string index) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="document"></param>
    /// <returns></returns>
    Task<bool> IndexDocumentAsync<T>(string index, T document) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="documents"></param>
    /// <returns></returns>
    Task<bool> IndexDocumentsAsync<T>(string index, List<T> documents) where T : class, new();

    /// <summary>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    Task<bool> IndexExistsAsync(string index);

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="Query"></param>
    /// <param name="Sort"></param>
    /// <returns></returns>
    Task<PageResult<T>> PageDocumentsAsync<T>(string index, int pageNumber = 1, int pageSize = 10,
        Action<QueryDescriptor<T>>? Query = null,
        Action<SortOptionsDescriptor<T>>? Sort = null) where T : class, new();

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    Task<PageResult<T>> SearchDocumentsAsync<T>(string index) where T : class, new();
}