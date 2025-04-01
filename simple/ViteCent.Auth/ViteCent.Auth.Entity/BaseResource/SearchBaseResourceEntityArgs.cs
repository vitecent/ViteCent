#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseResource;

/// <summary>
/// 搜索资源信息数据参数
/// </summary>
[Serializable]
public class SearchBaseResourceEntityArgs : SearchArgs, IRequest<List<BaseResourceEntity>>
{
}