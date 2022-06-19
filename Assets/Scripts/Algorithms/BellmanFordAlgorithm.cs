using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    class BellmanFordAlgorithm : Algorithm
    {
        /// <summary>
        /// Performs The Bellman Ford Algorithm on the current matrix(graph)
        /// </summary>
        /// <param name="from">Starting vertex of the search</param>
        /// <param name="to"> Optional ending vertex of the search</param>
        /// <returns></returns>
        public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
        {
            Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
            int[] dist = new int[matrix.Length];
            FillArray(dist, int.MaxValue);
            dist[from.id] = 0;

            RelaxEdges(parent, dist);

            if (!DoNegativeCyclesExist(parent, dist))
            {
                if (to != null)
                    StartCoroutine(HighlightShortestPath(parent, to));
            }
            else
            {
                print("A Negative cycle has been found, it's not possible to compute the shortest path");
            }

            GraphComponent.isAlgorithmRunning = false;
            yield return null;
        }

        /// <summary>
        /// Cycles over the 4 possibile grid directions
        /// </summary>
        /// <param name="from">The tile that is currently being parsed(vertex)</param>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance from the starting node to every other nodes in the graph</param>
        private void ParseAdjacentTiles(Tile from, Dictionary<int, Tile> parent, int[] dist)
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
                }
            }
        }

        /// <summary>
        /// Executes one iteration of the the Bellman ford algorithm in order to detect the existance of negative cycles
        /// </summary>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance for every node in the graph</param>
        /// <returns>Whether the current graph contains negative cycles or not</returns>
        private bool DoNegativeCyclesExist(Dictionary<int, Tile> parent, int[] dist)
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

        /// <summary>
        /// Relaxes all edges V - 1 times where V is the number of vertices in the Graph
        /// </summary>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance for every node in the graph</param>
        private void RelaxEdges(Dictionary<int, Tile> parent, int[] dist)
        {
            int v = 1;

            while (v < matrix.Length)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Tile t = matrix[i, j];

                        if (!t.isObstacle)
                            ParseAdjacentTiles(t, parent, dist);
                    }
                }

                v++;
            }
        }
    }
}
