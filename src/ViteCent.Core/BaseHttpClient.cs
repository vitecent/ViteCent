#region

using System.Text;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class BaseHttpClient<T> where T : BaseResult
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
            var client = new HttpClient();

            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Add(BaseConst.Token, token);

            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

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
            var client = new HttpClient();

            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Add(BaseConst.Token, token);

            var content = new StringContent(args.ToJson(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(data))
                    return data.DeJson<T>();
            }

            return default!;
        });
    }
}