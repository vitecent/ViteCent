#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Files.Api.FileApi;

/// <summary>
/// 文件上传接口，处理文件上传请求并保存到指定目录
/// </summary>
/// <param name="configuration">配置对象，用于获取文件上传的相关配置（如允许的文件扩展名和大小限制）</param>
[ApiController]
[Route("File")]
public class UploadFile(IConfiguration configuration) : BaseFileApi<IFormFile, BaseResult>
{
    /// <summary>
    /// 处理文件上传的方法
    /// </summary>
    /// <param name="file">上传的文件对象，包含文件内容和元数据</param>
    /// <returns>返回上传结果，成功返回文件相对路径，失败返回错误信息</returns>
    [HttpPost]
    [Route("UploadFile")]
    public override async Task<BaseResult> InvokeAsync(IFormFile file)
    {
        // 验证文件是否为空
        if (file is null) return new BaseResult(73001, "文件不能为空");

        // 构建文件保存路径
        var root = $"{Environment.CurrentDirectory}/wwwroot";
        var dir = $"/UploadFile/{DateTime.Now:yyyyMMdd}";
        var path = $"{root}{dir}";

        // 如果目录不存在则创建
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        // 获取并验证文件扩展名
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (string.IsNullOrWhiteSpace(extension)) return new BaseResult(73002, "文件格式错误");

        // 从配置中获取允许的文件扩展名列表
        var extensions = configuration["Upload:Extension"]?.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        // 验证文件扩展名是否在允许列表中
        if (!extensions?.Any(x => x == extension) ?? true) return new BaseResult(73003, "文件格式错误");

        // 验证文件大小是否超过限制
        var size = file.Length;
        var maxSize = Convert.ToInt32(configuration["Upload:Size"]) * 1024 * 1024;

        if (size > maxSize) return new BaseResult(73004, $"文件最大为{maxSize}M");

        // 生成唯一的文件名并保存文件
        var name = Guid.NewGuid().ToString("N") + extension;

        using var stream = file.OpenReadStream();
        using var saveStream = new FileStream($"{path}/{name}", FileMode.OpenOrCreate);
        await stream.CopyToAsync(saveStream);

        // 返回文件的相对路径
        return new BaseResult($"{dir}/{name}");
    }
}