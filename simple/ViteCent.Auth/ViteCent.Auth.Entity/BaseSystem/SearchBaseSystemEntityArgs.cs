#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseSystem;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseSystemEntityArgs : SearchArgs, IRequest<List<BaseSystemEntity>>
{
}