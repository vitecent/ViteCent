#region

using MediatR;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseOperation;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseOperationEntityArgs : SearchArgs, IRequest<List<BaseOperationEntity>>
{
}