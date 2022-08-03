using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int targetsQuantity;

    public delegate void AllCollectedEvent();

    public event AllCollectedEvent OnAllCollected;

    private void Awake()
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

    private void SetTargetsQuantity()
    {
        targetsQuantity = levelLoader.level.TargetsQuantity;
    }

    internal void UpdateTargetsQuantity()
    {
        targetsQuantity--;
        if (targetsQuantity == 0)
        {
            OnAllCollected?.Invoke();
        }
    }
}
