//-----------------------------------------------------------------------
// <copyright file="JokeRepository.cs" company="Luppes Consulting, Inc.">
// Copyright 2024, Luppes Consulting, Inc. All rights reserved.
// </copyright>
// <summary>
// Joke Repository
// </summary>
//-----------------------------------------------------------------------

/* TODO:
  - why doesn't the nullable JsonProperty work - attribution only shows up it's null...?
  - start figuring out alert/health queries...  should I publish those into the LAW also...?
*/

namespace DadABase.Data;

/// <summary>
/// Joke Repository
/// </summary>
[ExcludeFromCodeCoverage]
public class JokeRepository : BaseRepository, IJokeRepository
{
    #region Variables
    /// <summary>
    /// List of Jokes
    /// </summary>
    private static JokeList JokeData = null;

    /// <summary>
    /// List of Categories
    /// </summary>
    private static List<string> JokeCategories = null;

    /// <summary>
    /// Source of JSON Jokes
    /// </summary>
    private static readonly string sourceFileName = "Jokes.json"; // "Data/Jokes.json";

    /// <summary>
    /// Logging component
    /// </summary>
    private ILogger _logger;

    ///// <summary>
    ///// Auto Mapper
    ///// </summary>
    //private IMapper Mapper;
    #endregion

    #region Initialization
    /// <summary>
    /// Constructor
    /// </summary>
    public JokeRepository()
    {
        //SetupAutoMapper();

        var rootDirectory = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + Path.DirectorySeparatorChar;
        var jokeFilePath = rootDirectory + sourceFileName;

        // load up the jokes into memory
        using (var r = new StreamReader(jokeFilePath))
        {
            var json = r.ReadToEnd();
            JokeData = JsonConvert.DeserializeObject<JokeList>(json);
        }

        // select distinct categories from JokeData
        JokeCategories = JokeData.Jokes.Select(joke => joke.JokeCategoryTxt).Distinct().Order().ToList();
    }

    /// <summary>
    /// Add Logger component to repository
    /// </summary>
    /// <param name="logger"></param>
    public void SetupLogger(ILogger logger)
    {
        _logger = logger;
    }
    #endregion

    /// <summary>
    /// Get a random joke
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Record</returns>
    public string GetRandomJokeText(string requestingUserName = "ANON")
    {
        try
        {
            var joke = GetRandomJoke(requestingUserName);
            var jokeTxt = string.IsNullOrEmpty(joke.Attribution) ? joke.Joke : $"{joke.Joke} ({joke.Attribution})";
            return jokeTxt;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger?.LogError($"Error in GetRandomJokeText: {error}");
            return error;
        }
    }

    /// <summary>
    /// Get a random joke
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Record</returns>
    public JokeBasicPlus GetRandomJokeSimpleJson(string requestingUserName = "ANON")
    {
        try
        {
            var joke = GetRandomJoke(requestingUserName);
            return joke ?? new JokeBasicPlus("No jokes here!", "None");
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger?.LogError($"Error in GetRandomJokeSimpleJson: {error}");
            return new JokeBasicPlus(error, "Error");
        }
    }

    /// <summary>
    /// Get a random joke
    /// </summary>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Record</returns>
    public JokeBasicPlus GetRandomJoke(string requestingUserName = "ANON")
    {
        try
        {
            var joke = JokeData.Jokes[Random.Shared.Next(0, JokeData.Jokes.Count)];
            return (joke == null) ? new JokeBasicPlus("No jokes here!", string.Empty) : new JokeBasicPlus(joke);
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger?.LogError($"Error in GetRandomJoke: {error}");
            return new JokeBasicPlus(error, "Error");
        }
    }

    /// <summary>
    /// Find Matching Jokes by Search Text and Category
    /// </summary>
    /// <param name="searchTxt">Search Text</param>
    /// <param name="jokeCategoryTxt">Category</param>
    /// <param name="requestingUserName">Requesting UserName</param>
    /// <returns>Records</returns>
    public IQueryable<JokeBasicPlus> SearchJokes(string searchTxt = "", string jokeCategoryTxt = "", string requestingUserName = "ANON")
    {
        try
        {
            List<string> jokeCategoryList = null;
            jokeCategoryTxt = jokeCategoryTxt.Equals("All", StringComparison.OrdinalIgnoreCase) ? string.Empty : jokeCategoryTxt;

            if (!string.IsNullOrEmpty(jokeCategoryTxt))
            {
                // split the jokeCategoryTxt into a list of categories by comma
                var jokeCategoryArray = jokeCategoryTxt.Split(',').ToList();
                jokeCategoryList = JokeCategories.Where(category => jokeCategoryArray.Contains(category)).Select(c => c).ToList();
            }

            // user supplied both category and search term
            if (!string.IsNullOrEmpty(jokeCategoryTxt) && !string.IsNullOrEmpty(searchTxt))
            {
                var jokesByTermAndCategory = JokeData.Jokes
                    .Where(joke => jokeCategoryList.Any(category => category == joke.JokeCategoryTxt)
                        && joke.JokeTxt.Contains(searchTxt, StringComparison.InvariantCultureIgnoreCase))
                    .Select(joke => new JokeBasicPlus(joke))
                    .ToList();
                return jokesByTermAndCategory.AsQueryable();
            }

            // user supplied ONLY category and NOT search term
            if (!string.IsNullOrEmpty(jokeCategoryTxt) && string.IsNullOrEmpty(searchTxt))
            {
                var jokesInCategory = JokeData.Jokes
                    .Where(joke => jokeCategoryList.Any(category => category == joke.JokeCategoryTxt))
                    .Select(joke => new JokeBasicPlus(joke))
                    .ToList();
                return jokesInCategory.AsQueryable();
            }

            // user supplied NOT category and ONLY search term
            if (string.IsNullOrEmpty(jokeCategoryTxt) && !string.IsNullOrEmpty(searchTxt))
            {
                var jokesByTerm = JokeData.Jokes
                    .Where(joke => joke.JokeTxt.Contains(searchTxt, StringComparison.InvariantCultureIgnoreCase))
                    .Select(joke => new JokeBasicPlus(joke))
                    .ToList();
                return jokesByTerm.AsQueryable();
            }

            // user supplied NEITHER category NOR search term - get a random joke
            var randomJoke = GetRandomJoke();
            return new List<JokeBasicPlus> { randomJoke }.AsQueryable();
            //return new List<Joke> { randomJoke }.AsQueryable();
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger?.LogError($"Error in SearchJokes: {error}");
            return (new List<JokeBasicPlus> { new(error, "Error") }).AsQueryable();
            //return (new List<Joke> { new(error, "Error") }).AsQueryable();
        }
    }

    /// <summary>
    /// List All Jokes
    /// </summary>
    /// <returns>List of Category Names</returns>
    public IQueryable<JokeBasicPlus> ListAll(string requestingUserName = "ANON")
    {
        try
        {
            var allJokes= JokeData.Jokes
                .Select(joke => new JokeBasicPlus(joke))
                .ToList();
            return allJokes.AsQueryable();
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger?.LogError($"Error in ListAll: {error}");
            return (new List<JokeBasicPlus> { new(error, "Error") }).AsQueryable();
            //return (new List<Joke> { new(error, "Error") }).AsQueryable();
        }
    }

    /// <summary>
    /// Get Joke Categories
    /// </summary>
    /// <returns>List of Category Names</returns>
    public IQueryable<string> GetJokeCategories(string requestingUserName)
    {
        try
        {
            return JokeCategories.AsQueryable();
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger?.LogError($"Error in GetJokeCategories: {error}");
            return (new List<string> { error }).AsQueryable();
        }
    }

    //// --------------------------------------------------------------------------------------------------------------
    ////  NOT IMPLEMENTED YET!
    //// --------------------------------------------------------------------------------------------------------------
    //public JokeBasicPlus GetOne(int id, string requestingUserName = "ANON")
    //{
    //    throw new NotImplementedException();
    //}

    //public bool DupCheck(int keyValue, string dscr, ref string fieldName, ref string errorMessage)
    //{
    //    throw new NotImplementedException();
    //}

    //public bool Add(Joke Joke, string requestingUserName = "ANON")
    //{
    //    throw new NotImplementedException();
    //}

    //public bool DeleteCheck(int id, ref string errorMessage, string requestingUserName = "ANON")
    //{
    //    throw new NotImplementedException();
    //}

    //public bool Delete(int id, string requestingUserName = "ANON")
    //{
    //    throw new NotImplementedException();
    //}

    //public bool Save(int id, Joke joke, string requestingUserName = "ANON")
    //{
    //    throw new NotImplementedException();
    //}

    //public decimal AddRating(JokeRating jokeRating, string requestingUserName = "ANON")
    //{
    //    throw new NotImplementedException();
    //}

    ///// <summary>
    ///// Export Data
    ///// </summary>
    ///// <returns>Success</returns>
    //public bool ExportData(string fileName)
    //{
    //    using (var r = new StreamReader(sourceFileName))
    //    {
    //        var json = r.ReadToEnd();
    //        using (var w = new StreamWriter(fileName))
    //        {
    //            w.Write(json);
    //        }
    //    }
    //    return true;
    //}

    ///// <summary>
    ///// Import Data
    ///// </summary>
    ///// <returns>Success</returns>
    //public bool ImportData(string data)
    //{
    //    throw new NotImplementedException();
    //    // -- this *should* work, but hasn't been tested and we'd had to put in a file upload capability...
    //    //using (var w = new StreamWriter(sourceFileName))
    //    //{
    //    //    w.Write(data);
    //    //}
    //    //return true;
    //}

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    ///// <summary>
    ///// Set up Auto Mapper --- need to do this in startup, not here...!
    ///// </summary>
    //public void SetupAutoMapper()
    //{
    //    if (Mapper == null)
    //    {
    //        var mapperConfig = new MapperConfiguration(cfg =>
    //        {
    //            cfg.CreateMap<Joke, JokeBasic>()
    //                .ForMember(dest => dest.Joke, opt => opt.MapFrom(src => src.JokeTxt))
    //                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.JokeCategoryTxt));
    //            //.ForMember(dest => dest.ImageText, opt => opt.MapFrom(src => src.ImageTxt));
    //            cfg.CreateMap<Joke, JokeBasicPlus>()
    //                .ForMember(dest => dest.Joke, opt => opt.MapFrom(src => src.JokeTxt))
    //                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.JokeCategoryTxt))
    //                .ForMember(dest => dest.Attribution, opt => opt.MapFrom(src => src.Attribution));
    //            cfg.CreateMap<JokeCategory, CategoryBasic>()
    //                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.JokeCategoryTxt));
    //        });
    //        mapperConfig.AssertConfigurationIsValid();
    //        Mapper = mapperConfig.CreateMapper();
    //    }
    //}
}