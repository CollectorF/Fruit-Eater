using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int targetsQuantity;

    private void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
        levelLoader.OnLevelLoad += SubscribeOnTilesEvents;
        levelLoader.OnLevelLoad += SetTargetsQuantity;
    }

    private void SubscribeOnTilesEvents()
    {
        TileController[] tiles = FindObjectsOfType<TileController>();
        foreach (var item in tiles)
        {
            item.OnTargetRemove += UpdateTargetsQuantity;
        }
    }

    internal void UpdateTargetsQuantity()
    {
        targetsQuantity--;
        Debug.Log(targetsQuantity);
    }

    private void SetTargetsQuantity()
    {
        targetsQuantity = levelLoader.level.TargetsQuantity;
        Debug.Log(targetsQuantity);
    }

}
