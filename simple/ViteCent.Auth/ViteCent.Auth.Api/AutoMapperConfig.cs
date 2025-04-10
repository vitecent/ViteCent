/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseDictionary;
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
using ViteCent.Auth.Entity.BaseDictionary;
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
        CreateMap<DisableBaseCompanyArgs, GetBaseCompanyEntityArgs>();
        CreateMap<EnableBaseCompanyArgs, GetBaseCompanyEntityArgs>();
        CreateMap<SearchBaseCompanyArgs, SearchBaseCompanyEntityArgs>();
        CreateMap<BaseCompanyEntity, BaseCompanyResult>();
        CreateMap<DeleteBaseCompanyArgs, GetBaseCompanyEntityArgs>();
        CreateMap<BaseCompanyEntity, DeleteBaseCompanyEntity>();

        #endregion

        #region BaseDepartment

        CreateMap<AddBaseDepartmentArgs, AddBaseDepartmentEntity>();
        CreateMap<EditBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<GetBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<DisableBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<EnableBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<SearchBaseDepartmentArgs, SearchBaseDepartmentEntityArgs>();
        CreateMap<BaseDepartmentEntity, BaseDepartmentResult>();
        CreateMap<DeleteBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<BaseDepartmentEntity, DeleteBaseDepartmentEntity>();

        #endregion

        #region BaseDictionary

        CreateMap<AddBaseDictionaryArgs, AddBaseDictionaryEntity>();
        CreateMap<EditBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();
        CreateMap<GetBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();
        CreateMap<DisableBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();
        CreateMap<EnableBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();
        CreateMap<SearchBaseDictionaryArgs, SearchBaseDictionaryEntityArgs>();
        CreateMap<BaseDictionaryEntity, BaseDictionaryResult>();
        CreateMap<DeleteBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();
        CreateMap<BaseDictionaryEntity, DeleteBaseDictionaryEntity>();

        #endregion

        #region BaseOperation

        CreateMap<AddBaseOperationArgs, AddBaseOperationEntity>();
        CreateMap<EditBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<GetBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<DisableBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<EnableBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<SearchBaseOperationArgs, SearchBaseOperationEntityArgs>();
        CreateMap<BaseOperationEntity, BaseOperationResult>();
        CreateMap<DeleteBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<BaseOperationEntity, DeleteBaseOperationEntity>();

        #endregion

        #region BasePosition

        CreateMap<AddBasePositionArgs, AddBasePositionEntity>();
        CreateMap<EditBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<GetBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<DisableBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<EnableBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<SearchBasePositionArgs, SearchBasePositionEntityArgs>();
        CreateMap<BasePositionEntity, BasePositionResult>();
        CreateMap<DeleteBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<BasePositionEntity, DeleteBasePositionEntity>();

        #endregion

        #region BaseResource

        CreateMap<AddBaseResourceArgs, AddBaseResourceEntity>();
        CreateMap<EditBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<GetBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<DisableBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<EnableBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<SearchBaseResourceArgs, SearchBaseResourceEntityArgs>();
        CreateMap<BaseResourceEntity, BaseResourceResult>();
        CreateMap<DeleteBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<BaseResourceEntity, DeleteBaseResourceEntity>();

        #endregion

        #region BaseRole

        CreateMap<AddBaseRoleArgs, AddBaseRoleEntity>();
        CreateMap<EditBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<GetBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<DisableBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<EnableBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<SearchBaseRoleArgs, SearchBaseRoleEntityArgs>();
        CreateMap<BaseRoleEntity, BaseRoleResult>();
        CreateMap<DeleteBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<BaseRoleEntity, DeleteBaseRoleEntity>();

        #endregion

        #region BaseRolePermission

        CreateMap<AddBaseRolePermissionArgs, AddBaseRolePermissionEntity>();
        CreateMap<EditBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<GetBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<DisableBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<EnableBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<SearchBaseRolePermissionArgs, SearchBaseRolePermissionEntityArgs>();
        CreateMap<BaseRolePermissionEntity, BaseRolePermissionResult>();
        CreateMap<DeleteBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<BaseRolePermissionEntity, DeleteBaseRolePermissionEntity>();

        #endregion

        #region BaseSystem

        CreateMap<AddBaseSystemArgs, AddBaseSystemEntity>();
        CreateMap<EditBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<GetBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<DisableBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<EnableBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<SearchBaseSystemArgs, SearchBaseSystemEntityArgs>();
        CreateMap<BaseSystemEntity, BaseSystemResult>();
        CreateMap<DeleteBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<BaseSystemEntity, DeleteBaseSystemEntity>();

        #endregion

        #region BaseUser

        CreateMap<AddBaseUserArgs, AddBaseUserEntity>();
        CreateMap<EditBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<GetBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<DisableBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<EnableBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<SearchBaseUserArgs, SearchBaseUserEntityArgs>();
        CreateMap<BaseUserEntity, BaseUserResult>();
        CreateMap<DeleteBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<BaseUserEntity, DeleteBaseUserEntity>();

        #endregion

        #region BaseUserRole

        CreateMap<AddBaseUserRoleArgs, AddBaseUserRoleEntity>();
        CreateMap<EditBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<GetBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<DisableBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<EnableBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<SearchBaseUserRoleArgs, SearchBaseUserRoleEntityArgs>();
        CreateMap<BaseUserRoleEntity, BaseUserRoleResult>();
        CreateMap<DeleteBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<BaseUserRoleEntity, DeleteBaseUserRoleEntity>();

        #endregion

        OverrideMap();
    }
}