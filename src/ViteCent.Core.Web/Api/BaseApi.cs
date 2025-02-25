#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Api;

/// <summary>
/// </summary>
/// <typeparam name="Args"></typeparam>
/// <typeparam name="Result"></typeparam>
public abstract class BaseApi<Args, Result> : ControllerBase
    where Args : BaseArgs
    where Result : BaseResult
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public abstract Task<Result> InvokeAsync(Args args);
}