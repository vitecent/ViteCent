#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Files.Api.Data;

#endregion

namespace ViteCent.Files.Api.FileApi;

/// <summary>
/// 文件存在性检查接口
/// </summary>
[ApiController]
[Route("File")]
public class FileExist : BaseApi<GetFileArgs, BaseResult>
{
    /// <summary>
    /// 检查指定路径的文件是否存在
    /// </summary>
    /// <param name="args">文件参数，包含要检查的文件路径</param>
    /// <returns>检查结果，文件存在返回空字符串，不存在返回404错误信息</returns>
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