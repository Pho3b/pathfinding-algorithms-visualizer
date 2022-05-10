using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    private GridComponent gridComponent;


    /// <summary>
    /// Default Unity Awake
    /// </summary>
    private void Awake()
    {
        gridComponent = GridComponent.instance;
    }

    /// <summary>
    /// Cheks if any algorithm is already running, if not, it calls the method to reset the grid
    /// </summary>
    public void ResetButtonOnClick()
    {
        if (!GraphComponent.isAlgorithmRunning)
            gridComponent.ResetGrid();
    }

    /// <summary>
    /// Cheks if any algorithm is already running, if not, it calls the method to start the 'Depth First Search'
    /// </summary>
    public void StartDepthFirstSearch()
    {
        if (!GraphComponent.isAlgorithmRunning && GraphComponent.isGraphReady)
            gridComponent.RunAlgorithm(Enums.Algorithm.DepthFirstSearch);
    }

    /// <summary>
    /// Cheks if any algorithm is already running, if not, it calls the method to start the 'Breadth First Search'
    /// </summary>
    public void StartBreadthFirstSearch()
    {
        if (!GraphComponent.isAlgorithmRunning && GraphComponent.isGraphReady)
            gridComponent.RunAlgorithm(Enums.Algorithm.BreadthFirstSearch);
    }
}
