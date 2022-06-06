using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    class BellmanFordAlgorithm : Algorithm
    {
        private readonly WaitForSeconds bellmanFordWfs = new WaitForSeconds(0.00001f);
        private bool areAllEdgesRelaxed = false;


        public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
        {
            Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
            int[] dist = new int[matrix.Length];
            FillArray(dist, int.MaxValue);
            dist[from.id] = 0;
            int v = 1;

            while (v < matrix.Length && !areAllEdgesRelaxed)
            {
                areAllEdgesRelaxed = true;

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        yield return bellmanFordWfs;

                        StartCoroutine(AddAdjacentTiles(matrix[i, j], to, parent, dist));
                    }
                }
            }

            StartCoroutine(HighlightShortestPath(parent, to));
            GraphComponent.isAlgorithmRunning = false;
        }

        /// <summary>
        /// Cycles over the 4 possibile grid directions and adds the valid tiles to the indexed priority queue to visit them in the correct order.
        /// </summary>
        /// <param name="from">The starting tile(vertex)</param>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance for every node in the graph</param>
        /// <returns>The current instance 'wfs' coroutine attribute when a tile is added to the queue</returns>
        private IEnumerator<WaitForSeconds> AddAdjacentTiles(
            Tile from,
            Tile to,
            Dictionary<int, Tile> parent,
            int[] dist
        )
        {
            for (byte i = 0; i < Constant.DirectionsNumber; i++)
            {
                Tile tile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);

                if (tile == null)
                    continue;

                int newDist = from.Weight + dist[from.id];

                if (to != null && tile != null && (tile.id == to.id || to.id == from.id))
                {
                    if (!parent.ContainsKey(tile.id))
                        parent.Add(tile.id, from);
                }
                else if (newDist < dist[tile.id])
                {
                    areAllEdgesRelaxed = false;
                    dist[tile.id] = newDist;
                    tile.SetRandomColor();

                    if (!parent.ContainsKey(tile.id))
                        parent.Add(tile.id, tile);
                    else
                        parent[tile.id] = tile;

                    yield return wfs;
                }
            }
        }
    }
}
