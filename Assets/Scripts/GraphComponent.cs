using System.Collections.Generic;
using UnityEngine;

public class GraphComponent : MonoBehaviour
{
    [SerializeField] private GridComponent gridComponent;
    private readonly WaitForSeconds wfs = new WaitForSeconds(0.02f);
    private short[] rd = new short[4] { -1, +1, 0, 0 };
    private short[] cd = new short[4] { 0, 0, -1, +1 };
    private Tile[,] matrix;


    /// <summary>
    /// Default Awake (Constructor)
    /// </summary>
    /// <param name="matrix">The graph matrix that the algorithms will be applied on</param>
    private void Awake()
    {
        matrix = gridComponent.tilesMatrix;
    }


    /// <summary>
    /// Performs a BreadthFirstSearch on the given matrix(graph)
    /// If the 'to' vertex is given, the search will stop as soon as it will find it
    /// </summary>
    /// <param name="from">Starting vertex of the search</param>
    /// <param name="to">Ending vertex of the search</param>
    /// <returns></returns>
    public IEnumerator<WaitForSeconds> BreadthFirstSearch(Tile from, Tile to = null)
    {
        if (from.id == to.id)
        {
            from.Found();
            yield return null;
        }

        Stack<Tile> stack = new Stack<Tile>();
        stack.Push(from);

        while (stack.Count > 0)
        {
            Tile tile = stack.Pop();
            tile.Visited();

            yield return wfs;

            StartCoroutine(AddAdjacentTiles(tile, to, stack));
        }
    }

    /// <summary>
    /// Cycles over the 4 possibile grid directions and adds the valid tiles to the stack to visit them later.
    /// </summary>
    /// <param name="from">The starting tile(vertex)</param>
    /// <param name="stack">The stack where the Tiles that needs to be visited are stored</param>
    /// <returns>The current instance waitForSeconds when a tile is added to the stack</returns>
    private IEnumerator<WaitForSeconds> AddAdjacentTiles(Tile from, Tile to, Stack<Tile> stack)
    {
        for (byte i = 0; i < 4; i++)
        {
            Tile currentTile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);

            if (to != null && currentTile != null && currentTile.id == to.id)
            {
                from.Found();
                break;
            }
            else if (currentTile != null)
            {
                currentTile.ToVisit();
                stack.Push(currentTile);

                yield return wfs;
            }
        }
    }

    /// <summary>
    /// Returns a tile if it is a valid one, if it's not, NULL is returned.
    /// </summary>
    /// <param name="row">The X axis of the Tile that tries to retrieve</param>
    /// <param name="column">The Y axis of the Tile that tries to retrieve</param>
    /// <returns>The requested tile or NULL in case it not a valid tile</returns>
    /// <note>A tile is 'invalid' when it is out of the matrix bounds or it has been manually set as 'invalid'</note>
    private Tile RetrieveAdjacentTile(int row, int column)
    {
        if (
            row < 0
            || row > matrix.GetLength(0) - 1
            || column < 0
            || column > matrix.GetLength(1) - 1
            || matrix[row, column].visited
        )
        {
            return null;
        }

        return matrix[row, column];
    }

}
