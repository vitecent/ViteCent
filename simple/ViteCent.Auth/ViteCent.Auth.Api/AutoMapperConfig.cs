/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入公司信息相关的数据结构
using ViteCent.Auth.Data.BaseCompany;

// 引入部门信息相关的数据结构
using ViteCent.Auth.Data.BaseDepartment;

// 引入字典信息相关的数据结构
using ViteCent.Auth.Data.BaseDictionary;

// 引入日志信息相关的数据结构
using ViteCent.Auth.Data.BaseLogs;

// 引入操作信息相关的数据结构
using ViteCent.Auth.Data.BaseOperation;

// 引入职位信息相关的数据结构
using ViteCent.Auth.Data.BasePosition;

// 引入资源信息相关的数据结构
using ViteCent.Auth.Data.BaseResource;

// 引入角色信息相关的数据结构
using ViteCent.Auth.Data.BaseRole;

// 引入角色权限相关的数据结构
using ViteCent.Auth.Data.BaseRolePermission;

// 引入系统信息相关的数据结构
using ViteCent.Auth.Data.BaseSystem;

// 引入用户信息相关的数据结构
using ViteCent.Auth.Data.BaseUser;

// 引入用户角色相关的数据结构
using ViteCent.Auth.Data.BaseUserRole;

// 引入公司信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseCompany;

// 引入部门信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseDepartment;

// 引入字典信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseDictionary;

// 引入日志信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseLogs;

// 引入操作信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseOperation;

// 引入职位信息相关的数据模型对象
using ViteCent.Auth.Entity.BasePosition;

// 引入资源信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseResource;

// 引入角色信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseRole;

// 引入角色权限相关的数据模型对象
using ViteCent.Auth.Entity.BaseRolePermission;

// 引入系统信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseSystem;

// 引入用户信息相关的数据模型对象
using ViteCent.Auth.Entity.BaseUser;

// 引入用户角色相关的数据模型对象
using ViteCent.Auth.Entity.BaseUserRole;

// 引入 Web 核心
using ViteCent.Core.Web;

#endregion 引入命名空间

namespace ViteCent.Auth.Api;

/// <summary>
/// AutoMapper对象映射配置类
/// </summary>
/// <remarks>
/// 该类负责配置ViteCent.Auth模块中所有需要的对象映射关系，主要功能包括：
/// 1. ViteCent.Auth请求参数与模型参数之间的映射
/// 2. 继承自BaseMapperConfig基类，实现自动化的对象映射配置
/// </remarks>
public partial class AutoMapperConfig : BaseMapperConfig
{
    /// <summary>
    /// 配置对象映射关系
    /// </summary>
    /// <remarks>在此方法中配置所有需要的对象映射规则</remarks>
    public override void Map()
    {
        #region 公司信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseCompanyArgs, AddBaseCompanyEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseCompanyArgs, GetBaseCompanyEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseCompanyArgs, GetBaseCompanyEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseCompanyArgs, GetBaseCompanyEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseCompanyArgs, GetBaseCompanyEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseCompanyArgs, SearchBaseCompanyEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseCompanyEntity, BaseCompanyResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseCompanyArgs, GetBaseCompanyEntityArgs>();
        CreateMap<BaseCompanyEntity, DeleteBaseCompanyEntity>();

        #endregion 公司信息对象映射配置

        #region 部门信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseDepartmentArgs, AddBaseDepartmentEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseDepartmentArgs, SearchBaseDepartmentEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseDepartmentEntity, BaseDepartmentResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseDepartmentArgs, GetBaseDepartmentEntityArgs>();
        CreateMap<BaseDepartmentEntity, DeleteBaseDepartmentEntity>();

        #endregion 部门信息对象映射配置

        #region 字典信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseDictionaryArgs, AddBaseDictionaryEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseDictionaryArgs, SearchBaseDictionaryEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseDictionaryEntity, BaseDictionaryResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseDictionaryArgs, GetBaseDictionaryEntityArgs>();
        CreateMap<BaseDictionaryEntity, DeleteBaseDictionaryEntity>();

        #endregion 字典信息对象映射配置

        #region 日志信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseLogsArgs, AddBaseLogsEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseLogsArgs, GetBaseLogsEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseLogsArgs, SearchBaseLogsEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseLogsEntity, BaseLogsResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseLogsArgs, GetBaseLogsEntityArgs>();
        CreateMap<BaseLogsEntity, DeleteBaseLogsEntity>();

        #endregion 日志信息对象映射配置

        #region 操作信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseOperationArgs, AddBaseOperationEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseOperationArgs, GetBaseOperationEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseOperationArgs, GetBaseOperationEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseOperationArgs, GetBaseOperationEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseOperationArgs, GetBaseOperationEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseOperationArgs, SearchBaseOperationEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseOperationEntity, BaseOperationResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseOperationArgs, GetBaseOperationEntityArgs>();
        CreateMap<BaseOperationEntity, DeleteBaseOperationEntity>();

        #endregion 操作信息对象映射配置

        #region 职位信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBasePositionArgs, AddBasePositionEntity>();

        // 编辑对象映射配置
        CreateMap<EditBasePositionArgs, GetBasePositionEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBasePositionArgs, GetBasePositionEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBasePositionArgs, GetBasePositionEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBasePositionArgs, GetBasePositionEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBasePositionArgs, SearchBasePositionEntityArgs>();

        // 获取对象映射配置
        CreateMap<BasePositionEntity, BasePositionResult>();

        // 删除对象映射配置
        CreateMap<DeleteBasePositionArgs, GetBasePositionEntityArgs>();
        CreateMap<BasePositionEntity, DeleteBasePositionEntity>();

        #endregion 职位信息对象映射配置

        #region 资源信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseResourceArgs, AddBaseResourceEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseResourceArgs, GetBaseResourceEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseResourceArgs, GetBaseResourceEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseResourceArgs, GetBaseResourceEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseResourceArgs, GetBaseResourceEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseResourceArgs, SearchBaseResourceEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseResourceEntity, BaseResourceResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseResourceArgs, GetBaseResourceEntityArgs>();
        CreateMap<BaseResourceEntity, DeleteBaseResourceEntity>();

        #endregion 资源信息对象映射配置

        #region 角色信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseRoleArgs, AddBaseRoleEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseRoleArgs, GetBaseRoleEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseRoleArgs, GetBaseRoleEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseRoleArgs, GetBaseRoleEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseRoleArgs, GetBaseRoleEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseRoleArgs, SearchBaseRoleEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseRoleEntity, BaseRoleResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseRoleArgs, GetBaseRoleEntityArgs>();
        CreateMap<BaseRoleEntity, DeleteBaseRoleEntity>();

        #endregion 角色信息对象映射配置

        #region 角色权限对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseRolePermissionArgs, AddBaseRolePermissionEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseRolePermissionArgs, SearchBaseRolePermissionEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseRolePermissionEntity, BaseRolePermissionResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseRolePermissionArgs, GetBaseRolePermissionEntityArgs>();
        CreateMap<BaseRolePermissionEntity, DeleteBaseRolePermissionEntity>();

        #endregion 角色权限对象映射配置

        #region 系统信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseSystemArgs, AddBaseSystemEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseSystemArgs, GetBaseSystemEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseSystemArgs, GetBaseSystemEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseSystemArgs, GetBaseSystemEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseSystemArgs, GetBaseSystemEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseSystemArgs, SearchBaseSystemEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseSystemEntity, BaseSystemResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseSystemArgs, GetBaseSystemEntityArgs>();
        CreateMap<BaseSystemEntity, DeleteBaseSystemEntity>();

        #endregion 系统信息对象映射配置

        #region 用户信息对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseUserArgs, AddBaseUserEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseUserArgs, GetBaseUserEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseUserArgs, GetBaseUserEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseUserArgs, GetBaseUserEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseUserArgs, GetBaseUserEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseUserArgs, SearchBaseUserEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseUserEntity, BaseUserResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseUserArgs, GetBaseUserEntityArgs>();
        CreateMap<BaseUserEntity, DeleteBaseUserEntity>();

        #endregion 用户信息对象映射配置

        #region 用户角色对象映射配置

        // 新增对象映射配置
        CreateMap<AddBaseUserRoleArgs, AddBaseUserRoleEntity>();

        // 编辑对象映射配置
        CreateMap<EditBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();

        // 获取对象映射配置
        CreateMap<GetBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();

        // 禁用对象映射配置
        CreateMap<DisableBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();

        // 启用对象映射配置
        CreateMap<EnableBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();

        // 分页对象映射配置
        CreateMap<SearchBaseUserRoleArgs, SearchBaseUserRoleEntityArgs>();

        // 获取对象映射配置
        CreateMap<BaseUserRoleEntity, BaseUserRoleResult>();

        // 删除对象映射配置
        CreateMap<DeleteBaseUserRoleArgs, GetBaseUserRoleEntityArgs>();
        CreateMap<BaseUserRoleEntity, DeleteBaseUserRoleEntity>();

        #endregion 用户角色对象映射配置

        // 其他对象映射配置
        OverrideMap();
    }
}