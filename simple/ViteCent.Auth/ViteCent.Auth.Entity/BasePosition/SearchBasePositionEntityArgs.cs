#region

using MediatR;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// </summary>
[Serializable]
public class SearchBasePositionEntityArgs : SearchArgs, IRequest<List<BasePositionEntity>>
{
}