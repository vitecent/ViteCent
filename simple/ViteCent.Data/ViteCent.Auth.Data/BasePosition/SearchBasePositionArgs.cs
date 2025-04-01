#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// 搜索职位信息参数
/// </summary>
[Serializable]
public class SearchBasePositionArgs : SearchArgs, IRequest<PageResult<BasePositionResult>>
{
}