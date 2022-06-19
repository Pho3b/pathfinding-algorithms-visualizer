using System.Collections.Generic;
using Assets.Scripts.Algorithms.DS;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Algorithms
{
    /// <summary>
    /// Performs The A* Algorithm on the current matrix(graph)
    /// If the 'to' vertex is given, the search will stop as soon as it will find it
    /// </summary>
    /// <param name="from">Starting vertex of the search</param>
    /// <param name="to"> Optional ending vertex of the search</param>
    /// <returns></returns>
    class AStarAlgorithm : Algorithm
    {
        public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
        {
            Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
            IndexedPriorityQueue<TileWeight> iPriorityQueue = new IndexedPriorityQueue<TileWeight>(matrix.Length);
            iPriorityQueue.Insert(from.id, new TileWeight(from, 0));

            while (iPriorityQueue.Count > 0 && !GraphComponent.found)
            {
                TileWeight currentTile = iPriorityQueue.Pop();
                currentTile.Tile.SetState(Enums.TileState.Visited);

                yield return wfs;

                StartCoroutine(AddAdjacentTiles(currentTile, to, iPriorityQueue, parent));
            }

            if (GraphComponent.found)
                StartCoroutine(HighlightShortestPath(parent, to));

            GraphComponent.isAlgorithmRunning = false;
        }

        /// <summary>
        /// Cycles over the 4 possibile grid directions and adds the valid tiles to the indexed priority queue to visit them in the correct order.
        /// </summary>
        /// <param name="tileWeight">The starting tile(vertex)</param>
        /// <param name="iPriorityQueue">The indexed priority queue where the Tiles that needs to be visited are stored</param>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <returns>The current instance 'wfs' coroutine attribute when a tile is added to the queue</returns>
        private IEnumerator<WaitForSeconds> AddAdjacentTiles(
            TileWeight tileWeight,
            Tile to,
            IndexedPriorityQueue<TileWeight> iPriorityQueue,
            Dictionary<int, Tile> parent
        )
        {
            for (byte i = 0; i < Constant.DirectionsNumber; i++)
            {
                Tile tile = RetrieveAdjacentTile(tileWeight.Tile.x + rd[i], tileWeight.Tile.y + cd[i]);

                if (tile == null)
                    continue;

                if (to != null && (tile.id == to.id || to.id == tileWeight.Tile.id))
                {
                    to.SetState(Enums.TileState.Found);
                    GraphComponent.found = true;

                    if (!parent.ContainsKey(tile.id))
                        parent.Add(tile.id, tileWeight.Tile);
                }
                else
                {
                    int manhattanDistance = Mathf.Abs(tile.x - to.x) + Mathf.Abs(tile.y - to.y);
                    int heuristicWeight = tile.Weight + manhattanDistance;
                    tile.SetState(Enums.TileState.ToVisit);

                    if (iPriorityQueue[tile.id] == null)
                    {
                        iPriorityQueue.Insert(tile.id, new TileWeight(tile, heuristicWeight));
                    }
                    else
                    {
                        iPriorityQueue.DecreaseIndex(tile.id, new TileWeight(tile, heuristicWeight));
                    }

                    if (!parent.ContainsKey(tile.id))
                        parent.Add(tile.id, tileWeight.Tile);

                    yield return wfs;
                }
            }
        }
    }
}
