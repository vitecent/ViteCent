#region

using AutoMapper;
using System.Reflection.Metadata;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public abstract class BaseMapperConfig : Profile
{
    /// <summary>
    /// </summary>
    public BaseMapperConfig()
    {
        Map();
    }

    /// <summary>
    /// </summary>
    public abstract void Map();
}