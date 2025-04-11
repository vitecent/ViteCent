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

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// 批量新增用户信息参数
/// </summary>
[Serializable]
public class AddBaseUserListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public List<AddBaseUserArgs> Items = [];
}