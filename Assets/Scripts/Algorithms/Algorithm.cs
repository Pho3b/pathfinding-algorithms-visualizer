using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    abstract class Algorithm : MonoBehaviour
    {
        protected Tile[,] matrix;
        protected readonly WaitForSeconds wfs = new WaitForSeconds(0.04f);
        protected readonly short[] rd = new short[4] { -1, +1, 0, 0 };
        protected readonly short[] cd = new short[4] { 0, 0, -1, +1 };


        private void Awake()
        {
            matrix = GraphComponent.matrix;
        }

        /// <summary>
        /// Returns a tile if it is a valid one, if it's not, NULL is returned instead.
        /// </summary>
        /// <param name="x">The X axis of the Tile that tries to retrieve</param>
        /// <param name="y">The Y axis of the Tile that tries to retrieve</param>
        /// <returns>The requested tile or NULL in case it not a valid tile</returns>
        /// <note>A tile is 'invalid' when it is out of the matrix bounds or it has been manually set as 'invalid'</note>
        protected Tile RetrieveAdjacentTile(int x, int y)
        {
            if (
                x < 0
                || x > matrix.GetLength(0) - 1
                || y < 0
                || y > matrix.GetLength(1) - 1
                || matrix[x, y].visited
                || matrix[x, y].isObstacle
            )
            {
                return null;
            }

            return matrix[x, y];
        }

        abstract public IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null);
    }
}
