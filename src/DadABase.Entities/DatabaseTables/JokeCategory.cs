//-----------------------------------------------------------------------
// <copyright file="JokeCategory.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// JokeCategory Table
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Data;

[ExcludeFromCodeCoverage]
/// <summary>
/// JokeCategory Table
/// </summary>
[Table("JokeCategory")]
public class JokeCategory
{
    //CREATE TABLE [dbo].[JokeCategory](
    //	[JokeCategoryId] [int] IDENTITY(1,1) NOT NULL,
    //	[JokeCategoryTxt] [nvarchar](500) NULL,
    //	[SortOrderNbr] [int] NOT NULL,
    //	[ActiveInd] [nchar](1) NOT NULL,
    //	[CreateDateTime] [datetime] NOT NULL,
    //	[CreateUserName] [nvarchar](255) NOT NULL,
    //	[ChangeDateTime] [datetime] NOT NULL,
    //	[ChangeUserName] [nvarchar](255) NOT NULL,

    /// <summary>
    /// JokeCategory Id
    /// </summary>
    [Key, Column(Order = 0)]
    [Required(ErrorMessage = "JokeCategoryId is required")]
    [Display(Name = "JokeCategoryId", Description = "This is the JokeCategoryId field.", Prompt = "Enter JokeCategoryId")]
    public int JokeCategoryId { get; set; }
    
    /// <summary>
    /// Joke Category Text
    /// </summary>
    [Display(Name = "Joke Category Text", Description = "This is the Joke Category Text field.", Prompt = "Enter Joke TCategory ext")]
    [StringLength(500)]
    [Required(ErrorMessage = "Joke Category Text is required")]
    public string JokeCategoryTxt { get; set; }

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
    /// New Instance of JokeCategory
    /// </summary>
    public JokeCategory()
    {
        JokeCategoryId = 0;
        JokeCategoryTxt = string.Empty;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of JokeCategory
    /// </summary>
    public JokeCategory(int jokeCategoryId)
    {
        JokeCategoryId = jokeCategoryId;
        JokeCategoryTxt = string.Empty;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of JokeCategory
    /// </summary>
    public JokeCategory(string jokeCategoryTxt)
    {
        JokeCategoryId = 0;
        JokeCategoryTxt = jokeCategoryTxt;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of JokeCategory
    /// </summary>
    public JokeCategory(int jokeCategoryId, string jokeCategoryTxt)
    {
        JokeCategoryId = jokeCategoryId;
        JokeCategoryTxt = jokeCategoryTxt;
        SortOrderNbr = 50;
        ActiveInd = "Y";
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
        ChangeUserName = "UNKNOWN";
        ChangeDateTime = DateTime.UtcNow;
    }
}
