using Assets.Scripts.Algorithms;
using System.Collections.Generic;
using UnityEngine;

class BreadthFirstSearch : Algorithm
{
    public override IEnumerator<WaitForSeconds> Run(Tile from, Tile to = null)
    {
        Queue<Tile> queue = new Queue<Tile>();
        Dictionary<int, Tile> parent = new Dictionary<int, Tile>();
        queue.Enqueue(from);

        while (queue.Count > 0 && !GraphComponent.found)
        {
            Tile tile = queue.Dequeue();
            tile.SetState(Enums.TileState.Visited);

            yield return wfs;

            StartCoroutine(AddAdjacentTiles(tile, to, queue, parent));
        }

        if (GraphComponent.found)
            StartCoroutine(HighlightShortestPath(parent, to));

        GraphComponent.isAlgorithmRunning = false;
    }

    private IEnumerator<WaitForSeconds> AddAdjacentTiles(Tile from, Tile to, Queue<Tile> queue, Dictionary<int, Tile> parent)
    {
        for (byte i = 0; i < Constant.DirectionsNum; i++)
        {
            Tile tile = RetrieveAdjacentTile(from.x + rd[i], from.y + cd[i]);

            if (to != null && tile != null && (tile.id == to.id || to.id == from.id))
            {
                to.SetState(Enums.TileState.Found);
                GraphComponent.found = true;
                parent.Add(tile.id, from);
                break;
            }
            else if (tile != null)
            {
                tile.SetState(Enums.TileState.ToVisit);
                queue.Enqueue(tile);
                parent.Add(tile.id, from);

                yield return wfs;
            }
        }
    }

    private IEnumerator<WaitForSeconds> HighlightShortestPath(Dictionary<int, Tile> parent, Tile to)
    {
        Tile t = parent[to.id];
        t.SetState(Enums.TileState.Found);

        while (parent.TryGetValue(t.id, out t))
        {
            yield return wfs;
            t.SetState(Enums.TileState.Found);
        }
    }
}
