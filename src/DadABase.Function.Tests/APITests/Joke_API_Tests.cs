////-----------------------------------------------------------------------
//// <copyright file="Joke_API_Tests.cs" company="Luppes Consulting, Inc.">
//// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
//// </copyright>
//// <summary>
//// Joke API Tests
//// </summary>
////-----------------------------------------------------------------------
//namespace DadABase.Tests;

//[ExcludeFromCodeCoverage]
//public class Joke_API_Tests : BaseTest
//{
//    private readonly JokeRepositorySql repo;
//    private readonly JokeController apiController;

//    public Joke_API_Tests(ITestOutputHelper output)
//    {
//        Task.Run(() => SetupInitialize(output)).Wait();

//        var mockContext = GetMockHttpContext(testData.UserName);
//        repo = new JokeRepositorySql(appSettings, db);
//        apiController = new JokeController(appSettings, mockContext, repo);
//    }

//    [Fact]
//    public void Api_Joke_Get_List_Works()
//    {
//        // Arrange

//        // Act
//        var jokeList = apiController.List();

//        // Assert
//        Assert.True(jokeList != null, "Found no data!");
//        output.WriteLine($"Found {jokeList.Count} Jokes!");
//        foreach (var item in jokeList)
//        {
//            output.WriteLine($"Joke: {item.Category} {item.Joke}");
//        }
//        Assert.True(jokeList.Count >= 0, "Found no Jokes!");
//    }

//    [Fact]
//    public void Api_Joke_GetRandom_Works()
//    {
//        // Arrange

//        // Act
//        var joke = apiController.Get();

//        // Assert
//        Assert.True(joke != null, "Found no data!");
//        output.WriteLine($"Found Joke: {joke.Joke}");
//    }

//    [Fact]
//    public void Api_Joke_GetOne_Works()
//    {
//        // Arrange

//        // Act
//        var joke = apiController.GetOne(1);

//        // Assert
//        Assert.True(joke != null, "Found no data!");
//        output.WriteLine($"Found Joke: {joke.Joke}");
//    }

//    [Fact]
//    public void Api_Joke_Category_Works()
//    {
//        // Arrange

//        // Act
//        var jokeList = apiController.Category("Engineers");

//        // Assert
//        Assert.True(jokeList != null, "Found no data!");
//        output.WriteLine($"Found {jokeList.Count} Jokes!");
//        foreach (var item in jokeList)
//        {
//            output.WriteLine($"Joke: {item.Category} {item.Joke}");
//        }
//        Assert.True(jokeList.Count >= 0, "Found no Jokes!");
//    }

//    [Fact]
//    public void Api_Joke_Search_Works()
//    {
//        // Arrange

//        // Act
//        var jokeList = apiController.Search("it");

//        // Assert
//        Assert.True(jokeList != null, "Found no data!");
//        output.WriteLine($"Found {jokeList.Count} Jokes!");
//        foreach (var item in jokeList)
//        {
//            output.WriteLine($"Joke: {item.Category} {item.Joke}");
//        }
//        Assert.True(jokeList.Count >= 0, "Found no Jokes!");
//    }

//    //[Fact]
//    //public void Api_Joke_Put_Works()
//    //{
//    //    // Arrange
//    //    var newJoke = new Joke()
//    //    {
//    //        JokeCategoryId = 1,
//    //        JokeCategoryTxt = "Chickens",
//    //        JokeTxt = "Which day of the week do chickens hate most? Fry-day!"
//    //    };

//    //    // Act
//    //    var message = apiController.Put(newJoke);

//    //    // Assert
//    //    output.WriteLine($"API returned {message.Success} {message.Message}");
//    //    Assert.True(message.Success, "Put did not succeed!");
//    //}

//    [Fact]
//    public void Api_Joke_Initialize_Works()
//    {
//        // Arrange
//        _ = new JokeController(appSettings, GetMockHttpContext(testData.UserName), new JokeRepositorySql(appSettings, db));
//        // Act
//        // Assert
//    }
//}