#region

using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Files.Api.FileApi;

/// <summary>
/// </summary>
/// <param name="configuration"></param>
[ApiController]
[Route("File")]
public class UploadFile(IConfiguration configuration) : BaseFileApi<IFormFile, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("UploadFile")]
    public override async Task<BaseResult> InvokeAsync(IFormFile file)
    {
        if (file == null) return new BaseResult(73001, "文件不能为空");

        var root = $"{Environment.CurrentDirectory}/wwwroot";
        var dir = $"/UploadFile/{DateTime.Now:yyyyMMdd}";
        var path = $"{root}{dir}";

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var extension = Path.GetExtension(file.FileName).ToLower();

        if (string.IsNullOrWhiteSpace(extension)) return new BaseResult(73002, "文件格式错误");

        var extensions = configuration["Upload:Extension"]?.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        if (!extensions?.Any(x => x == extension) ?? true) return new BaseResult(73003, "文件格式错误");

        var size = file.Length;
        var maxSize = Convert.ToInt32(configuration["Upload:Size"]) * 1024 * 1024;

        if (size > maxSize) return new BaseResult(73004, $"文件最大为{maxSize}M");

        var name = Guid.NewGuid().ToString("N") + extension;

        using var stream = file.OpenReadStream();
        using var saveStream = new FileStream($"{path}/{name}", FileMode.OpenOrCreate);
        await stream.CopyToAsync(saveStream);

        return new BaseResult($"{dir}/{name}");
    }
}