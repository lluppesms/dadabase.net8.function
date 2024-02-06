namespace DadABase.Data;

public class JokeList
{
    public List<Joke> Jokes { get; set; }
    public JokeList()
    {
        Jokes = new List<Joke>();
    }
}
