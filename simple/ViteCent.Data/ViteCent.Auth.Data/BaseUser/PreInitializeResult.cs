#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class PreInitializeResult
{
    /// <summary>
    /// </summary>
    public bool Flag { get; set; }
}