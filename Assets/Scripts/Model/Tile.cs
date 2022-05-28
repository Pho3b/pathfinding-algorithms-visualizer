using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Tile : MonoBehaviour
{
    public bool visited, isObstacle = false;
    public int x, y, id;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private TextMeshPro weightText;
    private int weight = 0;
    private Constant constant;
    private GridComponent gridComponent;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        weightText = GetComponentInChildren<TextMeshPro>();
        gridComponent = GridComponent.instance;
        constant = new Constant();
    }

    /// <summary>
    /// Triggered when the mouse is positioned over this gameObject collider
    /// </summary>
    private void OnMouseOver()
    {
        // Early return if any algorithm is currently running
        if (GraphComponent.isAlgorithmRunning)
        {
            return;
        }

        // Tile INPUT actions list
        if (Input.GetMouseButton((int)MouseButton.LeftMouse) && Input.GetKey(KeyCode.LeftControl))
        {
            ResetTile();
        }
        else if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            SetStartingTile();
        }

        if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            SetEndingTile();
        }

        if (Input.GetMouseButton((int)MouseButton.MiddleMouse))
        {
            SetObstacleTile();
        }
    }

    /// <summary>
    /// Updates the current Tile with the given Tile 'state', updates the 'visited' attribute if the given state requires it
    /// </summary>
    /// <param name="state">The state that the Tile will be set to</param>
    public void SetState(Enums.TileState state)
    {
        spriteRenderer.color = constant.colorsDictionary[state];
        visited = state == Enums.TileState.Visited || state == Enums.TileState.ToVisit;
    }

    /// <summary>
    /// Getter and Setter for the 'matrix' attribute
    /// </summary>
    public int Weight
    {
        get { return weight; }
        set
        {
            weight = value;
            weightText.text = value.ToString();
        }
    }

    /// <summary>
    /// Sets the current Tile to be the STARTING one, or BASE if it already is the STARTING tile
    /// it also updates the 'startingTile' attribute in the 'gridComponent' and  the color accordingly.
    /// </summary>
    private void SetStartingTile()
    {
        spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[Enums.TileState.Found]
            ? constant.colorsDictionary[Enums.TileState.Found]
            : constant.colorsDictionary[Enums.TileState.Base];

        gridComponent.StartingTile = (gridComponent.StartingTile != null && gridComponent.StartingTile.id == id)
            ? null
            : this;
    }

    /// <summary>
    /// Sets the current Tile to be the ENDING one, or BASE if it already is the ENDING tile
    /// it also updates the 'endingTile' attribute in the 'gridComponent' and the color accordingly.
    /// </summary>
    private void SetEndingTile()
    {
        spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[Enums.TileState.ToSearch]
            ? constant.colorsDictionary[Enums.TileState.ToSearch]
            : constant.colorsDictionary[Enums.TileState.Base];

        gridComponent.EndingTile = (gridComponent.EndingTile != null && gridComponent.EndingTile.id == id)
            ? null
            : this;
    }

    /// <summary>
    /// Sets the current tile to be a blocking one and updates its color accordingly
    /// </summary>
    private void SetObstacleTile()
    {
        spriteRenderer.color = constant.colorsDictionary[Enums.TileState.Obstacle];
        isObstacle = true;
    }

    /// <summary>
    /// Resets the current tile attributes as they were when it was first instatiated.
    /// It also updates the 'gridComponent' 'startingTile' and 'endingTile' tile to be null it any of them happens
    /// to be assigned to this Tile isntance.
    /// </summary>
    private void ResetTile()
    {
        spriteRenderer.color = constant.colorsDictionary[Enums.TileState.Base];
        isObstacle = false;

        if (gridComponent.StartingTile != null && gridComponent.StartingTile.id == id)
        {
            gridComponent.StartingTile = null;
        }

        if (gridComponent.EndingTile != null && gridComponent.EndingTile.id == id)
        {
            gridComponent.EndingTile = null;
        }
    }
}
