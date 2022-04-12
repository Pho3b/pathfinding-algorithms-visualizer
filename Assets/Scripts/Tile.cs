using UnityEngine;

public partial class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color currentColor;
    private Constant constant;
    public bool visited = false;
    public int x, y, id;


    private void Awake()
    {
        constant = new Constant();
    }

    public void InitColor(bool isOffset)
    {
        currentColor = isOffset ? constant.colorsDictionary[TileColor.Offset] : constant.colorsDictionary[TileColor.Base];
        spriteRenderer.color = currentColor;
    }

    public void Visited()
    {
        spriteRenderer.color = constant.colorsDictionary[TileColor.Visited];
        visited = true;
    }

    public void ToVisit()
    {
        visited = true;
        spriteRenderer.color = constant.colorsDictionary[TileColor.ToVisit];
    }

    public void Found()
    {
        spriteRenderer.color = constant.colorsDictionary[TileColor.Found];
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = constant.colorsDictionary[TileColor.Visited];
        Debug.Log(name);
    }

    private void OnMouseUp()
    {
        spriteRenderer.color = currentColor;
    }
}
