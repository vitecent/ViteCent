#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// 搜索系统信息参数
/// </summary>
[Serializable]
public class SearchBaseSystemArgs : SearchArgs, IRequest<PageResult<BaseSystemResult>>
{
}