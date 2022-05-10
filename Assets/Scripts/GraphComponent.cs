using Assets.Scripts.Algorithms;
using UnityEngine;

public class GraphComponent : MonoBehaviour
{
    public static bool found, isAlgorithmRunning = false;
    public static Tile[,] matrix;
    public byte width, height;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        matrix = new Tile[width, height];
    }

    public void RunAlgorithm(Enums.Algorithm algorithm, Tile from, Tile to = null)
    {
        Algorithm currentAlgorithm = null;
        isAlgorithmRunning = true;

        switch (algorithm)
        {
            case Enums.Algorithm.DepthFirstSearch:
                currentAlgorithm = gameObject.GetComponent<DepthFirstSearch>();

                if (currentAlgorithm == null)
                    currentAlgorithm = gameObject.AddComponent<DepthFirstSearch>();
                break;
        }

        StartCoroutine(currentAlgorithm.Run(from, to));
        isAlgorithmRunning = false;
    }


    /// <summary>
    /// Resets the current graphComponent to be the same as it was when it was first instantiated
    /// </summary>
    public void Reset()
    {
        found = false;

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                Tile t = matrix[x, y];
                t.SetState(Enums.TileState.Base);
                t.isObstacle = false;
            }
        }
    }

    /// <summary>
    /// Getter and Setter for the 'matrix' attribute
    /// </summary>
    public Tile[,] Matrix
    {
        private get { return matrix; }
        set { matrix = value; }
    }
}
