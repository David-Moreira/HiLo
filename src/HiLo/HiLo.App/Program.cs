Console.WriteLine("HiLo!");

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