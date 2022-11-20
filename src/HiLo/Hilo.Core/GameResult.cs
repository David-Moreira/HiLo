namespace Hilo.Core;

/// <summary>
/// The GameResult. Whether a winner was found, or some error occurred.
/// </summary>
public class GameResult
{
    public string Winner { get; set; }
    public int Attempts { get; set; }

    public bool Error { get; set; }

    public GameErrorType ErrorType { get; set; }

    public string ErrorMessage { get; set; }


    public GameResult(string winner, int attempts)
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

    public static GameResult EndWithError(GameErrorType errorType, string error)
        => new GameResult(errorType, error);

    public static GameResult End(string winner, int attempts)
     => new GameResult(winner, attempts);

}
