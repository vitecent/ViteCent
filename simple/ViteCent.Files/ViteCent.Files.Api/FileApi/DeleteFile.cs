#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Files.Api.Data;


#endregion

namespace ViteCent.Files.Api.FileApi;

/// <summary>
/// </summary>
[ApiController]
[Route("File")]
public class DeleteFile : BaseApi<GetFileArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("DeleteFile")]
    public override async Task<BaseResult> InvokeAsync([FromBody] GetFileArgs args)
    {
        var root = $"{Environment.CurrentDirectory}/wwwroot";

        var exist = await new FileExist().InvokeAsync(args);

        if (!exist.Success) return exist;

        BaseFile.Delete($"{root}/{args.Path}");

        return new BaseResult(string.Empty);
    }
}