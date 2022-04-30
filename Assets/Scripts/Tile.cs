using UnityEngine;
using UnityEngine.UIElements;

public partial class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color initialColor;
    private Constant constant;
    public bool visited = false;
    public int x, y, id;


    /// <summary>
    /// Unity Awake Method
    /// </summary>
    private void Awake()
    {
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
                : initialColor;
        }

        if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            spriteRenderer.color = spriteRenderer.color != constant.colorsDictionary[TileState.ToSearch]
                ? constant.colorsDictionary[TileState.ToSearch]
                : initialColor;
        }
    }

    /// <summary>
    /// Inizialize the 'initialColor' attribute deciding the color based on the given 'isOffset' boolean
    /// </summary>
    /// <param name="isOffset">Boolean that decides whethe the Tile color will be the 'base' or the 'offset' one</param>
    public void InitColor(bool isOffset)
    {
        initialColor = isOffset ? constant.colorsDictionary[TileState.Offset] : constant.colorsDictionary[TileState.Base];
        spriteRenderer.color = initialColor;
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
