#region

using Autofac;
using System.Reflection;
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
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        var path = AppDomain.CurrentDomain.BaseDirectory;

        var assemblies = new List<Assembly>();

        var dlls = new List<string>() { "*Application.dll", "*Domain.dll" };

        foreach (var dll in dlls)
        {
            var files = Directory.GetFiles(path, dll);

            foreach (var item in files)
            {
                var assembly = Assembly.LoadFrom(item);
                assemblies.Add(assembly);
            }
        }

        builder.RegisterAssemblyTypes([.. assemblies])
            .Where(t => t.IsClass && !t.IsAbstract).AsImplementedInterfaces().InstancePerLifetimeScope();

        OverrideLoad(builder);
    }
}