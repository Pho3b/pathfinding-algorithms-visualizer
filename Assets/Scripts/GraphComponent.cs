using Assets.Scripts.Algorithms;
using UnityEngine;

public class GraphComponent : MonoBehaviour
{
    public static bool found, isAlgorithmRunning = false;
    public static bool isGraphReady = true;
    public static Tile[,] matrix;
    public byte width, height;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        matrix = new Tile[width, height];
    }

    /// <summary>
    /// Instantiate the concrete class to run the given algorithm if found,
    /// it also handles the state of some class static attributes to indicate that and algorithm is running.
    /// </summary>
    /// <param name="algorithm">The algorithm to run</param>
    /// <param name="from">Starting tile</param>
    /// <param name="to">Optional Ending tile</param>
    public void RunAlgorithm(Enums.Algorithm algorithm, Tile from, Tile to = null)
    {
        isAlgorithmRunning = true;
        isGraphReady = false;
        Algorithm currentAlgorithm = null;

        switch (algorithm)
        {
            case Enums.Algorithm.DepthFirstSearch:
                currentAlgorithm = gameObject.GetComponent<DepthFirstSearch>();

                if (currentAlgorithm == null)
                    currentAlgorithm = gameObject.AddComponent<DepthFirstSearch>();
                break;
            case Enums.Algorithm.BreadthFirstSearch:
                currentAlgorithm = gameObject.GetComponent<BreadthFirstSearch>();

                if (currentAlgorithm == null)
                    currentAlgorithm = gameObject.AddComponent<BreadthFirstSearch>();
                break;
            case Enums.Algorithm.DjikstraAlgorithm:
                currentAlgorithm = gameObject.GetComponent<DjikstraAlgorithm>();

                if (currentAlgorithm == null)
                    currentAlgorithm = gameObject.AddComponent<DjikstraAlgorithm>();
                break;
            case Enums.Algorithm.AStarAlgorithm:
                currentAlgorithm = gameObject.GetComponent<AStarAlgorithm>();

                if (currentAlgorithm == null)
                    currentAlgorithm = gameObject.AddComponent<AStarAlgorithm>();
                break;
            case Enums.Algorithm.BellmanFordAlgorithm:
                currentAlgorithm = gameObject.GetComponent<BellmanFordAlgorithm>();

                if (currentAlgorithm == null)
                    currentAlgorithm = gameObject.AddComponent<BellmanFordAlgorithm>();
                break;
            default:
                Debug.Log("The given Algorithm was not found");
                break;
        }

        StartCoroutine(currentAlgorithm.Run(from, to));
    }


    /// <summary>
    /// Resets the current graphComponent to be the same as it was when it was first instantiated
    /// </summary>
    public void Reset()
    {
        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                Tile t = matrix[x, y];
                t.SetState(Enums.TileState.Base);
                t.isObstacle = false;
            }
        }

        isGraphReady = true;
        found = false;
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
