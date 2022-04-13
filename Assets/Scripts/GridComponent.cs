﻿using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public Tile tile;
    public Tile[,] tilesMatrix;

    [SerializeField] private Camera _camera;
    [SerializeField] private byte width;
    [SerializeField] private byte height;
    [SerializeField] private GraphComponent graphComponent;



    private void Awake()
    {
        _camera = _camera ?? Camera.main;
        _camera.transform.position += new Vector3(width / 2, height / 2, -10);
        tilesMatrix = new Tile[width, height];
        CreateGrid();
    }

    private void Start()
    {
        Tile from = tilesMatrix[5, 7];
        Tile to = tilesMatrix[10, 2];

        StartCoroutine(graphComponent.BreadthFirstSearch(from, to));
    }

    private void CreateGrid()
    {
        int id = 0;

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                Vector3 worldPosition = new Vector3(x, y, 0);
                Tile spawnedTile = Instantiate(tile, worldPosition, Quaternion.identity);

                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(transform);
                spawnedTile.InitColor((x + y) % 2 == 1);
                spawnedTile.x = x;
                spawnedTile.y = y;
                spawnedTile.id = id;

                tilesMatrix[x, y] = spawnedTile;
                id++;
            }

            id++;
        }
    }
}
