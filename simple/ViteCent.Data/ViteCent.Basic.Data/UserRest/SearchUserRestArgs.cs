/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// 搜索调休申请参数
/// </summary>
[Serializable]
public class SearchUserRestArgs : SearchArgs, IRequest<PageResult<UserRestResult>>
{
}