#region

using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseDepartment;

/// <summary>
/// </summary>
public partial class EditBaseDepartment
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseDepartmentEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseDepartmentArgs request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId))
        {
            var hasParentArgs = new GetBaseDepartmentEntityArgs
            {
                CompanyId = request.CompanyId,
                Id = request.ParentId,
            };

            var hasParent = await mediator.Send(hasParentArgs, cancellationToken);

            if (hasParent == null)
                return new BaseResult(500, "�������Ų�����");

            if (hasParent.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "���������ѽ���");

            if (string.IsNullOrWhiteSpace(hasParent.Level))
                request.Level = hasParent.Id;
            else
                request.Level = $"{hasParent.Level},{hasParent.Id}";
        }

        var hasArgs = new HasBaseDepartmentEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}