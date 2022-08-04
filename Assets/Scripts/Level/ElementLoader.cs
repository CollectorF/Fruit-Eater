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
    [SerializeField]
    private List<Transform> spawnPoints;

    private Dictionary<ElementType, GameObject> elementAssets;

    private GameObject currentObject;

    private void Awake()
    {
        elementAssets = new Dictionary<ElementType, GameObject>();

        foreach (var element in elementsLibrary)
        {
            elementAssets.Add(element.Type, element.ElementAsset);
        }
    }

    internal List<GameObject> SetupActiveElements(List<LevelElement> levelElements)
    {
        List<GameObject> elements = new List<GameObject>();
        foreach (var item in levelElements)
        {
            foreach (KeyValuePair<ElementType, GameObject> element in elementAssets)
            {
                elementAssets.TryGetValue(item.Type, out GameObject elementOut);
                currentObject = Instantiate(elementOut, spawnPoints[item.SpawnPoint].position, Quaternion.Euler(0, item.Rotation, 0));
                elements.Add(currentObject);
                break;
            }
        }
        return elements;
    }
}


