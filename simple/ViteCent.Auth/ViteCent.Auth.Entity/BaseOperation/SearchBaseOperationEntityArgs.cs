#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseOperation;

/// <summary>
/// 搜索操作信息数据参数
/// </summary>
[Serializable]
public class SearchBaseOperationEntityArgs : SearchArgs, IRequest<List<BaseOperationEntity>>
{
}