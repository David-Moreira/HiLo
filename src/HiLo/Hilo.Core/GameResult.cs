namespace Hilo.Core;

/// <summary>
/// The GameResult. Whether a winner was found, or some error occurred.
/// </summary>
public class GameResult
{
    /// <summary>
    /// Winner of this game.
    /// </summary>
    public Player Winner { get; private set; }

    /// <summary>
    /// Number of attempts.
    /// </summary>
    public int Attempts { get; private set; }

    /// <summary>
    /// Game had an error.
    /// </summary>
    public bool Error { get; private set; }

    /// <summary>
    /// Type of Game Error.
    /// </summary>
    public GameErrorType ErrorType { get; private set; } = GameErrorType.None;

    /// <summary>
    /// Custom Error Message.
    /// </summary>
    public string ErrorMessage { get; private set; }


    public GameResult(Player winner, int attempts)
    {
        Winner = winner;
        Attempts = attempts;
    }

    public GameResult(GameErrorType errorType, string errorMessage)
    {
        Error = true;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// The game has found an error and should be ended.
    /// </summary>
    /// <param name="errorType"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static GameResult EndWithError(GameErrorType errorType, string error)
        => new GameResult(errorType, error);

    /// <summary>
    /// A winner was found and game ends.
    /// </summary>
    /// <param name="winner"></param>
    /// <param name="attempts"></param>
    /// <returns></returns>
    public static GameResult End(Player winner, int attempts)
        => new GameResult(winner, attempts);

}
