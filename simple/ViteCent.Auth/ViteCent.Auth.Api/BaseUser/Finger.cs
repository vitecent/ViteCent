#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 注册指纹接口
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class Finger(
    ILogger<Finger> logger,
    IBaseCache cache) : BaseLoginApi<FingerArgs, DataResult<FingerResult>>
{
    /// <summary>
    /// 注册指纹
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Finger")]
    public override async Task<DataResult<FingerResult>> InvokeAsync(FingerArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Finger");

        if (!cache.HasKey("RegisterFinger"))
            return new DataResult<FingerResult>(500, "请先录入指纹信息");

        var finger = cache.GetString<object>("RegisterFinger");

        if (finger == null)
            return new DataResult<FingerResult>(500, "指纹信息不存在");

        var result = new DataResult<FingerResult>()
        {
            Data = new FingerResult()
            {
                Template = finger
            }
        };

        return await Task.FromResult(result);
    }
}