using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private SpriteRenderer _renderer;
    private Color _assignedColor;


    public void InitColor(bool isOffset)
    {
        _assignedColor = isOffset ? _offsetColor : _baseColor;
        _renderer.color = _assignedColor;
    }

    public void Highligh()
    {
        _renderer.color = _highlightColor;
    }

    private void OnMouseDown()
    {
        _renderer.color = _highlightColor;
        Debug.Log(name);
    }

    private void OnMouseUp()
    {
        _renderer.color = _assignedColor;
    }

    private void OnMouseEnter()
    {
        // _renderer.color = _highlightColor;
    }

    private void OnMouseExit()
    {
        // _renderer.color = _assignedColor;
    }
}
