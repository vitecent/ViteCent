#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseResource;

/// <summary>
/// 搜索资源信息参数
/// </summary>
[Serializable]
public class SearchBaseResourceArgs : SearchArgs, IRequest<PageResult<BaseResourceResult>>
{
}