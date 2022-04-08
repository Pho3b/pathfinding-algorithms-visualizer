using UnityEngine;

public class GraphComponent : MonoBehaviour
{
    public GridComponent gridComponent;
    private Tile[,] graph;

    private void Start()
    {
        graph = gridComponent.graph;
    }
}
