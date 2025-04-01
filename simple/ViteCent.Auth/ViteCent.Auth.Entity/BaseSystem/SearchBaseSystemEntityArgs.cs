#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseSystem;

/// <summary>
/// 搜索系统信息数据参数
/// </summary>
[Serializable]
public class SearchBaseSystemEntityArgs : SearchArgs, IRequest<List<BaseSystemEntity>>
{
}