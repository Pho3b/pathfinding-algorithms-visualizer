using UnityEngine;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    private GridComponent gridComponent;
    [SerializeField] private Toggle weightToggle;
    [SerializeField] private Toggle negativeWeightToggle;


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

    /// <summary>
    /// Cheks if any algorithm is already running, if not, it calls the method to start the 'Breadth First Search'
    /// </summary>
    public void StartDjikstraAlgorithmh()
    {
        if (!GraphComponent.isAlgorithmRunning && GraphComponent.isGraphReady)
            gridComponent.RunAlgorithm(Enums.Algorithm.DjikstraAlgorithm);
    }

    /// <summary>
    /// Cheks if any algorithm is already running, if not, it calls the method to start the 'Breadth First Search'
    /// </summary>
    public void StartAStartAlgorithmh()
    {
        if (!GraphComponent.isAlgorithmRunning && GraphComponent.isGraphReady)
            gridComponent.RunAlgorithm(Enums.Algorithm.AStarAlgorithm);
    }

    /// <summary> 
    /// Toggles weighted/unweighted graph
    /// </summary>
    public void ToggleWeightedGraph()
    {
        if (!GraphComponent.isAlgorithmRunning && GraphComponent.isGraphReady)
        {
            if (weightToggle.isOn)
            {
                gridComponent.AddRandomWeights(negativeWeightToggle.isOn);
            }
            else
            {
                gridComponent.RemoveWeights();
            }
        }

    }

    /// <summary> 
    /// If the 'weightToggle' is On it toggles the weighted/unweighted graph including negative numbers, otherwise
    /// it just sets the behaviour that will be applied when the 'weightToggle' will be set to On
    /// </summary>
    public void ToggleNegativeWeightedGraph()
    {
        if (!GraphComponent.isAlgorithmRunning && GraphComponent.isGraphReady && weightToggle.isOn)
        {
            if (negativeWeightToggle.isOn)
            {
                gridComponent.AddRandomWeights(true);
            }
            else
            {
                gridComponent.AddRandomWeights(false);
            }
        }

    }
}
