#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Fiels.Data;

#endregion

namespace ViteCent.Files.Api.FileApi;

/// <summary>
/// 文件删除接口
/// </summary>
[ApiController]
[Route("File")]
public class DeleteFile : BaseApi<GetFileArgs, BaseResult>
{
    /// <summary>
    /// 删除指定路径的文件
    /// </summary>
    /// <param name="args">文件参数，包含要删除的文件路径</param>
    /// <returns>删除操作的结果，成功返回空字符串，失败返回错误信息</returns>
    [HttpPost]
    [Route("DeleteFile")]
    public override async Task<BaseResult> InvokeAsync([FromBody] GetFileArgs args)
    {
        // 获取网站根目录路径
        var root = $"{Environment.CurrentDirectory}/wwwroot";

        // 检查文件是否存在
        var exist = await new FileExist().InvokeAsync(args);

        // 如果文件不存在，直接返回错误信息
        if (!exist.Success) return exist;

        // 删除指定路径的文件
        BaseFile.Delete($"{root}/{args.Path}");

        // 删除成功，返回空字符串
        return new BaseResult(string.Empty);
    }
}