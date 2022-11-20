using Hilo.Core;

Console.WriteLine(Constants.NAME); 
Console.WriteLine("Try to guess the mystery number in the less attempts possible!");
Console.WriteLine("You'll be presented with Hi if your guess is above the mystery number.");
Console.WriteLine("You'll be presented with Lo if your guess is below the mystery number.");

var game = new GameEngine(Console.ReadLine, Console.WriteLine);
var gameResult = await game.Start(1, 10);

if (gameResult.Error)
{
    Console.WriteLine("Game wasn't completed.");
    Console.WriteLine(gameResult.ErrorMessage);
}
else
{
    Console.WriteLine($"Winner is {gameResult.Winner}! Congratulations!");
    Console.WriteLine($"Attempts: {gameResult.Attempts}");
}