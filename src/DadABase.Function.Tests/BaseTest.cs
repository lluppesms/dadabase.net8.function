namespace DadABase.Tests;

[ExcludeFromCodeCoverage]
public abstract class BaseTest
{
    protected TestingData testData = null;
    protected ProjectEntities db;
    protected AppSettings appSettings;
    protected ITestOutputHelper output;

    protected async Task SetupInitialize(ITestOutputHelper testOutput)
    {
        this.output = testOutput;
        Console.SetOut(new ConsoleWriter(output));
        testData = new TestingData();
        db = await testData.Initialize();
        appSettings = new AppSettings
        {
            UserName = testData.UserName
        };
    }

    protected class ConsoleWriter : StringWriter
    {
        private readonly ITestOutputHelper output;
        public ConsoleWriter(ITestOutputHelper output)
        {
            this.output = output;
        }

        public override void WriteLine(string m)
        {
            output.WriteLine(m);
        }
    }

    /// <summary>
    /// Gets Mock Http Context for testing
    /// </summary>
    /// <param name="userName">User to simulate</param>
    /// <returns>Context</returns>
    protected static IHttpContextAccessor GetMockHttpContext(string userName)
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(req => req.HttpContext.User.Identity.Name).Returns(userName);
        mockHttpContextAccessor.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(i => i == "Admin"))).Returns(true);
        // Should probably simulate this also...
        //     var isAdmin = currentUser.HasClaim("groups", AppSettingsValues.AdminGroupId);
        //     mockHttpContextAccessor.Setup(x => x.HttpContext.User.Claims(???...??? == groupId)).Returns(true);
        return mockHttpContextAccessor.Object;
    }
}
