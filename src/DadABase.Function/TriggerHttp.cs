namespace Dadabase.Function;

public class TriggerHttp(ILoggerFactory loggerFactory, IJokeRepository jokeRepo)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TriggerHttp>();

    [OpenApiOperation(operationId: "RandomJokeJson", tags: new[] { "name" }, Summary = "Get Random Joke Object", Description = "Gets a Random Joke and returns a JSON object", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "A Joke Object", Description = "This returns a joke JSON object")]
    [Function("RandomJokeJson")]
    public HttpResponseData GetRandomJokeJson([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Get Random Joke (JSON) requested via HTTP...");
        jokeRepo.SetupLogger(_logger);
        var joke = jokeRepo.GetRandomJokeSimpleJson();
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(joke);
        return response;
    }

    [OpenApiOperation(operationId: "RandomJoke", tags: new[] { "name" }, Summary = "Get Random Joke", Description = "Gets a Random Joke and returns plain text", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "A Joke", Description = "This returns a joke in plain text")]
    [Function("RandomJoke")]
    public HttpResponseData GetRandomJokeText([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Get Random Joke (Text) requested via HTTP...");
        jokeRepo.SetupLogger(_logger);
        var jokeText = jokeRepo.GetRandomJokeText();
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(jokeText);
        return response;
    }

    [OpenApiOperation(operationId: "Search", tags: new[] { "name" }, Summary = "Search Jokes", Description = "Searches for any jokes that match the text provided by the user and returns a list of joke objects", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "A List of Joke Objects", Description = "This returns a list of joke objects")]
    [OpenApiParameter(name: "searchTxt", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Search text")]
    [Function("Search")]
    public HttpResponseData SearchJokes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "search/{searchTxt}")] HttpRequestData req, string searchTxt)
    {
        _logger.LogInformation($"Joke Search for {searchTxt} requested via HTTP...");
        jokeRepo.SetupLogger(_logger);
        var jokeList = jokeRepo.SearchJokes(searchTxt, string.Empty);
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(jokeList);
        return response;
    }

    [OpenApiOperation(operationId: "SearchByCategory", tags: new[] { "name" }, Summary = "Search Jokes Within A Category", Description = "Searches for any jokes in one category that match the text provided by the user and returns a list of joke objects", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "A List of Joke Objects", Description = "This returns a list of joke objects")]
    [OpenApiParameter(name: "searchTxt", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Search text")]
    [OpenApiParameter(name: "categoryTxt", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Joke Category")]
    [Function("SearchByCategory")]
    public HttpResponseData SearchByCategory([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "search/{searchTxt}/{categoryTxt}")] HttpRequestData req, string searchTxt, string categoryTxt)
    {
        _logger.LogInformation($"Joke Search for {searchTxt} with Category {categoryTxt} requested via HTTP...");
        jokeRepo.SetupLogger(_logger);
        var jokeList = jokeRepo.SearchJokes(searchTxt, categoryTxt);
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(jokeList);
        return response;
    }

    [OpenApiOperation(operationId: "ListCategory", tags: new[] { "name" }, Summary = "List all Jokes In A Category", Description = "Searches for any jokes in one category and returns a list of joke objects", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "A List of Joke Objects", Description = "This returns a list of joke objects")]
    [OpenApiParameter(name: "categoryTxt", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Joke Category")]
    [Function("ListCategory")]
    public HttpResponseData ListCategory([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "category/{categoryTxt}")] HttpRequestData req, string categoryTxt)
    {
        _logger.LogInformation($"Joke Category List for {categoryTxt} requested via HTTP...");
        jokeRepo.SetupLogger(_logger);
        var jokeList = jokeRepo.SearchJokes(string.Empty, categoryTxt);
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(jokeList);
        return response;
    }

    [OpenApiOperation(operationId: "Categories", tags: new[] { "name" }, Summary = "Get Categories", Description = "Returns a list of the categories in the joke database", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "A List of Categories", Description = "This returns a list of categories")]
    [Function("Categories")]
    public HttpResponseData GetCategories([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categories")] HttpRequestData req)
    {
        _logger.LogInformation("Get Joke Categories requested via HTTP...");
        jokeRepo.SetupLogger(_logger);
        var categoryList = jokeRepo.GetJokeCategories();
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(categoryList);
        return response;
    }
}
