namespace DadABase.Data;

public class BackupData
{
    public List<JokeCategory> Categories { get; set; }
    public List<Joke> Jokes { get; set; }
    public List<JokeRating> Ratings { get; set; }
    public BackupData()
    {
        Categories = new List<JokeCategory>();
        Jokes = new List<Joke>();
        Ratings = new List<JokeRating>();
    }
}
