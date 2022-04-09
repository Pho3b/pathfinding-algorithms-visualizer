using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public Tile tile;

    [SerializeField] private Camera _camera;
    [SerializeField] private byte width = 16;
    [SerializeField] private byte height = 9;
    public Tile[,] graph;


    private void Awake()
    {
        _camera = _camera ?? Camera.main;
        _camera.transform.position += new Vector3(width / 2, height / 2, -10);
        graph = new Tile[height, width];
    }

    private void Start()
    {
        CreateGrid();
        ColorRandomVertices();
    }

    private void CreateGrid()
    {
        bool isOffset = true;

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                Vector3 worldPosition = new Vector3(x, y, 0);
                Tile spawnedTile = Instantiate(tile, worldPosition, Quaternion.identity);

                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(transform);
                spawnedTile.InitColor(isOffset);
                spawnedTile.x = x;
                spawnedTile.y = y;

                graph[y, x] = spawnedTile;

                isOffset = !isOffset;
            }
        }
    }

    private void ColorRandomVertices()
    {
        var rand = new System.Random();

        graph[rand.Next(height), rand.Next(width)].Visit();
        graph[rand.Next(height), rand.Next(width)].Visit();
    }

    public static void Print2DArray<T>(T[,] matrix)
    {
        for (int x = 0; x < matrix.GetLength(1); x++)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                Debug.Log(matrix[y, x] + "\t");
            }

            Debug.Log("\n");
        }
    }
}
