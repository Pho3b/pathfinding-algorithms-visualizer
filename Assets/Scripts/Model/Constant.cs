using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public Dictionary<Enums.TileState, Color> colorsDictionary;
    public const byte DirectionsNumber = 4;
    public const float AnimationSeconds = 0.008f;
    public const float FastAnimationSeconds = 0.0005f;
    public const string StateChangeTrigger = "StateChangeTrigger";
    public static float GridTilesOffset = 1.2f;


    /// <summary>
    /// Default constructor
    /// </summary>
    public Constant()
    {
        InitColorsDictionary();
    }

    /// <summary>
    /// Initializes the instance colorsDictionary attribute with all the colors 
    /// that will be used throughout the app to give graphic feedback of each Tile's state.
    /// </summary>
    private void InitColorsDictionary()
    { 
        colorsDictionary = new Dictionary<Enums.TileState, Color>(4)
        {
            { Enums.TileState.Base, new Color32(245, 250, 246, 255) },
            { Enums.TileState.Visited, new Color32(143, 191, 224, 255) },
            { Enums.TileState.Found, new Color32(50, 138, 65, 255) },
            { Enums.TileState.ToVisit, new Color32(194, 249, 187, 255) },
            { Enums.TileState.ToSearch, new Color32(204, 51, 99, 255) },
            { Enums.TileState.Obstacle, Color.black }
        };
    }
}
