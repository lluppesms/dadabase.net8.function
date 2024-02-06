/*
  ----------------------------------------------------------------------------------------------------
  ===> 01/30/2024:  these lines are probably wrong and need attention...:

  // LINE 66:   .UseStartup<TestingStartup>()
  // LINE 109:  var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
  ----------------------------------------------------------------------------------------------------
*/
namespace DadABase.Tests;

[ExcludeFromCodeCoverage]
public abstract class BaseWebTesting
{
    protected TestingData testData = null;
    protected ProjectEntities db;
    protected AppSettings appSettings;
    protected HttpClient _client;

    protected async Task SetupInitialize()
    {
        testData = new TestingData();
        db = await testData.Initialize();
        appSettings = new AppSettings
        {
            UserName = testData.UserName
        };
        _client = GetClient();
    }

    protected class ConsoleWriter : StringWriter
    {
#pragma warning disable IDE0044 // Add readonly modifier
        private ITestOutputHelper output;
#pragma warning restore IDE0044 // Add readonly modifier
        public ConsoleWriter(ITestOutputHelper output)
        {
            this.output = output;
        }

        public override void WriteLine(string m)
        {
            output.WriteLine(m);
        }
    }

    protected HttpClient GetClient()
    {
        var startupAssembly = Assembly.GetExecutingAssembly();
        //  OLD: var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
        var contentRoot = GetProjectPath(string.Empty, startupAssembly);
        var builder = new WebHostBuilder()
            .UseContentRoot(contentRoot)
            .ConfigureServices(InitializeServices)
            // ====>   do I need this for authentication.... ?????       .UseStartup<TestingStartup>()
            .UseEnvironment("Testing"); // ensure ConfigureTesting is called in Startup
        var server = new TestServer(builder);
        var client = server.CreateClient();
        return client;
    }

    protected virtual void InitializeServices(IServiceCollection services)
    {
        var startupAssembly = Assembly.GetExecutingAssembly();
        //  OLD: var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
        // Inject a custom application part manager. Overrides AddMvcCore() because that uses TryAdd().
        var manager = new ApplicationPartManager();
        manager.ApplicationParts.Add(new AssemblyPart(startupAssembly));
        manager.FeatureProviders.Add(new ControllerFeatureProvider());
        manager.FeatureProviders.Add(new ViewComponentFeatureProvider());
        services.AddSingleton(manager);
    }

    /// <summary>
    /// Gets the full path to the target project path that we wish to test
    /// </summary>
    /// <param name="solutionRelativePath">
    /// The parent directory of the target project.
    /// e.g. src, samples, test, or test/Websites
    /// </param>
    /// <param name="startupAssembly">The target project's assembly.</param>
    /// <returns>The full path to the target project.</returns>
    private static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
    {
        var solutionName = "DadABase.Website";

        // Get name of the target project which we want to test
        var projectName = startupAssembly.GetName().Name;
        // Get currently executing test project path
        var applicationBasePath = "/"; //// ?????? PlatformServices.Default.Application.ApplicationBasePath;
                                       // Find the folder which contains the solution file. We then use this information to find the target
                                       // project which we want to test.
        var directoryInfo = new DirectoryInfo(applicationBasePath);
        do
        {
            var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, solutionName));
            if (solutionFileInfo.Exists)
            {
                return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
            }
            directoryInfo = directoryInfo.Parent;
        }
        while (directoryInfo.Parent != null);
        throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
    }
}
