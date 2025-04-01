#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// 搜索操作信息参数
/// </summary>
[Serializable]
public class SearchBaseOperationArgs : SearchArgs, IRequest<PageResult<BaseOperationResult>>
{
}