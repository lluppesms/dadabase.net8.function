
var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureFunctionsWorkerDefaults(
      builder =>
      {
          builder.UseMiddleware<MyExceptionHandler>();
      }
    )
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IJokeRepository, JokeRepository>();
    })
    .Build();

host.Run();
