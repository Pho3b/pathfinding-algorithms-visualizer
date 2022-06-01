using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public static GridComponent instance { get; private set; }
    public Tile tilePrefab;

    [SerializeField] private GraphComponent graphComponent;
    private Tile startingTile, endingTile;
    private byte width, height;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        width = graphComponent.width;
        height = graphComponent.height;
        instance = this;
    }

    /// <summary>
    /// Default Unity Start
    /// </summary>
    private void Start()
    {
        GenerateGrid();
    }

    /// <summary>
    /// Calls the proper method on the 'graphComponent' to start the given 'Enums.Algorithm'
    /// </summary>
    public void RunAlgorithm(Enums.Algorithm algorithm)
    {
        if (startingTile != null)
        {
            graphComponent.RunAlgorithm(algorithm, startingTile, endingTile);
        }
        else
            Debug.Log("NULL starting tile");
    }

    /// <summary>
    /// Resets the UI grid and triggers the method to also rest the underlying 'matrix' data structure
    /// </summary>
    public void ResetGrid()
    {
        StartingTile = null;
        EndingTile = null;

        graphComponent.Reset();
    }

    /// <summary>
    /// Getter/Setter for the 'startingTile' attribute.
    /// Since this tile must remain unique on the grid, when setting it, the previous 'statingTile' UI 
    /// will be reset to the 'Base' state if not NULL before being assigned to the new 'startingTile' value
    /// </summary>
    public Tile StartingTile
    {
        get { return startingTile; }
        set
        {
            if (startingTile != null)
                startingTile.SetState(Enums.TileState.Base);

            startingTile = value;
        }
    }

    /// <summary>
    /// Getter/Setter for the 'endingTile' attribute.
    /// Since this tile must remain unique on the grid, when setting it, the previous 'endingTile' UI 
    /// will be reset to the 'Base' state if not NULL before being assigned to the new 'endingTile' value
    /// </summary>
    public Tile EndingTile
    {
        get { return endingTile; }
        set
        {
            if (endingTile != null)
                endingTile.SetState(Enums.TileState.Base);

            endingTile = value;
        }
    }

    /// <summary>
    /// Sets the weight of all the current Grid's tiles to a random number between 0 - 100 
    /// </summary>
    public void AddRandomWeights()
    {
        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                GraphComponent.matrix[x, y].Weight = Random.Range(0, 10);
            }
        }
    }

    /// <summary>
    /// Sets the weight of all the current Grid's tiles to zero
    /// </summary>
    public void RemoveWeights()
    {
        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                GraphComponent.matrix[x, y].Weight = 0;
            }
        }
    }

    /// <summary>
    /// Generates a fresh new UI grid and populates the graphComponent's attribute 'matrix' with the newly
    /// instantiated Tiles.
    /// </summary>
    private void GenerateGrid()
    {
        int id = 0;

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                Vector3 worldPosition = new Vector3(x, y, 0);
                Tile spawnedTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity);

                spawnedTile.name = $"tile: {id}";
                spawnedTile.transform.SetParent(transform);
                spawnedTile.SetState(Enums.TileState.Base);
                spawnedTile.x = x;
                spawnedTile.y = y;
                spawnedTile.id = id;

                GraphComponent.matrix[x, y] = spawnedTile;
                id++;
            }
        }
    }
}
