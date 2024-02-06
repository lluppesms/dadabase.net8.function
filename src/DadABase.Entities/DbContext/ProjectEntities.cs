//-----------------------------------------------------------------------
// <copyright file="ProjectEntities.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Project Entities
// </summary>
//-----------------------------------------------------------------------
// *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
// These notes are right for .NET Framework 4.x, but not right for .NET Core...!!!
// Will have to figure out how to set the right settings in Program.cs file...
// *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
//-----------------------------------------------------------------------
// Using Managed Identity rights to connect to SQL Server
//-----------------------------------------------------------------------
// if this is a local database, skip the constructor logic,
// otherwise use the App Service Managed Identity to get a token for login
//-----------------------------------------------------------------------
// Run these scripts in the database to grant access to the App Service MI:
//    CREATE USER [<appServiceIdentityName>] FROM EXTERNAL PROVIDER
//    ALTER ROLE db_owner ADD MEMBER  [<appServiceIdentityName>]
//-----------------------------------------------------------------------
// The Connection string should be set to the following:
//    data source=tcp:<databaseServerName>.database.windows.net,1433;initial catalog=<databaseName>;
//    data source=xxxxx.database.windows.net;Database=YourBase;Authentication=Active Directory Default;Connection Timeout=10;",
//-----------------------------------------------------------------------
// For more info, see:
// https://learn.microsoft.com/en-us/azure/app-service/tutorial-connect-msi-sql-database?tabs=windowsclient%2Cef%2Cdotnet
//-----------------------------------------------------------------------

namespace DadABase.Data;

[ExcludeFromCodeCoverage]
public partial class ProjectEntities : DbContext
{
    public ProjectEntities(DbContextOptions<ProjectEntities> options) : base(options)
    {
    }

    // need to figure out something here or in Program.cs to set the right connection string and get that token...
    //public ProjectEntities(DbContextOptions<ProjectEntities> options) : base(options)
    //{
    //    DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
    //    optionsBuilder.UseSqlServer("");
    //    var conn = (System.Data.SqlClient.SqlConnection)Database.Connection;
    //    if (conn.ConnectionString.IndexOf("data source=.", StringComparison.OrdinalIgnoreCase) < 0 &&
    //        conn.ConnectionString.IndexOf("data source=(localdb)", StringComparison.OrdinalIgnoreCase) < 0)
    //    {
    //        var credential = new Azure.Identity.DefaultAzureCredential();
    //        var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));
    //        conn.AccessToken = token.Token;
    //    }
    //}
    // This code worked in the .NET Framework app, but not here...
    //public ProjectEntities()
    //{
    //    var conn = (System.Data.SqlClient.SqlConnection)Database.Connection;
    //    if (conn.ConnectionString.IndexOf("data source=.", StringComparison.OrdinalIgnoreCase) < 0 &&
    //        conn.ConnectionString.IndexOf("data source=(localdb)", StringComparison.OrdinalIgnoreCase) < 0)
    //    {
    //        var credential = new Azure.Identity.DefaultAzureCredential();
    //        var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));
    //        conn.AccessToken = token.Token;
    //    }
    //}
    //public ProjectEntities(DbConnection connection)
    //: base(connection, true)
    //{
    //}


    public virtual DbSet<Joke> Joke { get; set; }

    public virtual DbSet<JokeCategory> JokeCategory { get; set; }

    public virtual DbSet<JokeRating> JokeRating { get; set; }
}
