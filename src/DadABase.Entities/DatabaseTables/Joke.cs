//-----------------------------------------------------------------------
// <copyright file="Joke.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Joke Table
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Data;

[ExcludeFromCodeCoverage]
/// <summary>
/// Joke Table
/// </summary>
[Table("Joke")]
public class Joke
{
    //CREATE TABLE [dbo].[Joke](
    //  [JokeId] [int] IDENTITY(1,1) NOT NULL,
    //  [JokeTxt] [nvarchar](max) NULL,
    //  [JokeCategoryId] [int] NULL,
    //  [Attribution] [nvarchar](500) NULL,
    //  [SortOrderNbr] [int] NOT NULL,
    //  [Rating] decimal (3,1)  NULL,
    //  [ActiveInd] [nchar](1) NOT NULL,
    //  [CreateDateTime] [datetime] NOT NULL,
    //  [CreateUserName] [nvarchar](255) NOT NULL,
    //  [ChangeDateTime] [datetime] NOT NULL,
    //  [ChangeUserName] [nvarchar](255) NOT NULL,

    /// <summary>
    /// Joke Id
    /// </summary>
    [Key, Column(Order = 0)]
    [Required(ErrorMessage = "JokeId is required")]
    [Display(Name = "JokeId", Description = "This is the JokeId field.", Prompt = "Enter JokeId")]
    public int JokeId { get; set; }

    /// <summary>
    /// Joke Text
    /// </summary>
    [Display(Name = "Joke Text", Description = "This is the Joke Text field.", Prompt = "Enter Joke Text")]
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "Joke Text is required")]
    public string JokeTxt { get; set; }

    /// <summary>
    /// Category
    /// </summary>
    [Display(Name = "Category", Description = "This is the Category field.", Prompt = "Enter Category")]
    public int? JokeCategoryId { get; set; }

    /// <summary>
    /// Joke Category Text
    /// </summary>
    [Display(Name = "Joke Category Text", Description = "This is the Joke Category Text field.", Prompt = "Enter Joke TCategory ext")]
    [StringLength(500)]
    public string JokeCategoryTxt { get; set; }

    /// <summary>
    /// Attribution
    /// </summary>
    [Display(Name = "Attribution", Description = "This is the Attribution field.", Prompt = "Enter Attribution")]
    [StringLength(500)]
    public string Attribution { get; set; }

    /// <summary>
    /// Image Text
    /// </summary>
    [Display(Name = "Image Text", Description = "This is the Image Text field.", Prompt = "Enter Image Text")]
    [DataType(DataType.MultilineText)]
    public string ImageTxt { get; set; }

    /// <summary>
    /// Active
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Active", Description = "This is the Active field.", Prompt = "Enter Active")]
    [StringLength(1)]
    public string ActiveInd { get; set; }

    /// <summary>
    /// Sort Order
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Sort Order", Description = "This is the Sort Order field.", Prompt = "Enter Sort Order")]
    public int SortOrderNbr { get; set; }

    /// <summary>
    /// Rating
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Rating", Description = "This is the Rating field.", Prompt = "Enter Rating")]
    public decimal? Rating { get; set; }

    /// <summary>
    /// Vote Count
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Vote Count", Description = "This is the Vote Count field.", Prompt = "Enter Vote Count")]
    public int? VoteCount { get; set; }

    /// <summary>
    /// Create Date Time
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Create Date Time", Description = "This is the Create Date Time field.", Prompt = "Enter Create Date Time")]
    [DataType(DataType.DateTime)]
    public DateTime CreateDateTime { get; set; }

    /// <summary>
    /// Create User Name
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Create User Name", Description = "This is the Create User Name field.", Prompt = "Enter Create User Name")]
    [StringLength(255)]
    public string CreateUserName { get; set; }

    /// <summary>
    /// Change Date Time
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Change Date Time", Description = "This is the Change Date Time field.", Prompt = "Enter Change Date Time")]
    [DataType(DataType.DateTime)]
    public DateTime ChangeDateTime { get; set; }

    /// <summary>
    /// Change User Name
    /// </summary>
    [JsonIgnore]
    [Display(Name = "Change User Name", Description = "This is the Change User Name field.", Prompt = "Enter Change User Name")]
    [StringLength(255)]
    public string ChangeUserName { get; set; }

    /// <summary>
    /// New Instance of Joke
    /// </summary>
    public Joke()
    {
        JokeId = 0;
        JokeTxt = string.Empty;
        JokeCategoryId = null;
        JokeCategoryTxt = string.Empty;
        Attribution = string.Empty;
        ImageTxt = string.Empty;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of Joke
    /// </summary>
    public Joke(int jokeId)
    {
        JokeId = jokeId;
        JokeTxt = string.Empty;
        JokeCategoryId = null;
        JokeCategoryTxt = string.Empty;
        Attribution = string.Empty;
        ImageTxt = string.Empty;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of Joke
    /// </summary>
    public Joke(string jokeTxt)
    {
        JokeId = 0;
        JokeTxt = jokeTxt;
        JokeCategoryId = null;
        JokeCategoryTxt = string.Empty;
        Attribution = string.Empty;
        ImageTxt = jokeTxt;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of Joke
    /// </summary>
    public Joke(string jokeTxt, string jokeCategoryTxt)
    {
        JokeId = 0;
        JokeTxt = jokeTxt;
        JokeCategoryId = null;
        JokeCategoryTxt = jokeCategoryTxt;
        Attribution = string.Empty;
        ImageTxt = jokeTxt;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of Joke
    /// </summary>
    public Joke(string jokeTxt, string jokeCategoryTxt, string imageTxt)
    {
        JokeId = 0;
        JokeTxt = jokeTxt;
        JokeCategoryId = null;
        JokeCategoryTxt = jokeCategoryTxt;
        Attribution = string.Empty;
        ImageTxt = imageTxt;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of Joke
    /// </summary>
    public Joke(int jokeId, string jokeTxt, int jokeCategory, string jokeCategoryTxt, string attribution, string imageTxt)
    {
        JokeId = jokeId;
        JokeTxt = jokeTxt;
        JokeCategoryId = jokeCategory;
        JokeCategoryTxt = jokeCategoryTxt;
        Attribution = attribution;
        ImageTxt = imageTxt;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }
}
