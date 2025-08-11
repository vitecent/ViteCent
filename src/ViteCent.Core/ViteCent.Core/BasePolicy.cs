#region

using Polly;
using Polly.Timeout;

#endregion

namespace ViteCent.Core;

/// <summary>
/// 基于Polly的策略模式工具类，提供超时重试和熔断等弹性功能
/// </summary>
/// <typeparam name="T">返回结果的类型，必须是引用类型</typeparam>
public class BasePolicy<T>
    where T : class
{
    /// <summary>
    /// 执行带有重试、超时和熔断策略的操作
    /// </summary>
    /// <param name="action">需要执行的操作委托</param>
    /// <param name="timeOut">超时时间（秒），默认15秒</param>
    /// <param name="breaker">触发熔断的连续失败次数，默认2次</param>
    /// <param name="breakTimes">熔断时间（秒），默认30秒</param>
    /// <param name="retryTimes">重试间隔时间列表（秒），默认[5,10,20]秒</param>
    /// <returns>返回泛型类型T的操作结果</returns>
    public static async Task<T> ExecuteAsync(Func<Task<T>> action, int timeOut = 15, int breaker = 2,
        int breakTimes = 30, params List<int> retryTimes)
    {
        var timeoutPolicy = Policy.TimeoutAsync(timeOut);

        if (retryTimes.Count == 0)
            retryTimes = [5, 10, 20];

        var timeSpans = retryTimes.Select(x => TimeSpan.FromSeconds(x)).ToList();

        var retryPolicy = Policy.Handle<Exception>()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(timeSpans);

        var breakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(breaker, TimeSpan.FromSeconds(breakTimes));

        var policyWrap = timeoutPolicy.WrapAsync(retryPolicy).WrapAsync(breakerPolicy);

        return await policyWrap.ExecuteAsync(action);
    }
}