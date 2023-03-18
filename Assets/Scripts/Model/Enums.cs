public class Enums
{
    /// <summary>
    /// Enumerator representing possible Tile states.
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
    /// Enumerator representing supported algorithms
    /// </summary>
    public enum Algorithm
    {
        DepthFirstSearch,
        BreadthFirstSearch,
        DjikstraAlgorithm,
        AStarAlgorithm,
        BellmanFordAlgorithm,
    }
}
