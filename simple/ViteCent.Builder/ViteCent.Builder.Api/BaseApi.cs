#region 引入命名空间

using AutoMapper;
using ViteCent.Builder.Data.Build;
using ViteCent.Core;
using ViteCent.Core.Orm;
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Builder.Api;

/// <summary>
/// </summary>
public static class BaseApi
{
    /// <summary>
    /// </summary>
    private static readonly BaseLogger logger;

    /// <summary>
    /// </summary>
    static BaseApi()
    {
        logger = new BaseLogger(typeof(BaseApi));
    }

    /// <summary>
    /// </summary>
    /// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
    /// <param name="setting">设置信息</param>
    /// <returns>处理结果</returns>
    public static async Task GetDatabase(this IMapper mapper,
        Setting setting)
    {
        var database = mapper.Map<BaseDatabaseInfo>(setting.Database);

        var configuration = database.GetConfiguration();

        var factoryConfig = new FactoryConfig
        {
            DbType = setting.Database.Type,
            Name = setting.Database.Name,
            Master = configuration,
            Slaves =
            [
                configuration
            ]
        };

        var client = new SqlSugarFactory(factoryConfig);

        var baseTables = await client.GetTables();

        var tables = mapper.Map<List<Table>>(baseTables);

        setting.Database.Tables = tables.OrderBy(x => x.CamelCaseName).ToList();

        foreach (var table in tables)
        {
            table.CamelCaseName = table.Name.ToCamelCase();

            if (table.Name == "base_logs")
                table.SplitType = "Year";

            var baseFields = await client.GetFields(table.Name);

            var fields = mapper.Map<List<Field>>(baseFields);

            foreach (var field in fields)
            {
                field.CamelCaseName = field.Name.ToCamelCase();
                field.DataType = field.Type.Replace("varchar", "string")
                    .Replace("nvarchar", "string")
                    .Replace("sql_variant", "string")
                    .Replace("text", "string")
                    .Replace("char", "string")
                    .Replace("ntext", "string")
                    .Replace("hierarchyid", "string")
                    .Replace("bit", "bool")
                    .Replace("datetime", "DateTime")
                    .Replace("datetime2", "DateTime")
                    .Replace("date", "DateTime")
                    .Replace("time", "DateTime")
                    .Replace("smalldatetime", "DateTime")
                    .Replace("DateTimestamp", "DateTime")
                    .Replace("tinyint", "byte")
                    .Replace("bigint", "long")
                    .Replace("longstring", "long")
                    .Replace("single", "decimal")
                    .Replace("money", "decimal")
                    .Replace("numeric", "decimal")
                    .Replace("smallmoney", "decimal")
                    .Replace("float", "decimal")
                    .Replace("real", "float")
                    .Replace("smallint", "short")
                    .Replace("uniqueidentifier", "Guid")
                    .Replace("smallmoney", "decimal");

                field.ColumnName = $"ColumnName = \"{field.Name}\"";

                if (field.Nullable)
                    field.DataType = $"{field.DataType}?";

                if (field.Length > 0)
                    field.ColumnLength = $", Length = {field.Length}";

                if (!string.IsNullOrWhiteSpace(field.Type))
                    field.ColumnType = $", ColumnDataType = \"{field.Type}\"";

                if (!string.IsNullOrWhiteSpace(field.Description))
                    field.ColumnDescription = $", ColumnDescription = \"{field.Description}\"";

                if (field.PrimaryKey)
                    field.ColumnPrimaryKey = ", IsPrimaryKey = true";

                if (field.Nullable)
                    field.ColumnNullable = ", IsNullable = true";

                if (field.Identity)
                    field.ColumnIdentity = ", IsIdentity = true";

                field.Default = field.DataType == "string" ? " = string.Empty;" : string.Empty;

                if (field.Name == "createTime" && table.SplitType != "None")
                    field.SplitField = true;

                if (field.Name == "version")
                {
					field.VersionField = true;
					field.EnableUpdateVersionValidation = ", IsEnableUpdateVersionValidation = true";
				}
				
            }

            table.Fields = [.. fields.OrderBy(x => x.CamelCaseName)];
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    public static async Task BuildCode(this Setting setting)
    {
        setting.Guid = Guid.NewGuid().ToString().ToUpper();
        setting.Data.Guid = Guid.NewGuid().ToString().ToUpper();
        setting.Entity.Guid = Guid.NewGuid().ToString().ToUpper();
        setting.Api.Guid = Guid.NewGuid().ToString().ToUpper();
        setting.Application.Guid = Guid.NewGuid().ToString().ToUpper();
        setting.Domain.Guid = Guid.NewGuid().ToString().ToUpper();

        var root = Path.Combine(setting.Root, setting.SolutionName, setting.SrcName);

        logger.LogInformation($"Root {root}");

        var dir = Directory.GetCurrentDirectory();
        var nh = new NVelocityExpand(dir);

        nh.Put("Version", "9.0.4");
        nh.Put("Setting", setting);

        setting.BuildData(root, nh);

        setting.BuildEntity(root, nh);

        setting.BuildApi(root, nh);

        setting.BuildApplication(root, nh);

        setting.BuildDomain(root, nh);

        setting.BuildSolution(root, nh);

        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    /// <param name="root">路径信息</param>
    /// <param name="nh">模板引擎</param>
    private static void BuildApi(this Setting setting,
        string root,
        NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Api.Name)) return;

        var apiPth = Path.Combine(root, setting.ProjrectName, $"{setting.Database.CamelCaseName}.{setting.Api.Name}");

        foreach (var table in setting.Database.Tables)
        {
            var path = Path.Combine(apiPth, table.Name.ToCamelCase());

            logger.LogInformation($"Build Api {table.Name.ToCamelCase()}");

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");
            var hasStatus = table.Fields.Any(x => x.Name.ToCamelCase() == "Status");
            var hasSort = table.Fields.Any(x => x.Name.ToCamelCase() == "Sort");
            var hasLog = table.CamelCaseName != "BaseLogs";

            nh.Put("Table", table);
            nh.Put("HasCompanyId", hasCompanyId);
            nh.Put("HasDepartmentId", hasDepartmentId);
            nh.Put("HasPositionId", hasPositionId);
            nh.Put("HasUserId", hasUserId);
            nh.Put("HasRoleId", hasRoleId);
            nh.Put("HasSystemId", hasSystemId);
            nh.Put("HasResourceId", hasResourceId);
            nh.Put("HasOperationId", hasOperationId);
            nh.Put("HasId", hasId);
            nh.Put("HasStatus", hasStatus);
            nh.Put("HasSort", hasSort);
            nh.Put("HasLog", hasLog);

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                nh.Save(@"Template\Api\Add", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));

                nh.Save(@"Template\Api\AddList", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\AddOverride", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.EditName))
            {
                nh.Save(@"Template\Api\Edit", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\EditOverride", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.GetName))
            {
                nh.Save(@"Template\Api\Get", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\GetOverride", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.PageName))
            {
                nh.Save(@"Template\Api\Page", Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\PageOverride", Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
            {
                nh.Save(@"Template\Api\Delete", Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\DeleteOverride", Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (setting.Application.Invoke == false && hasStatus && !string.IsNullOrWhiteSpace(setting.EnableName))
            {
                nh.Save(@"Template\Api\Enable", Path.Combine(path, $"{setting.EnableName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.EnableName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\EnableOverride", Path.Combine(path, $"{setting.EnableName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (setting.Application.Invoke == false && hasStatus && !string.IsNullOrWhiteSpace(setting.DisableName))
            {
                nh.Save(@"Template\Api\Disable", Path.Combine(path, $"{setting.DisableName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.DisableName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\DisableOverride", Path.Combine(path, $"{setting.DisableName}{table.Name.ToCamelCase()}.Override.cs"));
            }
        }

        nh.Put("Tables", setting.Database.Tables);

        nh.Save(@"Template\Api\Appsetting", Path.Combine(apiPth, "appsettings.json"));
        nh.Save(@"Template\Api\AppsettingDevelopment", Path.Combine(apiPth, "appsettings.Development.json"));
        nh.Save(@"Template\Api\Launchsetting", Path.Combine(apiPth, "Properties", "launchsettings.json"));

        if (!string.IsNullOrWhiteSpace(setting.Api.FacName))
        {
            nh.Save(@"Template\Api\AutoFacConfig", Path.Combine(apiPth, $"{setting.Api.FacName}.cs"));

            var hasOverride = File.Exists(Path.Combine(apiPth, $"{setting.Api.FacName}.Override.cs"));

            if (!hasOverride)
                nh.Save(@"Template\Api\AutoFacConfigOverride", Path.Combine(apiPth, $"{setting.Api.FacName}.Override.cs"));
        }

        if (!string.IsNullOrWhiteSpace(setting.Api.MapperName))
        {
            nh.Save(@"Template\Api\AutoMapperConfig", Path.Combine(apiPth, $"{setting.Api.MapperName}.cs"));

            var hasOverride = File.Exists(Path.Combine(apiPth, $"{setting.Api.MapperName}.Override.cs"));

            if (!hasOverride)
                nh.Save(@"Template\Api\AutoMapperConfigOverride", Path.Combine(apiPth, $"{setting.Api.MapperName}.Override.cs"));
        }

        nh.Save(@"Template\Api\Dockerfile", Path.Combine(apiPth, "Dockerfile"));
        nh.Save(@"Template\Api\Program", Path.Combine(apiPth, "Program.cs"));

        var hasCsproj = File.Exists(Path.Combine(apiPth, $"{setting.Database.CamelCaseName}.{setting.Api.Name}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Api\Csproj", Path.Combine(apiPth, $"{setting.Database.CamelCaseName}.{setting.Api.Name}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    /// <param name="root">路径信息</param>
    /// <param name="nh">模板引擎</param>
    private static void BuildApplication(this Setting
        setting,
        string root,
        NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Application.Name)) return;

        var applicatioPath = Path.Combine(root, setting.ProjrectName, $"{setting.Database.CamelCaseName}.{setting.Application.Name}");

        foreach (var table in setting.Database.Tables)
        {
            var path = Path.Combine(applicatioPath, table.Name.ToCamelCase());

            logger.LogInformation($"Build Application {table.Name.ToCamelCase()}");

            var removeField = new List<string> { "Id", "Version", "Creator", "CreateTime", "Updater", "UpdateTime" };

            var editField = table.Fields.Where(x => !removeField.Contains(x.Name.ToCamelCase())).ToList();
            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");
            var hasStatus = table.Fields.Any(x => x.Name.ToCamelCase() == "Status");

            nh.Put("Table", table);
            nh.Put("EditFields", editField);
            nh.Put("HasCompanyId", hasCompanyId);
            nh.Put("CompanyInvoke", hasCompanyId ? ", companyInvoke" : "");
            nh.Put("HasDepartmentId", hasDepartmentId);
            nh.Put("DepartmentInvoke", hasDepartmentId ? ", departmentInvoke" : "");
            nh.Put("HasPositionId", hasPositionId);
            nh.Put("PositionInvoke", hasPositionId ? ", positionInvoke" : "");
            nh.Put("HasUserId", hasUserId);
            nh.Put("UserInvoke", hasUserId ? ", userInvoke" : "");
            nh.Put("HasRoleId", hasRoleId);
            nh.Put("HasSystemId", hasSystemId);
            nh.Put("HasResourceId", hasResourceId);
            nh.Put("HasOperationId", hasOperationId);
            nh.Put("HasId", hasId);
            nh.Put("HasStatus", hasStatus);

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                if (setting.Application.Invoke)
                {
                    nh.Save(@"Template\Application\AddInvoke", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));

                    nh.Save(@"Template\Application\AddListInvoke", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix}.cs"));

                    var hasOverride = File.Exists(Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));

                    if (!hasOverride)
                        nh.Save(@"Template\Application\AddInvokeOverride", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));
                }
                else
                {
                    nh.Save(@"Template\Application\Add", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));

                    nh.Save(@"Template\Application\AddList", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix}.cs"));

                    var hasOverride = File.Exists(Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));

                    if (!hasOverride)
                        nh.Save(@"Template\Application\AddOverride", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));
                }
            }

            if (!string.IsNullOrWhiteSpace(setting.EditName))
            {
                if (setting.Application.Invoke)
                {
                    nh.Save(@"Template\Application\EditInvoke", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

                    var hasOverride = File.Exists(Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));

                    if (!hasOverride)
                        nh.Save(@"Template\Application\EditInvokeOverride", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));
                }
                else
                {
                    nh.Save(@"Template\Application\Edit", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

                    var hasOverride = File.Exists(Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));

                    if (!hasOverride)
                        nh.Save(@"Template\Application\EditOverride", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));
                }
            }

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Application\Get", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Application\Page", Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Application\Delete", Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.cs"));

            if (setting.Application.Invoke == false && hasStatus && !string.IsNullOrWhiteSpace(setting.EnableName))
                nh.Save(@"Template\Application\Enable", Path.Combine(path, $"{setting.EnableName}{table.Name.ToCamelCase()}.cs"));

            if (setting.Application.Invoke == false && hasStatus && !string.IsNullOrWhiteSpace(setting.DisableName))
                nh.Save(@"Template\Application\Disable", Path.Combine(path, $"{setting.DisableName}{table.Name.ToCamelCase()}.cs"));
        }

        var hasCsproj = File.Exists(Path.Combine(applicatioPath, $"{setting.Database.CamelCaseName}.{setting.Application.Name}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Application\Csproj",
                Path.Combine(applicatioPath, $"{setting.Database.CamelCaseName}.{setting.Application.Name}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    /// <param name="root">路径信息</param>
    /// <param name="nh">模板引擎</param>
    private static void BuildData(this Setting setting,
        string root,
        NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Data.Name)) return;

        var dataPath = Path.Combine(root, setting.Data.Projrect, $"{setting.Database.CamelCaseName}.{setting.Data.Name}");

        foreach (var table in setting.Database.Tables)
        {
            var path = Path.Combine(dataPath, table.Name.ToCamelCase());

            logger.LogInformation($"Build Data {table.Name.ToCamelCase()}");

            var removeField = new List<string> { "Id", "Version", "Creator", "CreateTime", "Updater", "UpdateTime" };

            var addField = table.Fields.Where(x => !removeField.Contains(x.Name.ToCamelCase())).ToList();
            var validatorFields = table.Fields.Where(x =>
                !x.Nullable && !removeField.Contains(x.Name.ToCamelCase())).ToList();

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");
            var hasStatus = table.Fields.Any(x => x.Name.ToCamelCase() == "Status");

            nh.Put("Table", table);
            nh.Put("AddFields", addField);
            nh.Put("ValidatorFields", validatorFields);
            nh.Put("Fields", table.Fields);
            nh.Put("HasCompanyId", hasCompanyId);
            nh.Put("HasDepartmentId", hasDepartmentId);
            nh.Put("HasPositionId", hasPositionId);
            nh.Put("HasUserId", hasUserId);
            nh.Put("HasRoleId", hasRoleId);
            nh.Put("HasSystemId", hasSystemId);
            nh.Put("HasResourceId", hasResourceId);
            nh.Put("HasOperationId", hasOperationId);
            nh.Put("HasId", hasId);
            nh.Put("HasStatus", hasStatus);

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                nh.Save(@"Template\Data\AddArgs", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

                nh.Save(@"Template\Data\AddListArgs", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix}{setting.Data.ArgsSuffix}.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.EditName))
                nh.Save(@"Template\Data\EditArgs", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.AddName) || !string.IsNullOrWhiteSpace(setting.EditName))
            {
                nh.Save(@"Template\Data\Validator", Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.ValidatorSuffix}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.ValidatorSuffix}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Data\ValidatorOverride", Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.ValidatorSuffix}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Data\GetArgs", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Data\SearchArgs", Path.Combine(path, $"{setting.Data.SearchPrefix}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.GetName) || !string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Data\Result", Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.ResultSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Data\DeleteArgs", Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

            if (setting.Application.Invoke == false && hasStatus && !string.IsNullOrWhiteSpace(setting.EnableName))
                nh.Save(@"Template\Data\EnableArgs", Path.Combine(path, $"{setting.EnableName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

            if (setting.Application.Invoke == false && hasStatus && !string.IsNullOrWhiteSpace(setting.DisableName))
                nh.Save(@"Template\Data\DisableArgs", Path.Combine(path, $"{setting.DisableName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.HasName))
            {
                var hasOverride = File.Exists(Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Data\HasArgs", Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Data.ArgsSuffix}.cs"));
            }
        }

        var hasCsproj = File.Exists(Path.Combine(dataPath, $"{setting.Database.CamelCaseName}.{setting.Data.Name}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Data\Csproj", Path.Combine(dataPath, $"{setting.Database.CamelCaseName}.{setting.Data.Name}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    /// <param name="root">路径信息</param>
    /// <param name="nh">模板引擎</param>
    private static void BuildDomain(this Setting setting,
        string root,
        NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Domain.Name)) return;

        var domainPath = Path.Combine(root, setting.ProjrectName, $"{setting.Database.CamelCaseName}.{setting.Domain.Name}");

        foreach (var table in setting.Database.Tables)
        {
            var path = Path.Combine(domainPath, table.Name.ToCamelCase());

            logger.LogInformation($"Build Domain {table.Name.ToCamelCase()}");

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");
            var hasStatus = table.Fields.Any(x => x.Name.ToCamelCase() == "Status");

            nh.Put("Table", table);
            nh.Put("HasCompanyId", hasCompanyId);
            nh.Put("HasDepartmentId", hasDepartmentId);
            nh.Put("HasPositionId", hasPositionId);
            nh.Put("HasUserId", hasUserId);
            nh.Put("HasRoleId", hasRoleId);
            nh.Put("HasSystemId", hasSystemId);
            nh.Put("HasResourceId", hasResourceId);
            nh.Put("HasOperationId", hasOperationId);
            nh.Put("HasId", hasId);
            nh.Put("HasStatus", hasStatus);

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                nh.Save(@"Template\Domain\Add", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));
                nh.Save(@"Template\Domain\AddList", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix} .cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.EditName))
                nh.Save(@"Template\Domain\Edit", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Domain\Get", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Domain\Page", Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Domain\Delete", Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.HasName))
            {
                var hasOverride = File.Exists(Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Domain\Has", Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}.cs"));

                var hasListOverride = File.Exists(Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix}.cs"));

                if (!hasListOverride)
                    nh.Save(@"Template\Domain\HasList", Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Data.ListSuffix}.cs"));
            }
        }

        var hasCsproj = File.Exists(Path.Combine(domainPath, $"{setting.Database.CamelCaseName}.{setting.Domain.Name}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Domain\Csproj", Path.Combine(domainPath, $"{setting.Database.CamelCaseName}.{setting.Domain.Name}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    /// <param name="root">路径信息</param>
    /// <param name="nh">模板引擎</param>
    private static void BuildEntity(this Setting setting,
        string root,
        NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Entity.Name)) return;

        var entifyPath = Path.Combine(root, setting.ProjrectName, $"{setting.Database.CamelCaseName}.{setting.Entity.Name}");

        logger.LogInformation($"Build Entity {entifyPath}");

        foreach (var table in setting.Database.Tables)
        {
            var path = Path.Combine(entifyPath, table.Name.ToCamelCase());

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");
            var hasStatus = table.Fields.Any(x => x.Name.ToCamelCase() == "Status");

            nh.Put("Table", table);
            nh.Put("BaseName", "BaseEntity");
            nh.Put("Fields", table.Fields);
            nh.Put("HasCompanyId", hasCompanyId);
            nh.Put("HasDepartmentId", hasDepartmentId);
            nh.Put("HasPositionId", hasPositionId);
            nh.Put("HasUserId", hasUserId);
            nh.Put("HasRoleId", hasRoleId);
            nh.Put("HasSystemId", hasSystemId);
            nh.Put("HasResourceId", hasResourceId);
            nh.Put("HasOperationId", hasOperationId);
            nh.Put("HasId", hasId);
            nh.Put("HasStatus", hasStatus);

            nh.Save(@"Template\Entity\Entity", Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Entity.Suffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                nh.Save(@"Template\Entity\AddEntity", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Entity.Suffix}.cs"));

                nh.Save(@"Template\Entity\AddEntityListArgs", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Entity.Suffix}{setting.Data.ListSuffix}{setting.Data.ArgsSuffix}.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Entity\DeleteEntity", Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}{setting.Entity.Name}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Entity\SearchEntityArgs", Path.Combine(path, $"{setting.Data.SearchPrefix}{table.Name.ToCamelCase()}{setting.Entity.Name}{setting.Data.ArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Entity\GetEntityArgs", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}{setting.Entity.Name}{setting.Data.ArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.HasName))
            {
                var hasOverride = File.Exists(Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Entity.Name}{setting.Data.ListSuffix}{setting.Data.ArgsSuffix}.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Entity\HasEntityListArgs", Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Entity.Name}{setting.Data.ListSuffix}{setting.Data.ArgsSuffix}.cs"));
            }
        }

        var hasCsproj = File.Exists(Path.Combine(entifyPath, $"{setting.Database.CamelCaseName}.{setting.Entity.Name}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Entity\Csproj", Path.Combine(entifyPath, $"{setting.Database.CamelCaseName}.{setting.Entity.Name}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting">设置信息</param>
    /// <param name="root">路径信息</param>
    /// <param name="nh">模板引擎</param>
    private static void BuildSolution(this Setting setting,
        string root,
        NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Database.CamelCaseName)) return;

        var path = Path.Combine(root, setting.ProjrectName);

        logger.LogInformation($"Build Solution {path}");

        var hasSolution = File.Exists(Path.Combine(path, $"{setting.Database.CamelCaseName}.sln"));

        if (!hasSolution)
            nh.Save(@"Template\Sln", Path.Combine(path, $"{setting.Database.CamelCaseName}.sln"));
    }
}