using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public Dictionary<Tile.TileColor, Color> colorsDictionary;

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
        colorsDictionary = new Dictionary<Tile.TileColor, Color>(4);
        colorsDictionary.Add(Tile.TileColor.Base, new Color(0.4295123f, 0.7169812f, 0.5501319f));
        colorsDictionary.Add(Tile.TileColor.Offset, new Color(0.1927287f, 0.6698113f, 0.3887901f));
        colorsDictionary.Add(Tile.TileColor.Visited, new Color(8962264f, 0.8135083f, 0.03804733f));
        colorsDictionary.Add(Tile.TileColor.Found, new Color(0f, 1f, 0f));
    }
}
