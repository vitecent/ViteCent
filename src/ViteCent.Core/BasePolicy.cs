#region

using Polly;
using Polly.Timeout;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class BasePolicy<T>
    where T : class
{
    /// <summary>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="timeOut"></param>
    /// <param name="breaker"></param>
    /// <param name="breakTimes"></param>
    /// <param name="retryTimes"></param>
    /// <returns></returns>
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