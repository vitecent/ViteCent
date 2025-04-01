#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// </summary>
[Serializable]
public class SearchBasePositionArgs : SearchArgs, IRequest<PageResult<BasePositionResult>>
{
}