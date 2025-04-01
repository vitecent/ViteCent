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
    private void OverrideLoad(ContainerBuilder builder)
    {
    }
}