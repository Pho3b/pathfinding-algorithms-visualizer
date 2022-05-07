using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    private GridComponent gridComponent;


    private void Awake()
    {
        gridComponent = GridComponent.instance;
    }

    public void ResetButtonOnClick()
    {
        if (!GraphComponent.isAlgorithmRunning)
            gridComponent.ResetGrid();
    }

    public void StartDepthFirstSearch()
    {
        if (!GraphComponent.isAlgorithmRunning)
            gridComponent.StartDFS();
    }
}
