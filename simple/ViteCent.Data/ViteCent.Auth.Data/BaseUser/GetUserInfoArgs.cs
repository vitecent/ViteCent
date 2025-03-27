#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class GetUserInfoArgs : BaseArgs, IRequest<DataResult<BaseUserInfo>>
{
}