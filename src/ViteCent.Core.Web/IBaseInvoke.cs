#region

using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
/// <typeparam name="Args"></typeparam>
/// <typeparam name="Result"></typeparam>
public interface IBaseInvoke<Args, Result> where Args : BaseArgs where Result : BaseResult
{
    /// <summary>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="api"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Result> InvokeGetMethodAsync(string service, string api, string token = "");

    /// <summary>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="api"></param>
    /// <param name="args"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Result> InvokePostAsync(string service, string api, Args args, string token = "");
}