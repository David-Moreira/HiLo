namespace Hilo.Core;
/// <summary>
/// HiLow Game engine, bootstraps the necessary components needed for the game to run.
/// </summary>
public class GameEngine
{
    private string ERROR_UNEXPECTED = "An unexpected error occurred.";
    private string ERROR_ABORTED = "Game was aborted.";
    private readonly IReadOnlyList<HiLoPlayer> _players;
    private readonly Action<string> _output;
    private int _mysteryNumber;
    private int _nextPlayerIdx = 0;

    public GameEngine(IReadOnlyList<HiLoPlayer> players, Action<string> Output)
    {
        _players = players;
        _output = Output;
    }

    private HiLoPlayer GetNextPlayer()
    {
        if (_nextPlayerIdx == _players.Count)
            _nextPlayerIdx = 0;
        return _players.ElementAt(_nextPlayerIdx++);
    }

    public Task<GameResult> Start(int min, int max, CancellationToken cancellationToken = default)
    {
        try
        {
            _mysteryNumber = Random.Shared.Next(min, max);
            
            _players.ElementAt(_nextPlayerIdx);
            
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return Task.FromResult(GameResult.EndWithError(GameErrorType.Aborted, ERROR_ABORTED));
                }

                var player = GetNextPlayer();
                player.IncrementAttempt();

                _output($"{player.Name}, Guess the number: ");

                var input = player.Input();

                if (int.TryParse(input, out var parsedInput))
                {
                    if (_mysteryNumber == parsedInput)
                    {
                        return Task.FromResult(GameResult.End(player, player.Attempts));
                    }

                    if (_mysteryNumber > parsedInput)
                    {
                        _output("LO");
                        continue;
                    }

                    if (_mysteryNumber < parsedInput)
                    {
                        _output("HI");
                        continue;
                    }


                }
                else
                {
                    _output($"{player.Name}, Please provide a valid number!");
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
