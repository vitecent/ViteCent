$root = "E:\Server\ViteCent\src\ViteCent.Core"

$paths = @("/ViteCent.Core/", 
"/ViteCent.Core.Api/", 
"/ViteCent.Core.Api.Swagger/",
"/ViteCent.Core.Authorize/",
"/ViteCent.Core.Authorize.Jwt/",
"/ViteCent.Core.Cache/", 
"/ViteCent.Core.Cache.Redis/",
"/ViteCent.Core.Job/",
"/ViteCent.Core.Job.Quartz/",
"/ViteCent.Core.Logging/",
"/ViteCent.Core.Logging.Log4Net/",
"/ViteCent.Core.Orm/",
"/ViteCent.Core.Orm.SqlSugar/",
"/ViteCent.Core.Register/",
"/ViteCent.Core.Register.Consul/",
"/ViteCent.Core.Trace/",
"/ViteCent.Core.Trace.Zipkin/",
"/ViteCent.Core.Web/"
)
 
foreach ($element in $paths) {
    $path = Join-Path -Path $root -ChildPath $element
	cd $path
	dotnet pack
}