using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Tiles
{
    public char Code;
    public LevelTile MapTile;
    public GameObject TileAsset;
}


[RequireComponent(typeof(LevelFiller))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string levelDir = "Level\\Level";
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private List<Tiles> tilesLibrary;

    private ILevelLoader levelLoader;
    private LevelFiller levelFiller;
    private ElementLoader elementLoader;
    private Dictionary<char, GameObject> tileAssets;
    private List<LevelElement> levelAssets;

    internal Level level;
    
    internal List<GameObject> tiles;
    internal List<GameObject> elements;
    public delegate void LevelLoadEvent();

    public event LevelLoadEvent OnLevelLoad;

    private void Awake()
    {
        tileAssets = new Dictionary<char, GameObject>();
        List<TileController> tiles = new List<TileController>();

        foreach (var tile in tilesLibrary)
        {
            tileAssets.Add(tile.Code, tile.TileAsset);
        }
        levelLoader = new ResourcesLevelLoader(tileAssets);
        levelFiller = GetComponent<LevelFiller>();
        elementLoader = GetComponent<ElementLoader>();
    }

    internal void SetupLevel(int levelid)
    {
        string levelName;
        if (tiles != null && elements != null)
        {
            DestroyLevel();
        }
        levelName = levelDir + (levelid + 1);
        level = levelLoader.ReadLevel(levelName);
        levelAssets = levelLoader.ReadLevelInfo(levelName);
        tiles = levelFiller.FillLevel(tileAssets, level, spawnPoint);
        SetInitialPointPosition();
        elements = elementLoader.SetupActiveElements(levelAssets);
        OnLevelLoad?.Invoke();
    }

    private void SetInitialPointPosition()
    {
        Transform basePrefabTransform = null;
        Transform[] prefabElements = tilesLibrary[0].TileAsset.GetComponentsInChildren<Transform>();
        foreach (var item in prefabElements)
        {
            if (item.CompareTag("Base"))
            {
                basePrefabTransform = item;
                break;
            };
        }
        Vector2 levelSize = level.GetLevelSize();
        spawnPoint.transform.position = new Vector3
            (
                (-levelSize.x * basePrefabTransform.localScale.x / 2) + basePrefabTransform.localScale.x / 2,
                0,
                (levelSize.y * basePrefabTransform.localScale.z / 2) - basePrefabTransform.localScale.z / 2
            );
    }

    private void DestroyLevel()
    {
        foreach (var item in tiles)
        {
            Destroy(item.gameObject);
        }
        tiles.Clear();
        foreach (var item in elements)
        {
            Destroy(item.gameObject);
        }
        elements.Clear();
        spawnPoint.transform.position = Vector3.zero;
    }
}

