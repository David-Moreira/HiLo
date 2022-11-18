Console.WriteLine( "HiLo!" );

var game = new GameEngine( Console.ReadLine, Console.WriteLine );
await game.Start( 1, 10 );
