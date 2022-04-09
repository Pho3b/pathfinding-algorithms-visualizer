
public class AdjacencyMatrixGraph
{
    private readonly int[,] matrix;
    private int numberOfEdges = 0;


    /// <summary>
    /// Constructor with given number of vertices
    /// </summary>
    /// 
    /// <param name="verticesNumber">The starting vertices number of the Graph</param>
    public AdjacencyMatrixGraph(int verticesNumber)
    {
        matrix = new int[verticesNumber, verticesNumber];
    }

    /// <summary>
    /// Adds a new edge from the given 'from' vertex to the 'to' vertex
    /// </summary>
    /// 
    /// <param name="from">Starting vertex's edge</param>
    /// <param name="to">Ending vertex's edge</param>
    /// <param name="isUndirected">Whether to add the reverse edge or not</param>
    /// <returns>false if the given vertices outbounds the matrix size, true otherwise</returns>
    public bool AddEdge(int from, int to, bool isUndirected = false)
    {
        if (from > matrix.Length || to > matrix.Length || from == to)
            return false;

        if (matrix[from, to] == 0)
        {
            numberOfEdges++;
            matrix[from, to] = 1;
        }

        if (isUndirected && matrix[to, from] == 0)
        {
            numberOfEdges++;
            matrix[to, from] = 1;
        }

        return true;
    }

}
