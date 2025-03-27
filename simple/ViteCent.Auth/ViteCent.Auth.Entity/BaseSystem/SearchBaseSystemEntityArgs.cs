#region

using MediatR;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseSystem;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseSystemEntityArgs : SearchArgs, IRequest<List<BaseSystemEntity>>
{
}