/// <summary>
/// HiLow Game engine, bootstraps the necessary components needed for the game to run.
/// </summary>
public class GameEngine
{
    private string ERROR_UNEXPECTED = "An unexpected error occurred.";
    private string ERROR_ABORTED = "Game was aborted.";
    private readonly Func<string> _input;
    private readonly Action<string> _output;
    private int mysteryNumber;

    public GameEngine(Func<string> Input, Action<string> Output)
    {
        _input = Input;
        _output = Output;
    }

    public Task<GameResult> Start(int min, int max, CancellationToken cancellationToken = default)
    {
        try
        {
            mysteryNumber = Random.Shared.Next(min, max);
            var attempts = 0;
            var win = false;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return Task.FromResult(GameResult.EndWithError(GameErrorType.Aborted, ERROR_ABORTED));
                }

                attempts++;
                _output("Guess the number: ");

                var input = _input();

                if (int.TryParse(input, out var parsedInput))
                {
                    if (mysteryNumber == parsedInput)
                    {
                        return Task.FromResult(GameResult.End("Player 1", attempts));
                    }

                    if (mysteryNumber > parsedInput)
                    {
                        _output("LO");
                        continue;
                    }

                    if (mysteryNumber < parsedInput)
                    {
                        _output("HI");
                        continue;
                    }


                }
                else
                {
                    _output("Please provide a valid number!");
                }

            }
        }
        catch (Exception)
        {
            //Log //Handle Error
            return Task.FromResult(GameResult.EndWithError(GameErrorType.Unexpected, ERROR_UNEXPECTED));
        }

    }
}
