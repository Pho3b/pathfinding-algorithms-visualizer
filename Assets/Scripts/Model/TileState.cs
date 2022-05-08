public partial class Tile
{
    /// <summary>
    /// Enumerator representing an eloquent reference to the basic possible Tile states.
    /// </summary>
    public enum TileState
    {
        Base,
        Visited,
        Found,
        ToVisit,
        ToSearch,
        Obstacle,
    }
}
