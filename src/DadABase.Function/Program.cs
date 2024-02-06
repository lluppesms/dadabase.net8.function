
var host = new HostBuilder()
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
