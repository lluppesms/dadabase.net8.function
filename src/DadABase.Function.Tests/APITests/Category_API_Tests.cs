////-----------------------------------------------------------------------
//// <copyright file="Category_API_Tests.cs" company="Luppes Consulting, Inc.">
//// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
//// </copyright>
//// <summary>
//// Category API Tests
//// </summary>
////-----------------------------------------------------------------------
//namespace DadABase.Tests;

//[ExcludeFromCodeCoverage]
//public class Category_API_Tests : BaseTest
//{
//    private readonly JokeCategoryRepositorySql repo;
//    private readonly CategoryController apiController;

//    public Category_API_Tests(ITestOutputHelper output)
//    {
//        Task.Run(() => SetupInitialize(output)).Wait();

//        var mockContext = GetMockHttpContext(testData.UserName);
//        repo = new JokeCategoryRepositorySql(appSettings, db);
//        apiController = new CategoryController(appSettings, mockContext, repo);
//    }

//    [Fact]
//    public void Api_Category_Get_List_Works()
//    {
//        // Arrange

//        // Act
//        var categoryList = apiController.List();

//        // Assert
//        Assert.True(categoryList != null, "Found no data!");
//        output.WriteLine($"Found {categoryList.Count} Categories!");
//        foreach (var item in categoryList)
//        {
//            output.WriteLine($"Joke: {item}");
//        }
//        Assert.True(categoryList.Count >= 0, "Found no Categories!");
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