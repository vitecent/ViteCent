#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseSystemArgs : SearchArgs, IRequest<PageResult<BaseSystemResult>>
{
}