#region

using Replicant;

using System.Text;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class BaseHttpClient<T>
    where T : class
{
    /// <summary>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<T> GetAsync(string uri, string token = "")
    {
        return await BasePolicy<T>.ExecuteAsync(async () =>
        {
            var client = HttpCache.Default;

            var response = await client.ResponseAsync(uri, false, x =>
            {
                if (!string.IsNullOrWhiteSpace(token))
                    x.Headers.Add(Const.Token, token);

                x.Method = HttpMethod.Get;
            });

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(data)) return data.DeJson<T>();
            }

            return default!;
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="args"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<T> PostAsync(string uri, BaseArgs args, string token = "")
    {
        return await BasePolicy<T>.ExecuteAsync(async () =>
        {
            var client = HttpCache.Default;

            var response = await client.ResponseAsync(uri, false, x =>
            {
                if (!string.IsNullOrWhiteSpace(token))
                    x.Headers.Add(Const.Token, token);

                x.Method = HttpMethod.Post;
                x.Content = new StringContent(args.ToJson(), Encoding.UTF8, "application/json");
            });

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(data)) return data.DeJson<T>();
            }

            return default!;
        });
    }
}