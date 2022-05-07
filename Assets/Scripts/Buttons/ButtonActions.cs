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
        gridComponent.Reset();
    }

    public void StartDepthFirstSearch()
    {
        gridComponent.StartDFS();
    }
}
