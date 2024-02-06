//-----------------------------------------------------------------------
// <copyright file="IJokeRepository.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Joke Interface
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Repositories;

/// <summary>
/// Joke Interface
/// </summary>
public interface IJokeRepository
{
    void SetupLogger(ILogger logger);

    /// <summary>
    /// Find All Records
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Records</returns>
    IQueryable<JokeBasicPlus> ListAll(string requestingUserName = "ANON");

    /// <summary>
    /// Get a random joke
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Record</returns>
    JokeBasicPlus GetRandomJoke(string requestingUserName = "ANON");

    /// <summary>
    /// Get a random joke
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Record</returns>
    string GetRandomJokeText(string requestingUserName = "ANON");

    /// <summary>
    /// Get a random joke
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Record</returns>
    JokeBasicPlus GetRandomJokeSimpleJson(string requestingUserName = "ANON");

    /// <summary>
    /// Get Joke Categories
    /// </summary>
    /// <returns>List of Category Names</returns>
    IQueryable<string> GetJokeCategories(string requestingUserName = "ANON");

    /// <summary>
    /// Find Records by Search Text and/or Category
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Records</returns>
    IQueryable<JokeBasicPlus> SearchJokes(string searchTxt, string jokeCategoryTxt, string requestingUserName = "ANON");

    ///// <summary>
    ///// Find One Specific Joke
    ///// </summary>
    ///// <param name="requestingUserName">Requesting UserName</param>
    ///// <param name="id">id</param>
    ///// <returns>Records</returns>
    //JokeBasicPlus GetOne(int id, string requestingUserName = "ANON");

    ///// <summary>
    ///// Duplicate Record Check
    ///// </summary>
    ///// <param name="keyValue">Key Value</param>
    ///// <param name="dscr">Description</param>
    ///// <param name="fieldName">Field Name</param>
    ///// <param name="errorMessage">Message</param>
    ///// <returns>Success</returns>
    //bool DupCheck(int keyValue, string dscr, ref string fieldName, ref string errorMessage);

    ///// <summary>
    ///// Add Record
    ///// </summary>
    ///// <param name="requestingUserName">Requesting UserName</param>
    ///// <param name="Joke">Object</param>
    ///// <returns>Success</returns>
    //bool Add(Joke Joke, string requestingUserName = "ANON");

    ///// <summary>
    ///// Delete Check
    ///// </summary>
    ///// <param name="requestingUserName">Requesting UserName</param>
    ///// <param name="id">Record Key</param>
    ///// <param name="errorMessage">Message</param>
    ///// <returns>Success</returns>
    //bool DeleteCheck(int id, ref string errorMessage, string requestingUserName = "ANON");

    ///// <summary>
    ///// Delete Record
    ///// </summary>
    ///// <param name="requestingUserName">Requesting UserName</param>
    ///// <param name="id">Record Key</param>
    ///// <returns>Success</returns>
    //bool Delete(int id, string requestingUserName = "ANON");

    ///// <summary>
    ///// Save Record
    ///// </summary>
    ///// <param name="requestingUserName">Requesting UserName</param>
    ///// <param name="id">Id</param>
    ///// <param name="Joke">Object</param>
    ///// <returns>Success</returns>
    //bool Save(int id, Joke joke, string requestingUserName = "ANON");

    ///// <summary>
    ///// Add Joke Rating
    ///// </summary>
    ///// <param name="requestingUserName">Requesting UserName</param>
    ///// <param name="JokeRating">Object</param>
    ///// <returns>Success</returns>
    //decimal AddRating(JokeRating jokeRating, string requestingUserName = "ANON");

    ///// <summary>
    ///// Export Data
    ///// </summary>
    ///// <returns>Success</returns>
    //bool ExportData(string fileName);

    ///// <summary>
    ///// Import Data
    ///// </summary>
    ///// <returns>Success</returns>
    //bool ImportData(string data);

    /// <summary>
    /// Disposal
    /// </summary>
    void Dispose();
}
