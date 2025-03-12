#region

using ViteCent.Core.Cache.Redis;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Test;

/// <summary>
/// </summary>
[TestClass]
public sealed class TestCache
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestCacheMethod()
    {
        var cache = new RedisCache("192.168.0.115:6379,password=123456,defaultDatabase=1");

        var args = new NextIdentifyArg()
        {
            CompanyId = "1",
            Name = "Test",
        };

        var result = await cache.NextIdentity(args);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Length > 0);
    }
}