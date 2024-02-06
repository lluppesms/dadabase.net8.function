//-----------------------------------------------------------------------
// <copyright file="TestingDataManager.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Testing Data Manager
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.SampleData;

/// <summary>
/// Testing Data Manager
/// </summary>
[ExcludeFromCodeCoverage]
public partial class TestingData
{
    /// <summary>
    /// Constructor
    /// </summary>
    public TestingData()
    {
        //// Initialize();
    }

    #region Data Variables
    /// <summary>
    /// The user identifier for the admin user
    /// </summary>
    public readonly string UserId = Guid.NewGuid().ToString();

    /// <summary>
    /// The user identifier for the normal user
    /// </summary>
    public readonly string NonPrivilegedUserId = Guid.NewGuid().ToString();

    /// <summary>
    /// The user identifier for the unknown user
    /// </summary>
    public readonly string UnknownUserId = Guid.NewGuid().ToString();

    /// <summary>
    /// The admin user name
    /// </summary>
    public readonly string UserName = "unittest";

    /// <summary>
    /// The normal user name
    /// </summary>
    public readonly string NonPrivilegedUserName = "unitnormal";

    /// <summary>
    /// The database builder
    /// </summary>
    private DbContextOptions<ProjectEntities> DatabaseOptions = null;
    #endregion

    /// <summary>
    /// Initializes the testing data module, including creating the sample data
    /// </summary>
    public async Task<ProjectEntities> Initialize()
    {
        //Settings = new AppSettings
        //{
        //    UserName = UserName
        //};
        DatabaseOptions = CreateDatabaseContextOptions();
        var db = new ProjectEntities(DatabaseOptions);
        SeedData(db);
        await Task.FromResult(true);
        return db;
    }

    /// <summary>
    /// Creates the actual test data.
    /// </summary>
    public static void SeedData(ProjectEntities db)
    {
        if (!db.Joke.Any())
        {
            Create_Category_Data(db);
            Create_Joke_Data(db);
            db.SaveChanges();
        }

        AddDataModelCodeCoverage();
    }

    /// <summary>
    /// Creates the new database context options.
    /// </summary>
    /// <returns></returns>
    public static DbContextOptions<ProjectEntities> CreateDatabaseContextOptions()
    {
        // Name in-memory database by GUID to ensure every test run has a new database not affected by others
        var options = new DbContextOptionsBuilder<ProjectEntities>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return options;
    }
}
