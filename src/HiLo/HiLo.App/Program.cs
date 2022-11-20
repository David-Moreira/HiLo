using System.Security.Cryptography;

using Hilo.Core;

Console.WriteLine(Constants.NAME); 
Console.WriteLine("Try to guess the mystery number in the less attempts possible!");
Console.WriteLine("You'll be presented with HI if your guess is above the mystery number.");
Console.WriteLine("You'll be presented with LO if your guess is below the mystery number.");

Console.WriteLine();

Console.WriteLine("Press any key to Start!");
Console.Read();
Console.Clear();


var input = string.Empty;
var numOfPlayers = 0;
Console.WriteLine("Insert number of players:");
while (!int.TryParse(input, out numOfPlayers)){
    input = Console.ReadLine();
}

var lobby = new GameLobby(numOfPlayers);
for (int i = 0; i < numOfPlayers; i++)
{
    Console.WriteLine($"Insert Player {i+1} Name: ");
    var playerName = Console.ReadLine();
    lobby.AddPlayer(new(playerName, Console.ReadLine));
}

var game = new GameEngine(new MysteryNumberGenerator(), lobby.Players, Console.WriteLine);
var gameResult = await game.Start(1, 10);

if (gameResult.Error)
{
    Console.WriteLine("Game wasn't completed.");
    Console.WriteLine(gameResult.ErrorMessage);
}
else
{
    Console.WriteLine($"Winner is {gameResult.Winner.Name}! Congratulations!");
    Console.WriteLine($"Attempts: {gameResult.Attempts}");
}