using System.Collections.Generic;
using UnityEngine;

public class GraphComponent : MonoBehaviour
{
    [SerializeField] private GridComponent gridComponent;
    private Tile[,] matrix;
    private short[] rd = new short[4] { -1, +1, 0, 0 };
    private short[] cd = new short[4] { 0, 0, -1, +1 };


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
        WaitForSeconds wfs = new WaitForSeconds(0.1f);
        Stack<Tile> stack = new Stack<Tile>();
        stack.Push(from);

        while (stack.Count > 0)
        {
            Tile t = stack.Pop();
            t.Visited();

            yield return wfs;

            StartCoroutine(ParseNeighborhood(from, stack, wfs));
            print(stack.Count);
        }
    }

    private IEnumerator<WaitForSeconds> ParseNeighborhood(Tile tile, Stack<Tile> stack, WaitForSeconds wfs)
    {
        for (byte i = 0; i < 4; i++)
        {
            int row = tile.x + rd[i];
            int column = tile.y + cd[i];

            if (
                row < 0
                || row > matrix.GetLength(0)
                || column < 0
                || column > matrix.GetLength(1)
                || matrix[row, column].visited
            )
            {
                continue;
            }

            stack.Push(matrix[row, column]);
            matrix[row, column].ToVisit();

            yield return wfs;
        }
    }

    private void AddToVisit(Tile tile, Stack<Tile> stack)
    {
        stack.Push(tile);
        tile.ToVisit();
    }
}
