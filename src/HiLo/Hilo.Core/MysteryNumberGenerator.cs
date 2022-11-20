namespace Hilo.Core;

public class MysteryNumberGenerator : IMysteryNumberGenerator
{
    public int Generate(int min, int max)
        => Random.Shared.Next(min, max);

}
