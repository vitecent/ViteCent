#region

using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Data.BaseSystem;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Auth.Api;

/// <summary>
/// 用户角色映射
/// </summary>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 映射
    /// </summary>
    public override void Map()
    {
        #region BaseCompany

        CreateMap<AddBaseCompanyArgs, AddBaseCompanyEntity>();
        CreateMap<EditBaseCompanyArgs, GetBaseCompanyEntityArgs>();
        CreateMap<GetBaseCompanyArgs, GetBaseCompanyEntityArgs>();
        CreateMap<SearchBaseCompanyArgs, SearchBaseCompanyEntityArgs>();
        CreateMap<BaseCompanyEntity, BaseCompanyResult>();
        CreateMap<DeleteBaseCompanyArgs, DeleteBaseCompanyEntityArgs>();

        #endregion

        #region BaseDepartment

        CreateMap<AddBaseDepartmentArgs, AddBaseDepartmentEntity>();
        CreateMap<EditBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<GetBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<SearchBaseDepartmentArgs, SearchBaseDepartmentEntityArgs>();
        CreateMap<BaseDepartmentEntity, BaseDepartmentResult>();
        CreateMap<DeleteBaseDepartmentArgs, DeleteBaseDepartmentEntityArgs>();

        #endregion

        #region BaseOperation

        CreateMap<AddBaseOperationArgs, AddBaseOperationEntity>();
        CreateMap<EditBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<GetBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<SearchBaseOperationArgs, SearchBaseOperationEntityArgs>();
        CreateMap<BaseOperationEntity, BaseOperationResult>();
        CreateMap<DeleteBaseOperationArgs, DeleteBaseOperationEntityArgs>();

        #endregion

        #region BasePosition

        CreateMap<AddBasePositionArgs, AddBasePositionEntity>();
        CreateMap<EditBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<GetBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<SearchBasePositionArgs, SearchBasePositionEntityArgs>();
        CreateMap<BasePositionEntity, BasePositionResult>();
        CreateMap<DeleteBasePositionArgs, DeleteBasePositionEntityArgs>();

        #endregion

        #region BaseResource

        CreateMap<AddBaseResourceArgs, AddBaseResourceEntity>();
        CreateMap<EditBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<GetBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<SearchBaseResourceArgs, SearchBaseResourceEntityArgs>();
        CreateMap<BaseResourceEntity, BaseResourceResult>();
        CreateMap<DeleteBaseResourceArgs, DeleteBaseResourceEntityArgs>();

        #endregion

        #region BaseRole

        CreateMap<AddBaseRoleArgs, AddBaseRoleEntity>();
        CreateMap<EditBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<GetBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<SearchBaseRoleArgs, SearchBaseRoleEntityArgs>();
        CreateMap<BaseRoleEntity, BaseRoleResult>();
        CreateMap<DeleteBaseRoleArgs, DeleteBaseRoleEntityArgs>();

        #endregion

        #region BaseRolePermission

        CreateMap<AddBaseRolePermissionArgs, AddBaseRolePermissionEntity>();
        CreateMap<EditBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<GetBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<SearchBaseRolePermissionArgs, SearchBaseRolePermissionEntityArgs>();
        CreateMap<BaseRolePermissionEntity, BaseRolePermissionResult>();
        CreateMap<DeleteBaseRolePermissionArgs, DeleteBaseRolePermissionEntityArgs>();

        #endregion

        #region BaseSystem

        CreateMap<AddBaseSystemArgs, AddBaseSystemEntity>();
        CreateMap<EditBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<GetBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<SearchBaseSystemArgs, SearchBaseSystemEntityArgs>();
        CreateMap<BaseSystemEntity, BaseSystemResult>();
        CreateMap<DeleteBaseSystemArgs, DeleteBaseSystemEntityArgs>();

        #endregion

        #region BaseUser

        CreateMap<AddBaseUserArgs, AddBaseUserEntity>();
        CreateMap<EditBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<GetBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<SearchBaseUserArgs, SearchBaseUserEntityArgs>();
        CreateMap<BaseUserEntity, BaseUserResult>();
        CreateMap<DeleteBaseUserArgs, DeleteBaseUserEntityArgs>();

        #endregion

        #region BaseUserRole

        CreateMap<AddBaseUserRoleArgs, AddBaseUserRoleEntity>();
        CreateMap<EditBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<GetBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<SearchBaseUserRoleArgs, SearchBaseUserRoleEntityArgs>();
        CreateMap<BaseUserRoleEntity, BaseUserRoleResult>();
        CreateMap<DeleteBaseUserRoleArgs, DeleteBaseUserRoleEntityArgs>();

        #endregion

        OverrideMap();
    }
}