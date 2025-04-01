using log4net;
using ViteCent.Builder.Data;
using ViteCent.Core;

namespace ViteCent.Builder.Core;

/// <summary>
/// </summary>
public class GenerateExtensions
{
    /// <summary>
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// </summary>
    public GenerateExtensions()
    {
        logger = new BaseLogger(typeof(GenerateExtensions));
    }

    /// <summary>
    /// </summary>
    /// <param name="databases"></param>
    public void GenerateCode(List<DataBase> databases)
    {
        foreach (var database in databases)
        {
            var setting = new Setting
            {
                ProjrectName = database.Name,
                Guid = Guid.NewGuid().ToString().ToUpper(),
                Api = new ApiSetting() { Guid = Guid.NewGuid().ToString().ToUpper() },
                Application = new ApplicationSetting() { Guid = Guid.NewGuid().ToString().ToUpper() },
                Domain = new DomainSetting() { Guid = Guid.NewGuid().ToString().ToUpper() },
                Entity = new EntitySetting() { Guid = Guid.NewGuid().ToString().ToUpper() },
                Data = new DataSetting() { Guid = Guid.NewGuid().ToString().ToUpper() }
            };

            var root = Path.Combine(setting.Root, setting.SolutionName, setting.SrcName);

            logger.LogInformation($"Root {root}");

            var dir = Directory.GetCurrentDirectory();
            var nh = new NVelocityExpand(dir);

            nh.Put("Version", "9.0.4");
            nh.Put("DataBase", database);
            nh.Put("Setting", setting);

            GenerateData(setting, root, database, nh);

            GenerateEntity(setting, root, database, nh);

            GenerateApi(setting, root, database, nh);

            GenerateApplication(setting, root, database, nh);

            GenerateDomain(setting, root, database, nh);

            GenerateSolution(setting, root, database, nh);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="root"></param>
    /// <param name="database"></param>
    /// <param name="nh"></param>
    private void GenerateApi(Setting setting, string root, DataBase database, NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Api.ApiName)) return;

        var apiPth = Path.Combine(root, setting.ProjrectName, $"{setting.ProjrectName}.{setting.Api.ApiName}");

        foreach (var table in database.Tables)
        {
            var path = Path.Combine(apiPth, table.Name.ToCamelCase());

            logger.LogInformation($"Generate Api {table.Name.ToCamelCase()}");

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");

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

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                nh.Save(@"Template\Api\Add", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Api\AddOverride", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.EditName))
                nh.Save(@"Template\Api\Edit", Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

            nh.Save(@"Template\Api\Get", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Api\Page", Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Api\Delete",
                    Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.cs"));
        }

        nh.Put("Tables", database.Tables);

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

        nh.Save(@"Template\Api\Log4net", Path.Combine(apiPth, "log4net.config"));
        nh.Save(@"Template\Api\Dockerfile", Path.Combine(apiPth, "Dockerfile"));
        nh.Save(@"Template\Api\Program", Path.Combine(apiPth, "Program.cs"));

        var hasCsproj = File.Exists(Path.Combine(apiPth, $"{database.Name}.{setting.Api.ApiName}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Api\Csproj", Path.Combine(apiPth, $"{database.Name}.{setting.Api.ApiName}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="root"></param>
    /// <param name="database"></param>
    /// <param name="nh"></param>
    private void GenerateApplication(Setting setting, string root, DataBase database, NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Application.ApplicationName)) return;

        var applicatioPath = Path.Combine(root, setting.ProjrectName,
            $"{setting.ProjrectName}.{setting.Application.ApplicationName}");

        foreach (var table in database.Tables)
        {
            var path = Path.Combine(applicatioPath, table.Name.ToCamelCase());

            logger.LogInformation($"Generate Application {table.Name.ToCamelCase()}");

            var removeField = new List<string>
                { "Id","CompanyId","DepartmentId","DataVersion", "Creator", "CreateTime", "Updater", "UpdateTime" };

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

            nh.Put("Table", table);
            nh.Put("EditFields", editField);
            nh.Put("HasCompanyId", hasCompanyId);
            nh.Put("HasDepartmentId", hasDepartmentId);
            nh.Put("HasPositionId", hasPositionId);
            nh.Put("HasUserId", hasUserId);
            nh.Put("HasRoleId", hasRoleId);
            nh.Put("HasSystemId", hasSystemId);
            nh.Put("HasResourceId", hasResourceId);
            nh.Put("HasOperationId", hasOperationId);
            nh.Put("HasId", hasId);

            if (!string.IsNullOrWhiteSpace(setting.AddName))
            {
                if (database.Invoke)
                    nh.Save(@"Template\Application\AddInvoke",
                           Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));
                else
                    nh.Save(@"Template\Application\Add",
                   Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Application\AddOverride",
                   Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.EditName))
            {
                if (database.Invoke)
                    nh.Save(@"Template\Application\EditInvoke",
                       Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));
                else
                    nh.Save(@"Template\Application\Edit",
                      Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Application\EditOverride",
                   Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Application\Get",
                       Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Application\Page",
                       Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Application\Delete",
                       Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.cs"));
        }

        var hasCsproj = File.Exists(Path.Combine(applicatioPath, $"{database.Name}.{setting.Application.ApplicationName}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Application\Csproj",
            Path.Combine(applicatioPath, $"{database.Name}.{setting.Application.ApplicationName}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="root"></param>
    /// <param name="database"></param>
    /// <param name="nh"></param>
    private void GenerateData(Setting setting, string root, DataBase database, NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Data.DataName)) return;

        var dataPath = Path.Combine(root, setting.Data.DataProjrectName,
            $"{setting.ProjrectName}.{setting.Data.DataName}");

        foreach (var table in database.Tables)
        {
            var path = Path.Combine(dataPath, table.Name.ToCamelCase());

            logger.LogInformation($"Generate Data {table.Name.ToCamelCase()}");

            var removeField = new List<string>
                { "Id","DataVersion", "Creator", "CreateTime", "Updater", "UpdateTime" };

            var removeFieldEx = new List<string> { "CompanyId", "DepartmentId" };

            var addField = table.Fields.Where(x => !removeField.Contains(x.Name.ToCamelCase())).ToList();
            var validatorFields = table.Fields.Where(x => !x.Nullable && !removeField.Contains(x.Name.ToCamelCase()) && !removeFieldEx.Contains(x.Name.ToCamelCase())).ToList();

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");

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

            if (!string.IsNullOrWhiteSpace(setting.AddName))
                nh.Save(@"Template\Data\AddArgs",
                    Path.Combine(path,
                        $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.EditName))
                nh.Save(@"Template\Data\EditArgs",
                    Path.Combine(path,
                        $"{setting.EditName}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.AddName) | !string.IsNullOrWhiteSpace(setting.EditName))
            {
                nh.Save(@"Template\Data\Validator",
                       Path.Combine(path,
                           $"{table.Name.ToCamelCase()}{setting.Data.ValidatorSuffix}.cs"));

                var hasOverride = File.Exists(Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.ValidatorSuffix}.Override.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Data\ValidatorOverride",
                        Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.ValidatorSuffix}.Override.cs"));
            }

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Data\GetArgs",
                        Path.Combine(path,
                            $"{setting.GetName}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Data\SearchArgs",
                    Path.Combine(path,
                        $"{setting.Data.DataSearchPrefix}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.GetName) || !string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Data\Result",
                    Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Data.DataResultSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Data\DeleteArgs",
                        Path.Combine(path,
                            $"{setting.DeleteName}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.HasName))
            {
                var hasOverride = File.Exists(Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Data\HasArgs",
                        Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}{setting.Data.DataArgsSuffix}.cs"));
            }
        }

        var hasCsproj = File.Exists(Path.Combine(dataPath, $"{database.Name}.{setting.Data.DataName}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Data\Csproj", Path.Combine(dataPath, $"{database.Name}.{setting.Data.DataName}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="root"></param>
    /// <param name="database"></param>
    /// <param name="nh"></param>
    private void GenerateDomain(Setting setting, string root, DataBase database, NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Domain.DomainName)) return;

        var domainPath = Path.Combine(root, setting.ProjrectName,
            $"{setting.ProjrectName}.{setting.Domain.DomainName}");

        foreach (var table in database.Tables)
        {
            var path = Path.Combine(domainPath, table.Name.ToCamelCase());

            logger.LogInformation($"Generate Domain {table.Name.ToCamelCase()}");

            var hasCompanyId = table.Fields.Any(x => x.Name.ToCamelCase() == "CompanyId");
            var hasDepartmentId = table.Fields.Any(x => x.Name.ToCamelCase() == "DepartmentId");
            var hasPositionId = table.Fields.Any(x => x.Name.ToCamelCase() == "PositionId");
            var hasUserId = table.Fields.Any(x => x.Name.ToCamelCase() == "UserId");
            var hasRoleId = table.Fields.Any(x => x.Name.ToCamelCase() == "RoleId");
            var hasSystemId = table.Fields.Any(x => x.Name.ToCamelCase() == "SystemId");
            var hasResourceId = table.Fields.Any(x => x.Name.ToCamelCase() == "ResourceId");
            var hasOperationId = table.Fields.Any(x => x.Name.ToCamelCase() == "OperationId");
            var hasId = table.Fields.Any(x => x.Name.ToCamelCase() == "Id");

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

            if (!string.IsNullOrWhiteSpace(setting.AddName))
                nh.Save(@"Template\Domain\Add", Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.EditName))
                nh.Save(@"Template\Domain\Edit",
                    Path.Combine(path, $"{setting.EditName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Domain\Get", Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Domain\Page",
                    Path.Combine(path, $"{setting.PageName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Domain\Delete",
                    Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.HasName))
            {
                var hasOverride = File.Exists(Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}.cs"));

                if (!hasOverride)
                    nh.Save(@"Template\Domain\Has",
                    Path.Combine(path, $"{setting.HasName}{table.Name.ToCamelCase()}.cs"));
            }
        }

        var hasCsproj = File.Exists(Path.Combine(domainPath, $"{database.Name}.{setting.Domain.DomainName}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Domain\Csproj",
            Path.Combine(domainPath, $"{database.Name}.{setting.Domain.DomainName}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="root"></param>
    /// <param name="database"></param>
    /// <param name="nh"></param>
    private void GenerateEntity(Setting setting, string root, DataBase database, NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(setting.Entity.EntityName)) return;

        var entifyPath = Path.Combine(root, setting.ProjrectName, $"{setting.ProjrectName}.{setting.Entity.EntityName}");

        logger.LogInformation($"Generate Entity {entifyPath}");

        foreach (var table in database.Tables)
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

            nh.Save(@"Template\Entity\Entity",
                Path.Combine(path, $"{table.Name.ToCamelCase()}{setting.Entity.EntitySuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.AddName))
                nh.Save(@"Template\Entity\Add",
                Path.Combine(path, $"{setting.AddName}{table.Name.ToCamelCase()}{setting.Entity.EntitySuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.DeleteName))
                nh.Save(@"Template\Entity\DeleteEntityArgs",
                     Path.Combine(path, $"{setting.DeleteName}{table.Name.ToCamelCase()}{setting.Entity.EntityName}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.PageName))
                nh.Save(@"Template\Entity\SearchEntityArgs",
                      Path.Combine(path, $"{setting.Data.DataSearchPrefix}{table.Name.ToCamelCase()}{setting.Entity.EntityName}{setting.Data.DataArgsSuffix}.cs"));

            if (!string.IsNullOrWhiteSpace(setting.GetName))
                nh.Save(@"Template\Entity\GetEntityArgs",
                      Path.Combine(path, $"{setting.GetName}{table.Name.ToCamelCase()}{setting.Entity.EntityName}{setting.Data.DataArgsSuffix}.cs"));
        }

        var hasCsproj = File.Exists(Path.Combine(entifyPath, $"{database.Name}.{setting.Entity.EntityName}.csproj"));

        if (!hasCsproj)
            nh.Save(@"Template\Entity\Csproj", Path.Combine(entifyPath, $"{database.Name}.{setting.Entity.EntityName}.csproj"));
    }

    /// <summary>
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="root"></param>
    /// <param name="database"></param>
    /// <param name="nh"></param>
    private void GenerateSolution(Setting setting, string root, DataBase database, NVelocityExpand nh)
    {
        if (string.IsNullOrWhiteSpace(database.Name)) return;

        var path = Path.Combine(root, setting.ProjrectName);

        logger.LogInformation($"Generate Solution {path}");

        var hasSolution = File.Exists(Path.Combine(path, $"{setting.ProjrectName}.sln"));

        if (!hasSolution)
            nh.Save(@"Template\Sln", Path.Combine(path, $"{database.Name}.sln"));
    }
}