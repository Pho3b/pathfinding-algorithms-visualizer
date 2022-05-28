public class Enums
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

    /// <summary>
    /// Supported algorithms enumerator
    /// </summary>
    public enum Algorithm
    {
        DepthFirstSearch,
        BreadthFirstSearch,
        DjikstraAlgorithm,
    }
}
