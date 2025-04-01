#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Files.Api.Data;


#endregion

namespace ViteCent.Files.Api.FileApi;

/// <summary>
/// </summary>
[ApiController]
[Route("File")]
public class FileExist : BaseApi<GetFileArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FileExist")]
    public override async Task<BaseResult> InvokeAsync([FromBody] GetFileArgs args)
    {
        return await Task.Run(() =>
        {
            var root = $"{Environment.CurrentDirectory}/wwwroot";

            if (System.IO.File.Exists($"{root}/{args.Path}")) return new BaseResult(string.Empty);

            return new BaseResult(404, $"文件{args.Path}不存在");
        });
    }
}