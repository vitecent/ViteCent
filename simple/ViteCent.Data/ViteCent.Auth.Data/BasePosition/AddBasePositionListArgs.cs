/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// 批量新增职位信息参数
/// </summary>
[Serializable]
public class AddBasePositionListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 职位信息
    /// </summary>
    public List<AddBasePositionArgs> Items = [];
}