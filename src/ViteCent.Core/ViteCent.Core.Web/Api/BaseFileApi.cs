#region

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Api;

/// <summary>
/// </summary>
public abstract class BaseFileApi<Args, Result> : ControllerBase
    where Args : IFormFile
    where Result : BaseResult
{
    /// <summary>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public abstract Task<Result> InvokeAsync(Args file);
}