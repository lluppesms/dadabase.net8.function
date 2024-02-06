//-----------------------------------------------------------------------
// <copyright file="JokeRating.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Joke Rating Table
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Data;

[ExcludeFromCodeCoverage]
/// <summary>
/// JokeRating Table
/// </summary>
[Table("JokeRating")]
public class JokeRating
{
    //CREATE TABLE [dbo].[JokeRating](
    //	[JokeRatingId] [int] IDENTITY(1,1) NOT NULL,
    //	[JokeId] [int] NOT NULL,
    //	[UserRating] [int] NOT NULL,
    //	[CreateDateTime] [datetime] NOT NULL,
    //	[CreateUserName] [nvarchar](255) NOT NULL,

    /// <summary>
    /// JokeRating Id
    /// </summary>
    [Key, Column(Order = 0)]
    [Required(ErrorMessage = "JokeRatingId is required")]
    [Display(Name = "JokeRatingId", Description = "This is the JokeRatingId field.", Prompt = "Enter JokeRatingId")]
    public int JokeRatingId { get; set; }

    /// <summary>
    /// JokeId
    /// </summary>
    [Required(ErrorMessage = "JokeId is required")]
    [Display(Name = "JokeId", Description = "This is the JokeId field.", Prompt = "Enter JokeId")]
    public int JokeId { get; set; }

    /// <summary>
    /// User Rating
    /// </summary>
    [Display(Name = "User Rating", Description = "This is the User Rating field.", Prompt = "Enter User Rating")]
    public int UserRating { get; set; }

    /// <summary>
    /// Create Date Time
    /// </summary>
    [Display(Name = "Create Date Time", Description = "This is the Create Date Time field.", Prompt = "Enter Create Date Time")]
    [DataType(DataType.DateTime)]
    public DateTime CreateDateTime { get; set; }

    /// <summary>
    /// Create User Name
    /// </summary>
    [Display(Name = "Create User Name", Description = "This is the Create User Name field.", Prompt = "Enter Create User Name")]
    [StringLength(255)]
    public string CreateUserName { get; set; }

    /// <summary>
    /// New Instance of JokeRating
    /// </summary>
    public JokeRating()
    {
        JokeRatingId = 0;
        JokeId = 0;
        UserRating = 0;
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of JokeRating
    /// </summary>
    public JokeRating(int jokeId)
    {
        JokeRatingId = 0;
        JokeId = jokeId;
        UserRating = 0;
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
    }

    /// <summary>
    /// New Instance of JokeRating
    /// </summary>
    public JokeRating(int jokeId, int userRating)
    {
        JokeRatingId = 0;
        JokeId = jokeId;
        UserRating = userRating;
        CreateUserName = "UNKNOWN";
        CreateDateTime = DateTime.UtcNow;
    }
}
