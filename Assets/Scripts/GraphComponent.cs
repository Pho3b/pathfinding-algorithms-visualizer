using System.Collections.Generic;
using UnityEngine;

public class GraphComponent
{
    Tile[,] matrix;


    /// <summary>
    /// Default constructor with given matrix
    /// </summary>
    /// <param name="matrix">The graph matrix the algorithms will be applied on</param>
    public GraphComponent(ref Tile[,] matrix)
    {
        this.matrix = matrix;
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
        WaitForSeconds wfs = new WaitForSeconds(0.06f);
        Stack<Tile> stack = new Stack<Tile>();
        stack.Push(from);

        while (stack.Count > 0)
        {
            Tile t = stack.Pop();
            t.Visit();

            yield return wfs;

            if (t.x > 0 && matrix[t.x - 1, t.y].visited == false)
            {
                AddToVisit(matrix[t.x - 1, t.y], stack);
                yield return wfs;
            }

            if (t.y > 0 && matrix[t.x, t.y - 1].visited == false)
            {
                AddToVisit(matrix[t.x, t.y - 1], stack);
                yield return wfs;
            }

            if (t.x < matrix.GetLength(0) - 1 && matrix[t.x + 1, t.y].visited == false)
            {
                AddToVisit(matrix[t.x + 1, t.y], stack);
                yield return wfs;
            }

            if (t.y < matrix.GetLength(1) - 1 && matrix[t.x, t.y + 1].visited == false)
            {
                AddToVisit(matrix[t.x, t.y + 1], stack);
                yield return wfs;
            }

        }
    }

    private void AddToVisit(Tile tile, Stack<Tile> stack)
    {
        stack.Push(tile);
        tile.ToVisit();
    }
}
