using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public Tile tile;

    [SerializeField] private Camera _camera;
    [SerializeField] private byte width = 16;
    [SerializeField] private byte height = 9;


    private void Awake()
    {
        _camera = _camera ?? Camera.main;
        _camera.transform.position += new Vector3(width / 2, height / 2, -10);
    }

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        byte name = 0;
        bool isOffset = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPosition = new Vector3(x, y, 0);
                Tile spawnedTile = Instantiate(tile, worldPosition, Quaternion.identity);

                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(transform);
                spawnedTile.InitColor(isOffset);

                isOffset = !isOffset;
                name++;
            }
        }
    }
}
