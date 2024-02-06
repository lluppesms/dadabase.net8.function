//-----------------------------------------------------------------------
// <copyright file="TestingData_Category.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Test Category Data
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.SampleData;

/// <summary>
/// Test Category Data
/// </summary>
public partial class TestingData
{
    /// <summary>
    /// Get First Category
    /// </summary>
    /// <param name="db">Database</param>
    /// <returns>Category</returns>
    public static JokeCategory GetFirstCategory(ProjectEntities db)
    {
        return db.JokeCategory.Where(c => c.JokeCategoryId == 1).FirstOrDefault();
    }

    /// <summary>
    /// Get Second Category
    /// </summary>
    /// <param name="db">Database</param>
    /// <returns>Category</returns>
    public static JokeCategory GetSecondCategory(ProjectEntities db)
    {
        return db.JokeCategory.Where(c => c.JokeCategoryId == 2).FirstOrDefault();
    }

    /// <summary>
    /// Get Third Category
    /// </summary>
    /// <param name="db">Database</param>
    /// <returns>Category</returns>
    public static JokeCategory GetThirdCategory(ProjectEntities db)
    {
        return db.JokeCategory.Where(c => c.JokeCategoryId == 3).FirstOrDefault();
    }

    /// <summary>
    /// Creates the Company Test Data
    /// </summary>
    private static void Create_Category_Data(ProjectEntities db)
    {
        if (!db.JokeCategory.Any())
        {
            db.JokeCategory.AddRange(
                new List<JokeCategory>() {
                    new JokeCategory { JokeCategoryId = 1, JokeCategoryTxt = "Chickens" },
                    new JokeCategory { JokeCategoryId = 2, JokeCategoryTxt = "Engineers" },
                    new JokeCategory { JokeCategoryId = 3, JokeCategoryTxt = "Jobs" }
                }
            );
        }
    }
}
