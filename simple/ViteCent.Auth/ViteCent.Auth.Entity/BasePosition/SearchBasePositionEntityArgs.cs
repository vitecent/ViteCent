#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// 搜索职位信息数据参数
/// </summary>
[Serializable]
public class SearchBasePositionEntityArgs : SearchArgs, IRequest<List<BasePositionEntity>>
{
}