#region

using ViteCent.Core.Data;
using ViteCent.Core.Elasticsearch;

#endregion

namespace ViteCent.Core.Test;

/// <summary>
/// </summary>
[TestClass]
public sealed class TestElasticsearch
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestElasticsearchMethod()
    {
        var factory = new ElasticsearchFactory("http://192.168.0.115:9200", "test_index");
        //var created = await factory.CreateIndexAsync("test_index");
        //Assert.IsTrue(created);

        var exists = await factory.IndexExistsAsync("test_index");
        Assert.IsTrue(exists);

        var document = await factory.IndexDocumentAsync("test_index", new BaseUserInfo() { Id = "99", Name = "test" });
        Assert.IsTrue(document);

        var get = await factory.GetDocumentAsync<BaseUserInfo>("99", "test_index");
        Assert.AreEqual("test", get.Name);
    }
}