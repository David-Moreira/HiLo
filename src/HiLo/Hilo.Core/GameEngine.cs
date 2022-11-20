namespace Hilo.Core;

/// <summary>
/// HiLow Game engine, bootstraps the necessary components needed for the game to run.
/// </summary>
public class GameEngine
{
    private string ERROR_MIN = "Min range for mystery number can't be higher then provided max.";
    private string ERROR_MAX = "Max range for mystery number can't be lower then provided min.";
    private string ERROR_NO_PLAYERS = "No players to start the game.";
    private string ERROR_UNEXPECTED = "An unexpected error occurred.";
    private string ERROR_ABORTED = "Game was aborted.";
    private readonly IReadOnlyList<HiLoPlayer> _players;
    private readonly Action<string> _output;
    private IMysteryNumberGenerator _mysteryNumberGenerator;
    private int _mysteryNumber;
    private int _nextPlayerIdx = 0;

    public GameEngine(IMysteryNumberGenerator mysteryNumberGenerator, IReadOnlyList<HiLoPlayer> players, Action<string> Output)
    {
        _mysteryNumberGenerator = mysteryNumberGenerator;
        _players = players;
        _output = Output;
    }

    private HiLoPlayer GetNextPlayer()
    {
        if (_nextPlayerIdx == _players.Count)
            _nextPlayerIdx = 0;
        return _players.ElementAt(_nextPlayerIdx++);
    }

    private void Reset()
    {
        _mysteryNumber = 0;
        _nextPlayerIdx = 0;
    }

    /// <summary>
    /// Validates if the game has valid arguments to start.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private (bool, GameResult) IsValidGame(int min, int max)
    {
        if (!_players?.Any() ?? true)
            return (false, (GameResult.EndWithError(GameErrorType.NoPlayers, ERROR_UNEXPECTED)));

        if (min > max)
            return (false, (GameResult.EndWithError(GameErrorType.InvalidArguments, ERROR_MIN)));

        if (max < min)
            return (false, (GameResult.EndWithError(GameErrorType.InvalidArguments, ERROR_MAX)));

        return (true, null);
    }

    /// <summary>
    /// Starts a game with the provided players, generating a mystery number according to the provided min/max.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<GameResult> Start(int min, int max, CancellationToken cancellationToken = default)
    {
        try
        {
            Reset();

            var (valid, result) = IsValidGame(min, max);
            if (!valid)
                return Task.FromResult(result);

            _mysteryNumber = _mysteryNumberGenerator.Generate(min, max);

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
