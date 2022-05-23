using Assets.Scripts.Algorithms;
using System.Collections.Generic;
using UnityEngine;

class DepthFirstSearch : Algorithm
{
    /// <summary>
    /// Performs a DepthFirstSearch on the current matrix(graph)
    /// If the 'to' vertex is given, the search will stop as soon as it will find it
    /// </summary>
    /// <param name="from">Starting vertex of the search</param>
    /// <param name="to"> Optional ending vertex of the search</param>
    /// <returns></returns>
    public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
    {
        Stack<Tile> stack = new Stack<Tile>();
        stack.Push(from);

        while (stack.Count > 0 && !GraphComponent.found)
        {
            Tile tile = stack.Pop();
            tile.SetState(Enums.TileState.Visited);

            yield return wfs;

            StartCoroutine(AddAdjacentTiles(tile, to, stack));
        }

        GraphComponent.isAlgorithmRunning = false;
    }

    /// <summary>
    /// Cycles over the 4 possibile grid directions and adds the valid tiles to the stack to visit them later.
    /// </summary>
    /// <param name="from">The starting tile(vertex)</param>
    /// <param name="stack">The stack where the Tiles that needs to be visited are stored</param>
    /// <returns>The current instance 'wfs' attribute when a tile is added to the stack</returns>
    private IEnumerator<WaitForSeconds> AddAdjacentTiles(Tile from, Tile to, Stack<Tile> stack)
    {
        for (byte i = 0; i < Constant.DirectionsNumber; i++)
        {
            Tile currentTile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);

            if (to != null && currentTile != null && (currentTile.id == to.id || to.id == from.id))
            {
                to.SetState(Enums.TileState.Found);
                GraphComponent.found = true;
                break;
            }
            else if (currentTile != null)
            {
                currentTile.SetState(Enums.TileState.ToVisit);
                stack.Push(currentTile);

                yield return wfs;
            }
        }
    }
}
