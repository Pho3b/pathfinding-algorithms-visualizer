using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public static GridComponent instance { get; private set; }
    public Tile tilePrefab;
    public Tile[,] tilesMatrix;

    [SerializeField] private Camera _camera;
    [SerializeField] private byte width, height;
    [SerializeField] private GraphComponent graphComponent;
    private Tile startingTile, endingTile;


    private void Awake()
    {
        instance = this;
        _camera = _camera ?? Camera.main;
        _camera.transform.position += new Vector3(width / 2, (height / 2) - 1, -10);
        _camera.orthographicSize = 8;
        tilesMatrix = new Tile[width, height];
        Create();
    }

    /// <summary>
    /// Resets the UI grid and the underlying 'tilesMatrix' data structure
    /// </summary>
    public void Reset()
    {
        StartingTile = null;
        StartingTile = null;

        Create();
        graphComponent.Matrix = tilesMatrix;
    }

    public void StartDFS()
    {
        if (startingTile != null)
            StartCoroutine(graphComponent.DepthFirstSearch(startingTile, endingTile));
        else
            print("NULL starting tile");
    }

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
    /// Generates a fresh new UI grid and populates the instance attribute 'tilesMatrix' with its newly
    /// instantiated Tiles.
    /// </summary>
    private void Create()
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

                tilesMatrix[x, y] = spawnedTile;
                id++;
            }

            id++;
        }
    }
}
