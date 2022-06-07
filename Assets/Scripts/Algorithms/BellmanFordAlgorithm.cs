using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    class BellmanFordAlgorithm : Algorithm
    {
        private readonly WaitForSeconds bellmanFordWfs = new WaitForSeconds(0.000001f);
        private bool areAllEdgesRelaxed, negativeCycleExist = false;


        public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
        {
            Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
            int[] dist = new int[matrix.Length];
            FillArray(dist, int.MaxValue);
            dist[from.id] = 0;
            int v = 1;


            // Relaxing all edges
            while (v < matrix.Length && !areAllEdgesRelaxed)
            {
                areAllEdgesRelaxed = true;

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

            // Checking for negative cycles
            for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        StartCoroutine(ParseAdjacentTiles(matrix[i, j], parent, dist, true));

                        if (negativeCycleExist) break;
                    }

                    if (negativeCycleExist) break;
                }

            yield return bellmanFordWfs;

            if (!negativeCycleExist)
            {
                StartCoroutine(HighlightShortestPath(parent, to));
            }
            else
                print("Error: Negative Cycle Exists");

            GraphComponent.isAlgorithmRunning = false;
        }

        /// <summary>
        /// Cycles over the 4 possibile grid directions
        /// </summary>
        /// <param name="from">The starting tile(vertex)</param>
        /// <param name="parent">The dictionary holding the parent reference for every tile, useful to reconstruct the shortest path</param>
        /// <param name="dist">The array holding the shortest distance for every node in the graph</param>
        /// <returns>The current instance 'wfs' coroutine attribute when a tile is added to the queue</returns>
        private IEnumerator<WaitForSeconds> ParseAdjacentTiles(
            Tile from,
            Dictionary<int, Tile> parent,
            int[] dist,
            bool checkForNegativeCycles = false
        )
        {
            for (byte i = 0; i < Constant.DirectionsNumber; i++)
            {
                Tile tile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);
                if (tile == null) 
                    continue;

                int newDist = dist[from.id] + tile.Weight;

                if (newDist < dist[tile.id])
                {
                    if (!checkForNegativeCycles)
                    {
                        areAllEdgesRelaxed = false;
                        dist[tile.id] = newDist;
                        // StartCoroutine(tile.Highlight(0.1f));

                        parent[tile.id] = from;

                        yield return bellmanFordWfs;
                    }
                    else
                    {
                        negativeCycleExist = true;
                        break;
                    }
                }
            }
        }
    }
}
