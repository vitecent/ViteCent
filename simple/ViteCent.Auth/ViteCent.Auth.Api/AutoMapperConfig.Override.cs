#region

using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Domain.BaseUser;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Auth.Api;

/// <summary>
/// </summary>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// </summary>
    private void OverrideMap()
    {
        CreateMap<InitializeArgs, AddBaseUserArgs>();
        CreateMap<LoginArgs, LoginEntityArgs>();
    }
}