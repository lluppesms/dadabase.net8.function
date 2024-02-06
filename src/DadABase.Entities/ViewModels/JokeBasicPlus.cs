//-----------------------------------------------------------------------
// <copyright file="JokeBasicPlus.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Basic Joke ViewModel
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Data;

public class JokeBasicPlus
{
    /// <summary>
    /// Joke Category
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// Joke
    /// </summary>
    [JsonProperty("joke")]
    public string Joke { get; set; }

    /// <summary>
    /// Attribution
    /// </summary>
    [JsonProperty("attribution", NullValueHandling = NullValueHandling.Ignore)]
    public string Attribution { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public JokeBasicPlus()
    {
        Joke = string.Empty;
        Category = string.Empty;
        Attribution = string.Empty;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public JokeBasicPlus(string jokeTxt, string jokeCategoryTxt)
    {
        Joke = jokeTxt;
        Category = jokeCategoryTxt;
        Attribution = null;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public JokeBasicPlus(string jokeTxt, string jokeCategoryTxt, string attribution)
    {
        Joke = jokeTxt;
        Category = jokeCategoryTxt;
        Attribution = string.IsNullOrEmpty(attribution) ? null : attribution;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public JokeBasicPlus(Joke joke)
    {
        Joke = joke.JokeTxt;
        Category = joke.JokeCategoryTxt;
        Attribution = string.IsNullOrEmpty(joke.Attribution) ? null : joke.Attribution;
    }
}
