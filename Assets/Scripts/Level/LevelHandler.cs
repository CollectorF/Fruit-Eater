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
        foreach (var item in levelLoader.tiles)
        {
            item.GetComponent<TileController>().OnTargetRemove += UpdateTargetsQuantity;
        }
    }

    private void SubscribeOnElementsEvents()
    {
        foreach (var item in levelLoader.elements)
        {
            item.GetComponent<BaseActionElement>().OnDie += UpdateElementsQuantity;
        }
    }

    private void SetTargetsQuantity()
    {
        targetsQuantity = levelLoader.level.TargetsQuantity;
    }

    private void SetElementsQuantity()
    {
        elementsQuantity = levelLoader.elements.Count;
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
