using ViteCent.Core.Enums;

namespace ViteCent.Core.Data;

/// <summary>
/// 自增标识生成参数类，用于配置生成自增标识的相关参数
/// </summary>
public class NextIdentifyArg : BaseArgs
{
    /// <summary>
    /// 公司标识，用于指定生成标识所属的公司
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 生成数量，指定需要生成的标识数量，默认为1
    /// </summary>
    public int Count { get; set; } = 1;

    /// <summary>
    /// 标识名称，用于区分不同业务场景的标识
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 标识前缀，生成的标识将以此为开头
    /// </summary>
    public string Prefix { get; set; } = "";

    /// <summary>
    /// 标识后缀，生成的标识将以此为结尾
    /// </summary>
    public string Suffix { get; set; } = "";

    /// <summary>
    /// 标识生成类型，指定标识的生成规则（如按天、按月等）
    /// </summary>
    public IdentifyEnum Type { get; set; } = IdentifyEnum.Day;
}