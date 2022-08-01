using System;
using System.Collections.Generic;
using UnityEngine;


public class LevelFiller : MonoBehaviour 
{

    public List<TileController> FillLevel(Dictionary<char, GameObject> tileAssets, Level level, GameObject parent)
    {
        List<TileController> tileControllers = new List<TileController>();
        for (int x = 0; x < level.GetLevelSize().x; x++)
        {
            for (int y = 0; y < level.GetLevelSize().y; y++)
            {
                foreach (KeyValuePair<char, GameObject> tile in tileAssets)
                {
                    tileAssets.TryGetValue(level.GetTileAt(x, y).Code, out GameObject tileOut);
                    if (tileOut != null)
                    {
                        Instantiate(tileOut, new Vector3(x, 0, -y), Quaternion.identity, parent.transform);
                        tileControllers.Add(tileOut.GetComponent<TileController>());
                    }
                }
            }
        }
        return tileControllers;
    }
}
