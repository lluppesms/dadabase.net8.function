//-----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Application Settings
// </summary>
//-----------------------------------------------------------------------
namespace DadABase.Data;

/// <summary>
/// Application Settings
/// </summary>
[ExcludeFromCodeCoverage]
public class AppSettings
{
    /// <summary>
    /// App Version
    /// </summary>
    public string AppVersion { get; set; }

    /// <summary>
    /// App Title
    /// </summary>
    public string AppTitle { get; set; }

    /// <summary>
    /// App Description
    /// </summary>
    public string AppDescription { get; set; }

    /// <summary>
    /// Environment Name
    /// </summary>
    public string EnvironmentName { get; set; }

    /// <summary>
    /// User Name
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Super User First Name
    /// </summary>
    public string SuperUserFirstName { get; set; }

    /// <summary>
    /// Super User Last Name
    /// </summary>
    public string SuperUserLastName { get; set; }

    /// <summary>
    /// Application Insights Key
    /// </summary>
    public string AppInsights_InstrumentationKey { get; set; }

    /// <summary>
    /// Generic API Key
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// OpenAI API Key
    /// </summary>
    public string OpenAIApiKey { get; set; }
    
    /// <summary>
    /// Name of Azure OpenAI Resource
    /// </summary>
    public string OpenAIResourceName { get; set; }

    /// <summary>
    /// Dall-E Image API Key
    /// </summary>
    public string DallEApiKey { get; set; }

    /// <summary>
    /// OpenAI API URL for creating an image
    /// </summary>
    public string OpenAIImageGenerateUrl { get; set; }

    /// <summary>
    /// OpenAI API URL for editing an image
    /// </summary>
    public string OpenAIImageEditUrl { get; set; }

    /// <summary>
    /// OpenAI API Image Size
    /// </summary>
    public string OpenAIImageSize { get; set; }

    /// <summary>
    /// Data Source
    /// </summary>
    public string DataSource { get; set; }

    /// <summary>
    /// Default Connection 
    /// </summary>
    public string DefaultConnection { get; set; }

    /// <summary>
    /// Project Entities
    /// </summary>
    public string ProjectEntities { get; set; }

    /// <summary>
    /// Should I use the local database or the Azure one...?
    /// </summary>
    public bool EnableSwagger { get; set; }

    /// <summary>
    /// Email From
    /// </summary>
    public string EmailFrom { get; set; }

    /// <summary>
    /// Send Errors To
    /// </summary>
    public string SendErrorsTo { get; set; }

    /// <summary>
    /// Send Grid Server
    /// </summary>
    public string SendGridServer { get; set; }

    /// <summary>
    /// Send Grid User
    /// </summary>
    public string SendGridUserId { get; set; }

    /// <summary>
    /// Send Grid Password
    /// </summary>
    public string SendGridPassword { get; set; }

    /// <summary>
    /// Application Settings
    /// </summary>
    public AppSettings()
    {
        UserName = string.Empty;
    }
}
