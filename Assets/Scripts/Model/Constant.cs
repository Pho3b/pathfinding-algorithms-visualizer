using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public Dictionary<Enums.TileState, Color> colorsDictionary;


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
        colorsDictionary = new Dictionary<Enums.TileState, Color>(4);
        colorsDictionary.Add(Enums.TileState.Base, new Color(0.4295123f, 0.7169812f, 0.5501319f));
        colorsDictionary.Add(Enums.TileState.Visited, new Color(8962264f, 0.8135083f, 0.03804733f));
        colorsDictionary.Add(Enums.TileState.Found, new Color(0f, 1f, 0f));
        colorsDictionary.Add(Enums.TileState.ToVisit, new Color(1f, 1f, 1f));
        colorsDictionary.Add(Enums.TileState.ToSearch, new Color(1f, 0f, 0f));
        colorsDictionary.Add(Enums.TileState.Obstacle, new Color(0f, 0f, 0f));
    }
}
