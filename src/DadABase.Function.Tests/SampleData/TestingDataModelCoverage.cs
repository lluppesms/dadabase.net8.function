//-----------------------------------------------------------------------
// <copyright file="TestingDataModelCoverage.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Testing Data Data Modal Coverage
// </summary>
//-----------------------------------------------------------------------
using DadABase.Data;

namespace DadABase.SampleData;

/// <summary>
/// Testing Data Manager
/// </summary>
public partial class TestingData
{
    /// <summary>
    /// Add Model Test Coverage
    /// </summary>
    private static void AddDataModelCodeCoverage()
    {
        _ = new JokeCategory() { JokeCategoryId = 1, JokeCategoryTxt = "Test" };
        _ = new Joke() { JokeId = 1, JokeCategoryId = 1, JokeTxt = "Test", ImageTxt = "Picture" };

        _ = new CategoryBasic();
        _ = new CategoryBasic().Category = "newCategory";
        _ = new CategoryBasic().Category;
        _ = new JokeBasic();
        _ = new JokeBasicList();
        _ = new JokeBasicList().Jokes;
        _ = new JokeList();
        _ = new JokeList().Jokes;
        _ = new JokeList().Jokes = new List<Joke>();
        _ = new ValueMessage().Value ;
        _ = new ValueMessage().Message;
        _ = new ValueMessage("test");
        _ = new ValueMessage("Error").Value;
        _ = new ValueMessage("TimeOut").Value;
        _ = new ValueMessage("test", 1);
    }
}
