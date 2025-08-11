#region

using System.Text;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core;

/// <summary>
/// HTTP客户端工具类，提供基础的HTTP请求功能
/// </summary>
/// <typeparam name="T">返回结果类型，必须继承自BaseResult类</typeparam>
public class BaseHttpClient<T> where T : BaseResult
{
    /// <summary>
    /// 发送GET请求的方法
    /// </summary>
    /// <param name="uri">请求的URL地址</param>
    /// <param name="token">身份验证令牌，可选参数</param>
    /// <returns>返回指定类型T的响应结果</returns>
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
    /// 发送POST请求的方法
    /// </summary>
    /// <param name="uri">请求的URL地址</param>
    /// <param name="args">请求参数对象，包含需要发送的数据</param>
    /// <param name="token">身份验证令牌，可选参数</param>
    /// <returns>返回指定类型T的响应结果</returns>
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