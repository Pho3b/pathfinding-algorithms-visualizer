using Assets.Scripts.Algorithms;
using System.Collections.Generic;
using UnityEngine;

class BreadthFirstSearch : Algorithm
{
    /// <summary>
    /// Performs a BreadthFirstSearch on the current matrix(graph)
    /// If the 'to' vertex is given, the search will stop as soon as it will find it
    /// </summary>
    /// <param name="from">Starting vertex of the search</param>
    /// <param name="to"> Optional ending vertex of the search</param>
    /// <returns></returns>
    public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
    {
        Queue<Tile> queue = new Queue<Tile>();
        Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
        queue.Enqueue(from);

        while (queue.Count > 0 && !GraphComponent.found)
        {
            Tile tile = queue.Dequeue();
            tile.SetState(Enums.TileState.Visited);

            yield return wfs;

            StartCoroutine(AddAdjacentTiles(tile, to, queue, parent));
        }

        if (GraphComponent.found)
            StartCoroutine(HighlightShortestPath(parent, to));

        GraphComponent.isAlgorithmRunning = false;
    }


    /// <summary>
    /// Cycles over the 4 possibile grid directions and adds the valid tiles to the queue to visit them in-order.
    /// </summary>
    /// <param name="from">The starting tile(vertex)</param>
    /// <param name="queue">The queue where the Tiles that needs to be visited are stored</param>
    /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
    /// <returns>The current instance 'wfs' attribute when a tile is added to the queue</returns>
    private IEnumerator<WaitForSeconds> AddAdjacentTiles(Tile from, Tile to, Queue<Tile> queue, Dictionary<int, Tile> parent)
    {
        for (byte i = 0; i < Constant.DirectionsNum; i++)
        {
            Tile tile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);

            if (to != null && tile != null && (tile.id == to.id || to.id == from.id))
            {
                to.SetState(Enums.TileState.Found);
                GraphComponent.found = true;
                parent.Add(tile.id, from);
                break;
            }
            else if (tile != null)
            {
                tile.SetState(Enums.TileState.ToVisit);
                queue.Enqueue(tile);
                parent.Add(tile.id, from);

                yield return wfs;
            }
        }
    }

    /// <summary>
    /// Follows the tiles in reverse order to reconstruct and highlight the shortest path from the Ending tile to the Starting one
    /// </summary>
    /// <param name="parent">The dictionary holding the parent reference for every tile</param>
    /// <param name="to">Optional ending vertex of the search</param>
    /// <returns>The current instance 'wfs' attribute when a tile is highlighted</returns>
    private IEnumerator<WaitForSeconds> HighlightShortestPath(Dictionary<int, Tile> parent, Tile to)
    {
        Tile t = parent[to.id];
        t.SetState(Enums.TileState.Found);

        while (parent.TryGetValue(t.id, out t))
        {
            yield return wfs;
            t.SetState(Enums.TileState.Found);
        }
    }
}
