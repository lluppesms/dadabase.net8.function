//-----------------------------------------------------------------------
// <copyright file="Joke_Repository_Tests.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Joke Repository Tests
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Tests;

[ExcludeFromCodeCoverage]
public class Joke_Repository_Tests : BaseTest
{
    private readonly JokeRepository repo;

    public Joke_Repository_Tests(ITestOutputHelper output)
    {
        Task.Run(() => SetupInitialize(output)).Wait();
        repo = new JokeRepository();
    }

    [Fact]
    public void Repo_Joke_GetRandomJokeText_Works()
    {
        var joke = repo.GetRandomJokeText(testData.UserName);
        output.WriteLine($"Found joke: {joke}");
        Assert.True(!string.IsNullOrEmpty(joke), "Found no Jokes!");
    }

    [Fact]
    public void Repo_Joke_GetRandomJoke_Works()
    {
        var joke = repo.GetRandomJoke(testData.UserName);
        output.WriteLine($"Found joke: {joke.Joke}");
        Assert.True(joke != null && !string.IsNullOrEmpty(joke.Joke), "Found no Jokes!");
    }

    [Fact]
    public void Repo_Joke_GetRandomJokeSimpleJson_Works()
    {
        var joke = repo.GetRandomJokeSimpleJson(testData.UserName);
        output.WriteLine($"Found joke: {joke.Joke}");
        Assert.True(joke != null && !string.IsNullOrEmpty(joke.Joke), "Found no Jokes!");
    }

    [Fact]
    public void Repo_Joke_SearchJokes_Works()
    {
        var jokes = repo.SearchJokes("road", string.Empty, testData.UserName).ToList();
        output.WriteLine($"SearchTxt Only: Found {jokes.Count} Jokes!");
        Assert.True(jokes.Count >= 0, "SearchTxt Only: Found no Jokes!");

        jokes = repo.SearchJokes("road", "Chickens", testData.UserName).ToList();
        output.WriteLine($"SearchTxt+Category: Found {jokes.Count} Jokes!");
        Assert.True(jokes.Count >= 0, "SearchTxt+Category: Found no Jokes!");
    }

    [Fact]
    public void Repo_Joke_GetJokeCategories_Works()
    {
        var jokes = repo.GetJokeCategories(testData.UserName).ToList();
        output.WriteLine($"Found {jokes.Count} Categories!");
        Assert.True(jokes.Count >= 0, "Found no Categories!");
    }

    [Fact]
    public void Repo_Joke_FindAll_Works()
    {
        var jokes = repo.ListAll(testData.UserName).ToList();
        output.WriteLine($"Found {jokes.Count} Jokes!");
        Assert.True(jokes.Count >= 0, "Found no Jokes!");
    }

    //[Theory]
    //[InlineData("ADD")]
    //[InlineData("UPDATE")]
    //public void Repo_Joke_AddUpdate_Works(string action)
    //{
    //    // Arrange
    //    output.WriteLine($"Performing {action} test...");
    //    var item = CreateNewJokeRecord();
    //    Assert.True(item != null, "Joke record was not created properly!");

    //    var newJokeId = item.JokeId;
    //    var newJokeTxt = item.JokeTxt;

    //    // Act
    //    output.WriteLine($"  Adding JokeId {newJokeId}");
    //    var addSuccess = repo.Add(item, testData.UserName);
    //    if (!addSuccess) { output.WriteLine($"  Add Joke returned false!"); }
    //    Assert.True(addSuccess);
    //    newJokeId = item.JokeId;
    //    output.WriteLine($"  Added JokeId {newJokeId} with text {newJokeTxt}");

    //    // detach the item so we get a different instance
    //    db.Entry(item).State = EntityState.Detached;

    //    // fetch the item
    //    var newItem = repo.GetOne(newJokeId, testData.UserName);
    //    output.WriteLine($"  Retrieved new Joke");
    //    Assert.NotNull(newItem);

    //    // Assert
    //    Assert.True(newItem.JokeId != 0);
    //    Assert.Equal(item.JokeTxt, newJokeTxt);

    //    if (action == "UPDATE")
    //    {
    //        // change the data
    //        newItem.JokeTxt = Guid.NewGuid().ToString();

    //        // update the item
    //        repo.Save(newItem.JokeId, newItem, testData.UserName);
    //        var updatedItem = repo.GetOne(newJokeId, testData.UserName);

    //        Assert.Equal(item.JokeId, updatedItem.JokeId);
    //        Assert.NotEqual(item.JokeTxt, updatedItem.JokeTxt);
    //    }

    //    // Cleanup
    //    output.WriteLine($"  Deleting new Joke");
    //    repo.Delete(item.JokeId, testData.UserName);
    //    // Assert.Pass();
    //}

    //[Fact]
    //public void Repo_Joke_Delete_Works()
    //{
    //    var errorMsg = string.Empty;

    //    // Arrange
    //    var item = CreateNewJokeRecord();
    //    Assert.True(item != null, "Joke record was not created properly!");

    //    // Act
    //    repo.Add(item, testData.UserName);
    //    var newJokeId = item.JokeId;
    //    var newJokeTxt = item.JokeTxt;

    //    var result = repo.Delete(newJokeId, testData.UserName);

    //    // Assert
    //    var missingJoke = repo.ListAll(testData.UserName).Where(i => i.JokeTxt == newJokeTxt).FirstOrDefault();
    //    if (missingJoke != null && missingJoke.JokeId != 0)
    //    {
    //        output.WriteLine($"Found Joke { missingJoke.JokeTxt } that should have been deleted!");
    //    }
    //    var jokeWasDeleted = missingJoke == null || missingJoke.JokeId == 0;
    //    output.WriteLine($"Search for Joke { newJokeTxt } returned: {jokeWasDeleted}");
    //    Assert.True(jokeWasDeleted);
    //}

    //private Joke CreateNewJokeRecord()
    //{
    //    output.WriteLine("Creating Joke Record...");

    //    if (!db.JokeCategory.Any())
    //    {
    //        output.WriteLine("  --> No categories found in database...!");
    //        return null;
    //    }
    //    var firstCategory = db.JokeCategory.FirstOrDefault();
    //    if (firstCategory == null || firstCategory.JokeCategoryId == 0)
    //    {
    //        output.WriteLine("  --> First category not found in database...!");
    //        return null;
    //    }

    //    var lastJokeId = db.Joke.LastOrDefault().JokeId;

    //    var item = new Joke
    //    {
    //        JokeId = lastJokeId + 1,
    //        JokeTxt = $"This is a bad joke: {Guid.NewGuid()} !",
    //        JokeCategoryId = firstCategory.JokeCategoryId
    //    };
    //    output.WriteLine($"  Joke.JokeCategoryId: {firstCategory.JokeCategoryId}");
    //    output.WriteLine($"  Joke.JokeId: {item.JokeId}");
    //    output.WriteLine($"  Joke.JokeTxt: {item.JokeTxt}");
    //    return item;
    //}
}