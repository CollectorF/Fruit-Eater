using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Horizontal,
    Angle,
    Triple
}


[Serializable]
public struct Elements
{
    public ElementType Type;
    public GameObject ElementAsset;
}

public class ElementLoader : MonoBehaviour
{
    [SerializeField]
    private List<Elements> elementsLibrary;

    private Dictionary<ElementType, GameObject> elementAssets;


    private void Awake()
    {
        elementAssets = new Dictionary<ElementType, GameObject>();

        foreach (var element in elementsLibrary)
        {
            elementAssets.Add(element.Type, element.ElementAsset);
        }
    }

    internal void SetupActiveElements(List<LevelElement> levelElements)
    {
        foreach (var item in levelElements)
        {
            foreach (KeyValuePair<ElementType, GameObject> element in elementAssets)
            {
                elementAssets.TryGetValue(item.Type, out GameObject elementOut);
                Instantiate(elementOut, Vector3.zero, Quaternion.Euler(0, item.Rotation, 0));
                break;
            }
        }
    }
}


