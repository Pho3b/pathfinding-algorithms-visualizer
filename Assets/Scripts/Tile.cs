using UnityEngine;
using UnityEngine.UIElements;

public partial class Tile : MonoBehaviour
{
    public bool visited = false;
    public int x, y, id;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Constant constant;
    private GridComponent gridComponent;


    /// <summary>
    /// Unity Awake Method
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
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[TileState.Found]
                ? constant.colorsDictionary[TileState.Found]
                : constant.colorsDictionary[TileState.Base];

            gridComponent.StartingTile = (gridComponent.StartingTile != null && gridComponent.StartingTile.id == id)
                ? null 
                : this;
        }

        if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[TileState.ToSearch]
                ? constant.colorsDictionary[TileState.ToSearch]
                : constant.colorsDictionary[TileState.Base];

            gridComponent.EndingTile = (gridComponent.EndingTile != null && gridComponent.EndingTile.id == id)
                ? null
                : this;
        }
    }

    /// <summary>
    /// Updates the current Tile with the given Tile 'state', updates the 'visited' attribute if the given state requires it
    /// </summary>
    /// <param name="state">The state that the Tile needs to be set to</param>
    public void SetState(TileState state)
    {
        spriteRenderer.color = constant.colorsDictionary[state];

        if (state == TileState.Visited || state == TileState.ToVisit)
            visited = true;
    }
}
