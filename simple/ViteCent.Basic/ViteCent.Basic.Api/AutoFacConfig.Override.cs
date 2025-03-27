#region

using Autofac;
using Module = Autofac.Module;

#endregion

namespace ViteCent.Basic.Api;

/// <summary>
/// </summary>
public partial class AutoFacConfig : Module
{
    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    protected void OverrideLoad(ContainerBuilder builder)
    {
    }
}