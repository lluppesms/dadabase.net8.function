//-----------------------------------------------------------------------
// <copyright file="TestingData_Joke.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Test Joke Data
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.SampleData;

/// <summary>
/// Test Joke Data
/// </summary>
public partial class TestingData
{
    /// <summary>
    /// Get First Joke
    /// </summary>
    /// <param name="db">Database</param>
    /// <returns>Joke</returns>
    public static Joke GetFirstJoke(ProjectEntities db)
    {
        return db.Joke.Where(c => c.JokeId == 1).FirstOrDefault();
    }

    /// <summary>
    /// Get Second Joke
    /// </summary>
    /// <param name="db">Database</param>
    /// <returns>Joke</returns>
    public static Joke GetSecondJoke(ProjectEntities db)
    {
        return db.Joke.Where(c => c.JokeId == 2).FirstOrDefault();
    }

    /// <summary>
    /// Get Third Joke
    /// </summary>
    /// <param name="db">Database</param>
    /// <returns>Joke</returns>
    public static Joke GetThirdJoke(ProjectEntities db)
    {
        return db.Joke.Where(c => c.JokeId == 3).FirstOrDefault();
    }

    /// <summary>
    /// Creates the Company Test Data
    /// </summary>
    private static void Create_Joke_Data(ProjectEntities db)
    {
        if (!db.Joke.Any())
        {
            db.Joke.AddRange(
                new List<Joke>() {
                  new Joke { JokeCategoryId = 1, JokeCategoryTxt = "Chickens",  JokeId = 1, JokeTxt = "What do you call a chicken crossing the road? Poultry in motion." },
                  new Joke { JokeCategoryId = 1, JokeCategoryTxt = "Chickens",  JokeId = 2, JokeTxt = "What do you get when a chicken lays an egg on top of a barn? An eggroll." },
                  new Joke { JokeCategoryId = 1, JokeCategoryTxt = "Engineers", JokeId = 3, JokeTxt = "Normal: if it ain't broke, don't fix it. Engineer: if it ain't broke, it doesn't have enough features yet." },
                  new Joke { JokeCategoryId = 1, JokeCategoryTxt = "Engineers", JokeId = 4, JokeTxt = "Optimist: the glass is half-full. Pessimist: the glass is half-empty. Engineer: that glass is twice as big as it needs to be." },
                  new Joke { JokeCategoryId = 1, JokeCategoryTxt = "Jobs",      JokeId = 5, JokeTxt = "I want a job cleaning mirrors. It's a job I can see myself doing." },
                  new Joke { JokeCategoryId = 1, JokeCategoryTxt = "Jobs",      JokeId = 6, JokeTxt = "I wasn't happy being a glue salesman, but I stuck with it." }
                }
            );
        }
    }
}
