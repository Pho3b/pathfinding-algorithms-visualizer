using UnityEngine;
using UnityEngine.UI;

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
    /// <param name="uiToggles">List of toggles that will be set to 'false', null by default</param>
    /// </summary>
    public void ResetGrid(Toggle[] uiToggles = null)
    {
        StartingTile = null;
        EndingTile = null;

        foreach (Toggle uiToggle in uiToggles ?? new Toggle[0])
        {
            uiToggle.isOn = false;
        }

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
    /// Sets the weight of all the current Grid's tiles to a random number
    /// Does not include negative numbers.
    /// </summary>
    /// <param name="includeNegativeNumbers">If true the numbers range will be between [-5 to 100] otherwise [0 to 100]</param>
    public void AddRandomWeights(bool includeNegativeNumbers = false)
    {
        int min = includeNegativeNumbers ? -5 : 0;

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                GraphComponent.matrix[x, y].Weight = Random.Range(min, 100);
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
        float xPos = -1;
        float yPos = -1;

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                Tile t = Instantiate(tilePrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                t.name = $"tile: {id}";
                t.transform.SetParent(transform);
                t.SetState(Enums.TileState.Base);
                t.x = x;
                t.y = y;
                t.id = id;

                id++;
                yPos += Constant.GridTilesOffset;
                GraphComponent.matrix[t.x, t.y] = t;
            }

            yPos = -1;
            xPos += Constant.GridTilesOffset;
        }
    }
}
