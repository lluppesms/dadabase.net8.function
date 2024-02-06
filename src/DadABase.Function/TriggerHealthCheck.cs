namespace Dadabase.Function;

public class TriggerHealthCheck(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TriggerHttp>();

    [OpenApiOperation(operationId: "Hello", tags: new[] { "name" }, Summary = "Hello", Description = "Returns a greeting", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "Greeting", Description = "This returns a greeting")]
    [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "Name")]
    [Function("Hello")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "hello/{name}")] HttpRequestData req, string name)
    {
        _logger.LogInformation("C# HTTP trigger function processed a Hello request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        var dotNetVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
        response.WriteString($"{DateTime.Now:g} Hello {name} and welcome to {dotNetVersion}!");

        return response;
    }

    [OpenApiOperation(operationId: "HealthCheck", tags: new[] { "name" }, Summary = "Health Check", Description = "Returns OK if things are working", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "Health Check", Description = "This returns OK if things are working")]
    [Function("HealthCheck")]
    public HttpResponseData RunHealthCheck([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a Health Check request.");
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("OK");
        return response;
    }
}
