using UnityEngine;
using UnityEngine.UIElements;

public partial class Tile : MonoBehaviour
{
    public bool visited, isObstacle = false;
    public int x, y, id;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Constant constant;
    private GridComponent gridComponent;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        gridComponent = GridComponent.instance;
        constant = new Constant();
    }

    /// <summary>
    /// Triggered when the mouse is positioned over this gameObject collider
    /// </summary>
    private void OnMouseOver()
    {
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
    /// TODO: update it to be just SetColor
    /// </summary>
    /// <param name="state">The state that the Tile will be set to</param>
    public void SetState(TileState state)
    {
        spriteRenderer.color = constant.colorsDictionary[state];

        if (state == TileState.Visited || state == TileState.ToVisit)
            visited = true;
        else
            visited = false;
    }

    /// <summary>
    /// It updates the 'startingTile' attribute in the 'gridComponent' and also updates the color accordingly.
    /// Sets the current Tile to be the STARTING one, or BASE if it already is the STARTING tile
    /// </summary>
    private void SetStartingTile()
    {
        spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[TileState.Found]
            ? constant.colorsDictionary[TileState.Found]
            : constant.colorsDictionary[TileState.Base];

        gridComponent.StartingTile = (gridComponent.StartingTile != null && gridComponent.StartingTile.id == id)
            ? null
            : this;
    }

    /// <summary>
    /// It updates the 'endingTile' attribute in the 'gridComponent' and also updates the color accordingly.
    /// Sets the current Tile to be the ENDING one, or BASE if it already is the ENDING tile
    /// </summary>
    private void SetEndingTile()
    {
        spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[TileState.ToSearch]
            ? constant.colorsDictionary[TileState.ToSearch]
            : constant.colorsDictionary[TileState.Base];

        gridComponent.EndingTile = (gridComponent.EndingTile != null && gridComponent.EndingTile.id == id)
            ? null
            : this;
    }

    private void SetObstacleTile()
    {
        spriteRenderer.color = constant.colorsDictionary[TileState.Obstacle];
        isObstacle = true;
    }

    private void ResetTile()
    {
        spriteRenderer.color = constant.colorsDictionary[TileState.Base];
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