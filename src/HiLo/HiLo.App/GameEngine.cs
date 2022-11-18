// See https://aka.ms/new-console-template for more information

/// <summary>
/// HiLow Game engine, bootstraps the necessary components needed for the game to run.
/// </summary>
public class GameEngine
{
    private readonly Func<string> _input;
    private readonly Action<string> _output;
    private int mysteryNumber;

    public GameEngine( Func<string> Input, Action<string> Output )
    {
        _input = Input;
        _output = Output;
    }

    public Task Start( int min, int max, CancellationToken cancellationToken = default )
    {
        mysteryNumber = Random.Shared.Next( min, max );
        var attempts = 0;
        var win = false;
        while ( true )
        {
            if ( cancellationToken.IsCancellationRequested )
                break;

            attempts++;
            _output( "Guess the number: " );

            var input = _input();

            if ( int.TryParse( input, out var parsedInput ) )
            {
                if ( mysteryNumber == parsedInput )
                {
                    win = true;
                    break;
                }

                if ( mysteryNumber > parsedInput )
                {
                    _output( "LO" );
                    continue;
                }

                if ( mysteryNumber < parsedInput )
                {
                    _output( "HI" );
                    continue;
                }


            }
            else
            {
                _output( "Please provide a valid number!" );
            }

        }

        if ( win )
            _output( "Winner!" );

        _output( $"Your attempts: {attempts}" );

        return Task.CompletedTask;

    }
}