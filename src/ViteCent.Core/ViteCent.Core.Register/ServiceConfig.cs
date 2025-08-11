namespace ViteCent.Core.Register;

/// <summary>
/// 服务配置类，用于配置服务注册相关信息
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// 服务地址
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// 健康检查地址
    /// </summary>
    public string Check { get; set; } = string.Empty;

    /// <summary>
    /// 服务注销时间（单位：秒）
    /// </summary>
    public int Deregister { get; set; }

    /// <summary>
    /// 服务唯一标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 是否使用HTTPS协议
    /// </summary>
    public bool IsHttps { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 服务端口号
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 超时时间（单位：秒）
    /// </summary>
    public int Timeout { get; set; }
}