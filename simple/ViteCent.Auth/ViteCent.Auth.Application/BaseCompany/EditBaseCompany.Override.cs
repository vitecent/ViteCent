#region

using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseCompany;

/// <summary>
/// </summary>
public partial class EditBaseCompany
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditBaseCompanyArgs request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ParentId))
        {
            var hasParentArgs = new GetBaseCompanyEntityArgs
            {
                Id = request.ParentId,
            };

            var hasParent = await mediator.Send(hasParentArgs, cancellationToken);

            if (hasParent == null)
                return new BaseResult(500, "父级公司不存在");

            if (hasParent.Status == (int)StatusEnum.Disable)
                return new BaseResult(500, "父级公司已禁用");

            if (string.IsNullOrWhiteSpace(hasParent.Level))
                request.Level = hasParent.Id;
            else
                request.Level = $"{hasParent.Level},{hasParent.Id}";
        }

        var hasArgs = new HasBaseCompanyEntityArgs
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}