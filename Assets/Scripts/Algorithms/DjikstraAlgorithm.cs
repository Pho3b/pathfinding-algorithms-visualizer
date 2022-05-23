using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Algorithms.DS;

namespace Assets.Scripts.Algorithms
{
    class DjikstraAlgorithm : Algorithm
    {
        public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
        {
            int[] dist = new int[matrix.Length];
            FillArray(dist, int.MaxValue);
            dist[from.id] = 0;

            Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
            IndexedPriorityQueue<Tile> iPriorityQueue = new IndexedPriorityQueue<Tile>(matrix.Length);
            iPriorityQueue.Insert(from.id, from);


            // while (stack.Count > 0 && !GraphComponent.found)
            while (iPriorityQueue.Count > 0)
            {
                Tile tile = iPriorityQueue.Pop();
                tile.SetState(Enums.TileState.Visited);

                yield return wfs;

                StartCoroutine(AddAdjacentTiles(tile, to, iPriorityQueue, parent, dist));
            }

            GraphComponent.isAlgorithmRunning = false;
        }

        /// <summary>
        /// Cycles over the 4 possibile grid directions and adds the valid tiles to the queue to visit them in-order.
        /// </summary>
        /// <param name="currentTile">The starting tile(vertex)</param>
        /// <param name="iPriorityQueue">The indexed priority queue where the Tiles that needs to be visited are stored</param>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <returns>The current instance 'wfs' attribute when a tile is added to the queue</returns>
        private IEnumerator<WaitForSeconds> AddAdjacentTiles(
            Tile currentTile,
            Tile to,
            IndexedPriorityQueue<Tile> iPriorityQueue,
            Dictionary<int, Tile> parent,
            int[] dist
        )
        {
            for (byte i = 0; i < Constant.DirectionsNumber; i++)
            {
                Tile tile = RetrieveAdjacentTile(currentTile.x + rd[i], currentTile.y + cd[i]);
                int newDist = dist[currentTile.Weight] + tile.Weight;

                if (to != null && tile != null && (tile.id == to.id || to.id == currentTile.id))
                {
                    to.SetState(Enums.TileState.Found);
                    GraphComponent.found = true;

                    if (!parent.ContainsKey(tile.id))
                        parent.Add(tile.id, currentTile);
                    break;
                }
                else if (tile != null && newDist < dist[tile.id])
                {
                    tile.SetState(Enums.TileState.ToVisit);

                    if (iPriorityQueue[tile.id] != null)
                    {
                        iPriorityQueue.Insert(tile.id, newDist);
                    }
                   

                    if (!parent.ContainsKey(tile.id))
                        parent.Add(tile.id, currentTile);

                    yield return wfs;
                }
            }
        }

        /// <summary>
        /// Fills the given array with the given 'fillValue'
        /// </summary>
        /// <param name="toFill">The array to fill</param>
        /// <param name="fillValue">The value that will fill the array</param>
        protected void FillArray(int[] toFill, int fillValue)
        {
            for (int i = 0; i < toFill.Length; i++)
            {
                toFill[i] = fillValue;
            }
        }
    }
}
