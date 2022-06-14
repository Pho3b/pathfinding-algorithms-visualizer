using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    class BellmanFordAlgorithm : Algorithm
    {
        public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
        {
            Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
            int[] dist = new int[matrix.Length];
            FillArray(dist, int.MaxValue);
            dist[from.id] = 0;
            int v = 1;

            // Relaxing all edges (V - 1) times
            while (v < matrix.Length)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Tile t = matrix[i, j];

                        if (!t.isObstacle)
                            StartCoroutine(ParseAdjacentTiles(t, parent, dist));
                    }
                }

                v++;
            }

            if (CheckForNegativeCycles(parent, dist))
            {
                print("Negative Cycle Exists");
            }
            else
            {
                StartCoroutine(HighlightShortestPath(parent, to));
            }

            GraphComponent.isAlgorithmRunning = false;
            yield return null;
        }

        /// <summary>
        /// Cycles over the 4 possibile grid directions
        /// </summary>
        /// <param name="from">The tile that is currently being parsed(vertex)</param>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance for every node in the graph</param>
        /// <returns>The current instance 'wfs' coroutine attribute when a tile is added to the queue</returns>
        private IEnumerator<WaitForSeconds> ParseAdjacentTiles(Tile from, Dictionary<int, Tile> parent, int[] dist)
        {
            for (byte i = 0; i < Constant.DirectionsNumber; i++)
            {
                Tile tile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);
                if (tile == null)
                    continue;

                int newDist = CalculateNewDistance(dist[from.id], tile.Weight);

                if (newDist < dist[tile.id])
                {
                    dist[tile.id] = newDist;
                    parent[tile.id] = from;

                    yield return null;
                }
            }
        }

        /// <summary>
        /// Executes the Bellman ford algorithm one time in order to detect the existance of negative cycles
        /// </summary>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance for every node in the graph</param>
        /// <returns>Whether the current graph contains negative cycles or not</returns>
        private bool CheckForNegativeCycles(Dictionary<int, Tile> parent, int[] dist)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Tile t = matrix[i, j];

                    if (t.isObstacle)
                        continue;

                    for (byte z = 0; z < Constant.DirectionsNumber; z++)
                    {
                        Tile tile = RetrieveAdjacentTile(t.x + rd[z], t.y + cd[z]);
                        if (tile == null)
                            continue;

                        if (CalculateNewDistance(dist[t.id], tile.Weight) < dist[tile.id])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates the distance from one tile to another checking for INT overflows
        /// </summary>
        /// <param name="currentTileDist">The distance/weight that has been previously calculated for the current tile</param>
        /// <param name="nextTileWeight">The cost needed to arrive to the given tile</param>
        /// <returns>The new distance value</returns>
        private int CalculateNewDistance(int currentTileDist, int nextTileWeight)
        {
            try
            {
                return checked(currentTileDist + nextTileWeight);
            }
            catch (System.OverflowException)
            {
                return int.MaxValue;
            }
        }
    }
}
