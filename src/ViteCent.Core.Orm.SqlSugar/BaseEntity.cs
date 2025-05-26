namespace ViteCent.Core.Orm.SqlSugar;

/// <summary>
/// 实体基类，提供基础的实体功能
/// </summary>
/// <remarks>该类作为所有实体类的基类，实现了IBaseEntity接口，为实体提供基础的功能支持。 所有业务实体类都应继承此类以获得基础的实体功能。</remarks>
[Serializable]
public class BaseEntity : IBaseEntity
{
}