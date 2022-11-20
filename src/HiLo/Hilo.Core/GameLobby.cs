namespace Hilo.Core;

/// <summary>
/// Sets up a Game Lobby waiting for players to start a new game.
/// </summary>
public class GameLobby
{
    /// <summary>
    /// Unique room Id
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Room Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Max Player quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The player's in this room.
    /// </summary>
    public IReadOnlyList<HiLoPlayer> Players => _players.AsReadOnly();


    private List<HiLoPlayer> _players;

    public GameLobby(int numOfPlayers)
    {
        //if (numOfPlayers > 4)
        //{
        //    throw new ArgumentException("Only up to 4 players are allowed");
        //}
        Quantity = numOfPlayers;
    }

    /// <summary>
    /// Adds a player to the room
    /// </summary>
    public void AddPlayer(HiLoPlayer player)
    {
        _players ??= new();
        _players.Add(player);
    }

    /// <summary>
    /// Removes a player from the room
    /// </summary>
    public void RemovePlayer(HiLoPlayer player)
    {
        _players ??= new();
        _players.Remove(player);
    }
}