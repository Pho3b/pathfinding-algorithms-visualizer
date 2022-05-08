using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public static GridComponent instance { get; private set; }
    public Tile tilePrefab;

    [SerializeField] private Camera _camera;
    [SerializeField] private byte width, height;
    [SerializeField] private GraphComponent graphComponent;
    private Tile startingTile, endingTile;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        instance = this;

        // setting up camera position
        _camera = _camera ?? Camera.main;
        _camera.transform.position += new Vector3(graphComponent.width / 2, (graphComponent.height / 2) - 1, -10);
        _camera.orthographicSize = 8;
    }

    /// <summary>
    /// Default Unity Start
    /// </summary>
    private void Start()
    {
        GenerateGrid();
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
    /// TODO: update it
    /// </summary>
    public void StartDFS()
    {
        if (startingTile != null)
            StartCoroutine(graphComponent.DepthFirstSearch(startingTile, endingTile));
        else
            Debug.Log("NULL starting tile");
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
                startingTile.SetState(Tile.TileState.Base);

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
                endingTile.SetState(Tile.TileState.Base);

            endingTile = value;
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

                spawnedTile.name = $"Tile {x} : {y}";
                spawnedTile.transform.SetParent(transform);
                spawnedTile.SetState(Tile.TileState.Base);
                spawnedTile.x = x;
                spawnedTile.y = y;
                spawnedTile.id = id;

                graphComponent.matrix[x, y] = spawnedTile;
                id++;
            }

            id++;
        }
    }
}
