namespace Hilo.Core;

public class Player
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string Name { get; init; }

    public Func<string> Input { get; init; }


    public Player(string name, Func<string> input )
    {
        Name = name;
        Input = input;
    }
}

public class HiLoPlayer : Player
{
    public int Attempts { get; private set; }
    public HiLoPlayer(string name, Func<string> input) : base(name, input)
    {
    }

    public void IncrementAttempt() => Attempts++;

}
