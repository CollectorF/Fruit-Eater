using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int targetsQuantity;
    public int elementsQuantity;

    public delegate void TargetsQuantityChangedEvent(int targQuantity, int elemQuantity);

    public event TargetsQuantityChangedEvent OnQuantityChanged;

    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
        levelLoader.OnLevelLoad += SubscribeOnTilesEvents;
        levelLoader.OnLevelLoad += SubscribeOnElementsEvents;

        levelLoader.OnLevelLoad += SetTargetsQuantity;
        levelLoader.OnLevelLoad += SetElementsQuantity;
    }

    private void SubscribeOnTilesEvents()
    {
        TileController[] tiles = FindObjectsOfType<TileController>();
        foreach (var item in tiles)
        {
            item.OnTargetRemove += UpdateTargetsQuantity;
        }
    }

    private void SubscribeOnElementsEvents()
    {
        BaseActionElement[] elements = FindObjectsOfType<BaseActionElement>();
        foreach (var item in elements)
        {
            item.OnDie += UpdateElementsQuantity;
        }
    }

    private void SetTargetsQuantity()
    {
        targetsQuantity = levelLoader.level.TargetsQuantity;
    }

    private void SetElementsQuantity()
    {
        elementsQuantity = levelLoader.levelElements.Count;
    }

    private void UpdateTargetsQuantity()
    {
        targetsQuantity--;
        OnQuantityChanged?.Invoke(targetsQuantity, elementsQuantity);
    }

    internal void UpdateElementsQuantity()
    {
        elementsQuantity--;
        OnQuantityChanged?.Invoke(targetsQuantity, elementsQuantity);
    }
}
