using UnityEngine;

public partial class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color currentColor;
    private Constant constant;
    public int x, y;


    private void Awake()
    {
        constant = new Constant();
    }


    public void InitColor(bool isOffset)
    {
        currentColor = isOffset ? constant.colorsDictionary[TileColor.Offset] : constant.colorsDictionary[TileColor.Base];

        spriteRenderer.color = currentColor;
    }

    public void Visit()
    {
        spriteRenderer.color = constant.colorsDictionary[TileColor.Visited];
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
